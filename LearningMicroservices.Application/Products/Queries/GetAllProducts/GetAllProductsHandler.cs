using LearningMicroservices.Application.Products.DTOs;
using Marten;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningMicroservices.Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
    {
        private readonly IQuerySession _session;

        public GetAllProductsHandler(IQuerySession session)
        {
            _session = session;
        }

        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var data = await _session.Query<Product>().ToListAsync(cancellationToken);
            return data.ToList();
        }
    }
}
