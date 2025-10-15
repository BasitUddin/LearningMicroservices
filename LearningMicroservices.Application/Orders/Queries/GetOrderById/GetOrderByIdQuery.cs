using OrderManagement.Application.Orders.DTOs;
using MediatR;


namespace OrderManagement.Application.Orders.Queries.GetOrderById
{
    public record GetOrderByIdQuery(Guid Id) : IRequest<OrderDto?>;
}
