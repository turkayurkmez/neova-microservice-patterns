using MediatR;
using Neova.Catalog.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neova.Catalog.Application.Features.Product.Commands
{
    internal class DiscountProductPriceCommandHandler : IRequestHandler<DiscountProductPriceCommandRequest,DiscountProductPriceCommandResponse>
    {

        private readonly IProductRepository productRepository;

        public DiscountProductPriceCommandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<DiscountProductPriceCommandResponse> Handle(DiscountProductPriceCommandRequest request, CancellationToken cancellationToken)
        {
            //TODO 2: DB işlemleri burada çağrılacak....

            var product =  await productRepository.GetByIdAsync(request.ProducId);
            if (product is null)
            {
                return new DiscountProductPriceCommandResponse(false, "Ürün bulunamadı");
            }
            product.ApplyDiscount(request.DiscountRate);
            await productRepository.UpdateAsync(product);


            return new DiscountProductPriceCommandResponse(true, "Ürün fiyatında indirim yapıldı");
        }
    }
}
