using LearningMicroservices.Application.Orders.DTOs;
using MediatR;


namespace LearningMicroservices.Application.Orders.Queries.GetOrderById
{
    public record GetOrderByIdQuery(Guid Id) : IRequest<OrderDto?>;
}
