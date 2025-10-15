using OrderManagement.Application.Products.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Products.Queries.GetAllProducts
{
    public record GetAllProductsQuery() : IRequest<List<Product>>;
}
