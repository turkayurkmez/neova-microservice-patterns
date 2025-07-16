using MassTransit;
using Neova.Shared.EventBus;

namespace Neova.Orders.API.Consumers
{
    public class StockNotAvilableEventConsumer : IConsumer<StockNotAvailableEvent>
    {
        private readonly ILogger<StockNotAvilableEventConsumer> logger;

        public StockNotAvilableEventConsumer(ILogger<StockNotAvilableEventConsumer> logger)
        {
            this.logger = logger;
        }

        public Task Consume(ConsumeContext<StockNotAvailableEvent> context)
        {

            var command = context.Message;

            logger.LogInformation($"Stok başarısız. Sipariş iptal edildi. Sebep: {command.Reason}");

            return Task.CompletedTask;

        }
    }


}
