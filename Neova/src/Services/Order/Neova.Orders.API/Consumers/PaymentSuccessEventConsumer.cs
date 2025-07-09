using MassTransit;
using Neova.Shared.EventBus;

namespace Neova.Orders.API.Consumers
{
    public class PaymentSuccessEventConsumer : IConsumer<PaymentSuccessfulEvent>
    {
        private readonly ILogger<PaymentSuccessEventConsumer> logger;

        public PaymentSuccessEventConsumer(ILogger<PaymentSuccessEventConsumer> logger)
        {
            this.logger = logger;
        }

        public Task Consume(ConsumeContext<PaymentSuccessfulEvent> context)
        {

            var command = context.Message;

            logger.LogInformation("Ödeme başarılı. Sipariş onaylandı");

            return Task.CompletedTask;

        }
    }

    public class PaymentFailedEventConsumer : IConsumer<PaymentFailedEvent>
    {
        private readonly ILogger<PaymentFailedEventConsumer> logger;

        public PaymentFailedEventConsumer(ILogger<PaymentFailedEventConsumer> logger)
        {
            this.logger = logger;
        }

        public Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {

            var command = context.Message;

            logger.LogInformation($"Ödeme başarısız. Sipariş iptal edildi. Sebep: {command.Reason}");

            return Task.CompletedTask;

        }
    }

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
