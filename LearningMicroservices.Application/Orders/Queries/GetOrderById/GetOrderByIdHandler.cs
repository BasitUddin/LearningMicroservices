using Application.Common.Interfaces;
using LearningMicroservices.Application.Orders.DTOs;
using MediatR;
using Mapster;
using Microsoft.EntityFrameworkCore;
using LearningMicroservices.Domain.Interfaces;

namespace LearningMicroservices.Application.Orders.Queries.GetOrderById
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);

            return order?.Adapt<OrderDto>();
        }
    }
}
