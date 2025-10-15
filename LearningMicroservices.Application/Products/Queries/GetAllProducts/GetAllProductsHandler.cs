using OrderManagement.Application.Products.DTOs;
using OrderManagement.Domain.Interfaces;
using Marten;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
    {
        private readonly IQuerySession _session;
        private readonly ICacheService _cache;

        public GetAllProductsHandler(IQuerySession session, ICacheService cache)
        {
            _session = session;
            _cache = cache;
        }

        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var data = await _session.Query<Product>().ToListAsync(cancellationToken);
            var cacheKey = "AllProducts";
            await _cache.SetAsync(cacheKey, data, TimeSpan.FromMinutes(30));
            return data.ToList();
        }
    }
}
