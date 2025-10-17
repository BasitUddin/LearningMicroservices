using FluentValidation;
using Infrastructure.Persistence;
using OrderManagement.Infrastructure.Repositories;
using OrderManagement.Infrastructure.Services;
using Mapster;
using MapsterMapper;
using Marten;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Reflection;
using Weasel.Core;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Application.Orders.Commands.CreateOrder;
using OrderManagement.Application.Products.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            // JWT Auth
            var jwt = configuration.GetSection("Jwt");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt["Issuer"],
                    ValidAudience = jwt["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!))
                };
            });
            services.AddAuthorization();
            // ---------------------------------------------------------
            // ✅ Database (PostgreSQL + EF Core)
            // ---------------------------------------------------------
            services.AddDbContext<AppDbContext>(options =>
                //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<AppDbContext>();

            // Register Marten document store
            services.AddMarten(options =>
            {
                options.Connection(configuration.GetConnectionString("DefaultConnection"));
                //options.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.All; // Auto-create tables
            });

            // ---------------------------------------------------------
            // ✅ Mapster Configuration (DTO ↔ Domain)
            // ---------------------------------------------------------
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            // ---------------------------------------------------------
            // ✅ API + Swagger
            // ---------------------------------------------------------
            services.AddEndpointsApiExplorer();


            // Redis Configuration
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!);
            });

            // Register Cache Service
            services.AddScoped<ICacheService, RedisCacheService>();

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // ---------------------------------------------------------
            // ✅ MediatR + FluentValidation
            // ---------------------------------------------------------
            //services.AddMediatR(cfg =>
            //    cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            //Order
            services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(typeof(CreateOrderCommand).Assembly));

            services.AddValidatorsFromAssembly(typeof(CreateOrderCommand).Assembly);

            //Product
            services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(typeof(CreateProductCommand).Assembly));

            services.AddValidatorsFromAssembly(typeof(CreateProductCommand).Assembly);


            // ✅ Pipeline for automatic validation
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // Registering Repositories
            services.AddTransient(typeof(IOrderRepository), typeof(OrderRepository));

            TypeAdapterConfig.GlobalSettings.Scan(typeof(MappingConfig).Assembly);






            return services;
        }
    }
}
