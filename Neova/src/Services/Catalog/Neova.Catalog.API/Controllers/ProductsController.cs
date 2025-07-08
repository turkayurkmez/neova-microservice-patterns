using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Neova.Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            /*
             * var request = new  DiscountProductPriceCommandRequest("sdsdsdsdsdsd",0.25);
               var handler = new  DiscountProductPriceCommandHandler();
               var response = handler.Handle(request);
             * 
             * var rsp = arabulucu.Gonder(request);
             */
            return Ok();
        }
    }
}
