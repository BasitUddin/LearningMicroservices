using JasperFx.Core;
using OrderManagement.Application.Products.DTOs;
using OrderManagement.Domain.Interfaces;
using Marten;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Products.Commands
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IDocumentSession _session;
        private readonly ICacheService _cacheService;

        public CreateProductHandler(IDocumentSession session, ICacheService cacheService)
        {
            _session = session;
            _cacheService = cacheService;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var cachedProduct = await _cacheService.GetAsync<IReadOnlyList<Product>>("AllProducts");
            if(cachedProduct != null)
            {
                var data = cachedProduct?.Any(x => x.Name == request.Name);
                if(data == true)
                {
                    return Guid.Empty;
                }
            }

            var product = new Product
            {
                Name = request.Name,
                Price = request.Price
            };

            _session.Store(product);
            await _session.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
