using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neova.Catalog.Application.Features.Product.Commands
{
    public record DiscountProductPriceCommandRequest(Guid ProducId, decimal DiscountRate);

    public record DiscountProductPriceCommandResponse(bool IsSuccess, string Message);
   
}
