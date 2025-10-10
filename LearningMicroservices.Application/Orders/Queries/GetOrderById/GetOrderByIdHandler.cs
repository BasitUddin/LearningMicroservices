using Application.Common.Interfaces;
using LearningMicroservices.Application.Orders.DTOs;
using MediatR;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LearningMicroservices.Application.Orders.Queries.GetOrderById
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
    {
        private readonly IAppDbContext _context;

        public GetOrderByIdHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            return order?.Adapt<OrderDto>();
        }
    }
}
