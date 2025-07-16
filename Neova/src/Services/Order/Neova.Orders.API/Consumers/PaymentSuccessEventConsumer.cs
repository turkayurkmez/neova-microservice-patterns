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


}
