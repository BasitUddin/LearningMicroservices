using Infrastructure.Persistence;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace OrderManagement.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Order?> GetByIdWithCustomerAsync(Guid id)
        {
             return await base._context.Orders.Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
