using MassTransit;
using Neova.Shared.EventBus;

namespace Neova.Orders.API.Consumers
{
    public class OrderProductPriceDiscountConsumer : IConsumer<ProductPriceDiscountedEvent>
    {
        private readonly ILogger<OrderProductPriceDiscountConsumer> _logger;

        public OrderProductPriceDiscountConsumer(ILogger<OrderProductPriceDiscountConsumer> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public Task Consume(ConsumeContext<ProductPriceDiscountedEvent> context)
        {
           var incomingMessage = context.Message;
            _logger.LogInformation($"Order tarafından: Ürün fiyatı indirimi alındı: {incomingMessage.ProductId}, Eski Fiyat: {incomingMessage.OldPrice}, Yeni Fiyat: {incomingMessage.NewPrice}");
            // Burada sipariş güncelleme işlemleri yapılabilir.
            return Task.CompletedTask;

        }
    }
}
