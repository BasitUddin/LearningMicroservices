using LearningMicroservices.Application.Products.DTOs;
using Marten;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningMicroservices.Application.Products.Commands
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IDocumentSession _session;

        public CreateProductHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
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
