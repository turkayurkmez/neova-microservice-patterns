using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Neova.Shared.EventBus;

namespace Neova.Orders.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public OrdersController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            //db işlemleri burada yapılacak (Puzzle'ın bu kısmı sizde :))

            var orderItems = request.OrderItems.Select(item => new OrderItemInEvent(item.ProductId, item.Quantity, item.Price)).ToList();

            var randomOrderId = new Random().Next(1000, 9999); // Örnek olarak rastgele bir sipariş ID'si oluşturuyoruz.
            var orderCreatedCommand = new OrderCreatedCommand(randomOrderId, request.CustomerId, orderItems, "11111111");
            var @event = new OrderCreatedEvent(orderCreatedCommand);
            await _publishEndpoint.Publish(@event);
            return Ok(new { message =$"{randomOrderId} id'li sipariş oluşturuldı" }); 

        }
    }

    public record CreateOrderRequest(string CustomerId, List<OrderItem> OrderItems);
    public record OrderItem(string ProductId, int Quantity, decimal Price);
}
