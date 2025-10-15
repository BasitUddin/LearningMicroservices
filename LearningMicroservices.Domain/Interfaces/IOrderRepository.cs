using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<Order?> GetByIdWithCustomerAsync(Guid id);
    }
}
