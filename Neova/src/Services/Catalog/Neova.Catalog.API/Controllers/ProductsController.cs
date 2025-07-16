using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Neova.Catalog.Application.Features.Product.Commands;
using Neova.Catalog.Application.Features.Product.Queries.GetAllProducts;

namespace Neova.Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            /*
             * var request = new  DiscountProductPriceCommandRequest("sdsdsdsdsdsd",0.25);
               var handler = new  DiscountProductPriceCommandHandler();
               var response = handler.Handle(request);
             * 
             * var rsp = arabulucu.Gonder(request);
             */

            var request = new GetAllProductsRequest();
            var response = await mediator.Send(request);
            return Ok(response);
        }


        //Fiyat indirimi için bir endpoint ekleyelim:

        [HttpPost("discount")]
        public async Task<IActionResult> DiscountProductPrice(DiscountProductPriceCommandRequest request)
        {
          
            var response = await mediator.Send(request);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }


    }
}
