using Infrastructure.Persistence;
using LearningMicroservices.Domain.Entities;
using LearningMicroservices.Domain.Interfaces;
using LearningMicroservices.Domain.Interfaces.Base;
using LearningMicroservices.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningMicroservices.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
