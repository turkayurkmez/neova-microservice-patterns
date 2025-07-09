using MassTransit;
using Neova.Shared.EventBus;

namespace Neova.Basket.API.Consumers
{
    public class BasketProductPriceDiscountConsumer : IConsumer<ProductPriceDiscountedEvent>
    {
        private readonly ILogger<BasketProductPriceDiscountConsumer> _logger;

        public BasketProductPriceDiscountConsumer(ILogger<BasketProductPriceDiscountConsumer> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public Task Consume(ConsumeContext<ProductPriceDiscountedEvent> context)
        {
            var incomingMessage = context.Message;
            _logger.LogInformation($"Basket tarafından: Ürün fiyatı indirimi alındı: {incomingMessage.ProductId}, Eski Fiyat: {incomingMessage.OldPrice}, Yeni Fiyat: {incomingMessage.NewPrice}");

            // Burada sepet güncelleme işlemleri yapılabilir.

            return Task.CompletedTask;


        }
    }

}
