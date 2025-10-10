using Mapster;
using MapsterMapper;
using System.Reflection;
using MediatR;
using LearningMicroservices.Application.Orders.Commands.CreateOrder;
using LearningMicroservices.Application.Orders.Queries.GetOrderById;
using LearningMicroservices.Application.Orders.DTOs;
using FluentValidation;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

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

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
