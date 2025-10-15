using OrderManagement.Application.Products.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using OrderManagement.Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Order> Orders { get; }
        DbSet<Customer> Customers { get; }
        DbSet<Product> Products { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
