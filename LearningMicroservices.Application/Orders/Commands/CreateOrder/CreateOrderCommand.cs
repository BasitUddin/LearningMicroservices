using OrderManagement.Application.Orders.DTOs;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.ValueObjects;
using MediatR;


namespace OrderManagement.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(Guid CustomerId, Address ShippingAddress, List<OrderItemDto> Items) : IRequest<Guid>;
}
