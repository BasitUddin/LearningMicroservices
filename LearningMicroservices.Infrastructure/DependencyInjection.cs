using Application.Common.Interfaces;
using FluentValidation;
using Infrastructure.Persistence;
using LearningMicroservices.Application.Orders.Commands.CreateOrder;
using LearningMicroservices.Application.Products.Commands;
using LearningMicroservices.Domain.Interfaces;
using LearningMicroservices.Domain.Interfaces.Base;
using LearningMicroservices.Infrastructure.Repositories;
using Mapster;
using MapsterMapper;
using Marten;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Weasel.Core;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
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
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Learning MicroServices API",
                    Version = "v1"
                });
            });

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






            return services;
        }
    }
}
