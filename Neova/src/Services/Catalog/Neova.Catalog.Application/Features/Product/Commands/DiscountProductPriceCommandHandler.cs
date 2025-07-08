using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neova.Catalog.Application.Features.Product.Commands
{
    internal class DiscountProductPriceCommandHandler : IRequestHandler<DiscountProductPriceCommandRequest,DiscountProductPriceCommandResponse>
    {
       
        public Task<DiscountProductPriceCommandResponse> Handle(DiscountProductPriceCommandRequest request, CancellationToken cancellationToken)
        {
            //TODO 2: DB işlemleri burada çağrılacak....

            throw new NotImplementedException();
        }
    }
}
