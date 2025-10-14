using LearningMicroservices.Domain.Entities;
using LearningMicroservices.Domain.Interfaces;
using MediatR;

namespace LearningMicroservices.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        public CreateOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                CustomerId = request.CustomerId,
                TotalAmount = request.TotalAmount,
            };
            var data = await _orderRepository.AddAsync(order);

            return data.Id;
        }
    }
}
