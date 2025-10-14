using LearningMicroservices.Application.Products.DTOs;
using LearningMicroservices.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
