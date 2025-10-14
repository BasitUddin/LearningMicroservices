using LearningMicroservices.Domain.Entities;
using LearningMicroservices.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningMicroservices.Domain.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
    }
}
