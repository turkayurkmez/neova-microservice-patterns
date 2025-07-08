using MediatR;
using Neova.Catalog.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neova.Catalog.Application.Features.Product.Queries.GetAllProducts
{

    //1. Bu nesnenin oluşabilmesi için IProductRepository interface'ini implemente eden bir sınıfa ihtiyaç var:
    internal class GetAllProductsHandler(IProductRepository productRepository) : IRequestHandler<GetAllProductsRequest, GetAllProductsResponse>
    {
        public async Task<GetAllProductsResponse> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetAllAsync();
            var dtoResult = products.ToList().Select(p => new ProductsForDisplay(p.Id, p.Price, p.Description, p.CategoryId, p.ImageUrl));
            return new GetAllProductsResponse(dtoResult);
        }
    }
}
