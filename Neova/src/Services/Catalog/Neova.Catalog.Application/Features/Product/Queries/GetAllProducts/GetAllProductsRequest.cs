using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neova.Catalog.Application.Features.Product.Queries.GetAllProducts
{
    public record GetAllProductsRequest() : IRequest<GetAllProductsResponse> ;

    public record GetAllProductsResponse(IEnumerable<ProductsForDisplay> Products);

    public record ProductsForDisplay(Guid ProductId, decimal Price, string Description, int CategoryID, string ImageUrl ); 


   
}
