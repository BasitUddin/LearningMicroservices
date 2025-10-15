using OrderManagement.Application.Products.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IRequest<Product?>;
}
