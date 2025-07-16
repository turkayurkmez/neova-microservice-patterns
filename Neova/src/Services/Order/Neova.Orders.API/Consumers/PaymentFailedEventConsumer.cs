using MassTransit;
using Neova.Shared.EventBus;

namespace Neova.Orders.API.Consumers
{
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


}
