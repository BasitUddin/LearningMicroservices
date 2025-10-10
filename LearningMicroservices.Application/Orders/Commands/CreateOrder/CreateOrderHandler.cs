using Application.Common.Interfaces;
using LearningMicroservices.Domain.Entities;
using MediatR;

namespace LearningMicroservices.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IAppDbContext _context;

        public CreateOrderHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                CustomerId = request.CustomerId,
                TotalAmount = request.TotalAmount,
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }
}
