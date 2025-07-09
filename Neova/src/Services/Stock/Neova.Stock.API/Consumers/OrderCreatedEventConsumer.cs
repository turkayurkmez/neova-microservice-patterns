using MassTransit;
using Neova.Shared.EventBus;

namespace Neova.Stock.API.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly ILogger<OrderCreatedEventConsumer> logger;

        public OrderCreatedEventConsumer(ILogger<OrderCreatedEventConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var command = context.Message.Command;
            bool isAvailable = checkStock(command.OrderItems);
            if (isAvailable)
            {
                var stockAvailableCommand = new StockAvailableCommand(command.OrderId, command.CustomerId, command.CreditCardInfo, command.OrderItems.Sum(oi => oi.Price * oi.Quantity));

                var @event = new StockAvailableEvent(stockAvailableCommand);
                await context.Publish(@event);
                logger.LogInformation("Stok uygun.... Ödeme servisine gönderildi");

            }
            else {
                var notAvailableEvent = new StockNotAvailableEvent(command.OrderId, "Stokta uygun ürün yok!");
                await context.Publish(notAvailableEvent);

                logger.LogInformation("Stok uygun değiş.... Sipariş servisine geri gönderildi!!!");

            }
        }

        private bool checkStock(List<OrderItemInEvent> orderItems)
        {
            return true;
        }
    }
}
