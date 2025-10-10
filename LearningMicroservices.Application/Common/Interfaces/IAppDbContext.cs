using LearningMicroservices.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Order> Orders { get; }
        DbSet<Customer> Customers { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
