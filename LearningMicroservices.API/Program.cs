using FluentValidation;
using FluentValidation.Results;
using OrderManagement.Application.Orders.Commands.CreateOrder;
using OrderManagement.Application.Orders.DTOs;
using OrderManagement.Application.Orders.Queries.GetOrderById;
using OrderManagement.Application.Products.Commands;
using OrderManagement.Application.Products.DTOs;
using OrderManagement.Application.Products.Queries.GetAllProducts;
using OrderManagement.Application.Products.Queries.GetProductById;
using Mapster;
using MapsterMapper;
using Marten;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using System.Reflection;
using OrderManagement.Application.Orders.Queries.GetOrderById;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

// Add middleware early in the pipeline

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception is ValidationException validationException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                Errors = validationException.Errors.Select(e => e.Value)
            });
            return;
        }
        // Handle generic exceptions
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(new
        {
            Error = "An unexpected error occurred. Please try again later."
        });
    });
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();


app.MapPost("/api/orders", async (CreateOrderCommand command, IMediator mediator) =>
{
    var id = await mediator.Send(command);
    return Results.Created($"/api/orders/{id}", new { id });
})
.WithName("CreateOrder")
.WithTags("Orders")
.Produces<Guid>(StatusCodes.Status201Created);

// 🟣 Get Order by Id
app.MapGet("/api/orders/{id:guid}", async (Guid id, IMediator mediator) =>
{
    var result = await mediator.Send(new GetOrderByIdQuery(id));
    return result is null ? Results.NotFound() : Results.Ok(result);
})
.WithName("GetOrderById")
.WithTags("Orders")
.Produces<OrderDto>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

#region Product APIs
// ✅ Minimal API with MediatR
app.MapPost("/products", async (CreateProductCommand command, IMediator mediator) =>
{
    var id = await mediator.Send(command);
    return Results.Created($"/products/{id}", new { id });
});

app.MapGet("/products/{id:guid}", async (Guid id, IMediator mediator) =>
{
    var product = await mediator.Send(new GetProductByIdQuery(id));
    return product is not null ? Results.Ok(product) : Results.NotFound();
});

app.MapGet("/products", async (IMediator mediator) =>
{
    try
    {
        var products = await mediator.Send(new GetAllProductsQuery());
        return Results.Ok(products);
    }
    catch (Exception ex) 
    { 
        return Results.NotFound();
    }
});
#endregion

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
