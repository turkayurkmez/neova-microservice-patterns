using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neova.Catalog.Application.Features.Product.Commands
{

    /*
     * Vertical Slice (dikey büyüme) tercih edildi.
     * 
     * 1. İsteği tutan nesne (request)
     * 2. İsteği değerlendiren ve uygulayan nesne (handler)
     * 3. İstek değerlendirme ve uygulanmasının ardından dönecek nesne (respnse)
     */
    public record DiscountProductPriceCommandRequest(Guid ProducId, decimal DiscountRate): IRequest<DiscountProductPriceCommandResponse>;

    public record DiscountProductPriceCommandResponse(bool IsSuccess, string Message);
   
}
