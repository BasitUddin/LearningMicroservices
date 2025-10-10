using MediatR;


namespace LearningMicroservices.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(Guid CustomerId, decimal TotalAmount) : IRequest<Guid>;
}
