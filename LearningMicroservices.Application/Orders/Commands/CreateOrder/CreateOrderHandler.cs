using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using Mapster;
using MediatR;

namespace OrderManagement.Application.Orders.Commands.CreateOrder
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
            try
            {
                var order = request.Adapt<Order>();
                //foreach (var itemDto in request.items)
                //{
                //    var money = new Money(itemDto.UnitPrice, "PKR");
                //    order.AddItem(itemDto.ProductId, itemDto.Quantity, money);
                //}
                //order.TotalAmount = order.GetTotal().Amount;
                var data = await _orderRepository.AddAsync(order);

                return data.Id;
            }
            catch(Exception ex)
            {
                return Guid.Empty;
            }
        }
    }
}
