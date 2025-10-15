using OrderManagement.Application.Products.DTOs;
using Marten;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Products.Queries.GetProductById
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product?>
    {
        private readonly IQuerySession _session;

        public GetProductByIdHandler(IQuerySession session)
        {
            _session = session;
        }

        public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _session.LoadAsync<Product>(request.Id, cancellationToken);
        }
    }
}
