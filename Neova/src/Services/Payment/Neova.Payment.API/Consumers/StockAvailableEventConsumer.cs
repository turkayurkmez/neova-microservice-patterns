using MassTransit;
using Neova.Shared.EventBus;

namespace Neova.Payment.API.Consumers
{
    public class StockAvailableEventConsumer : IConsumer<StockAvailableEvent>
    {
        private readonly ILogger<StockAvailableEventConsumer> _logger;
        public StockAvailableEventConsumer(ILogger<StockAvailableEventConsumer> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task Consume(ConsumeContext<StockAvailableEvent> context)
        {
            var incomingMessage = context.Message;

            var paymentSuccess = true;
            if (paymentSuccess)
            {
                var @event = new PaymentSuccessfulEvent(incomingMessage.Command.OrderId);
                await context.Publish(@event);
                _logger.LogInformation($"Ödeme servisi tarafından: Ödeme başarılı, sipariş ID: {incomingMessage.Command.OrderId}");
            }
            else
            {
                var failedEvent = new PaymentFailedEvent(incomingMessage.Command.OrderId, "Ödeme işlemi başarısız oldu.");
                await context.Publish(failedEvent);
                _logger.LogInformation($"Ödeme servisi tarafından: Ödeme başarısız, sipariş ID: {incomingMessage.Command.OrderId}");
            }

        
        }
    }
  
}
