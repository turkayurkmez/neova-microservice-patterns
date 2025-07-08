using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Neova.Catalog.Domain.Events;
using Neova.Shared.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neova.Catalog.Infrastructure.EventHandlers
{
    public class ProductPriceDiscountedDomainEventHandler : INotificationHandler<ProductPriceDiscountedDomainEvent>
    {
        private readonly ILogger<ProductPriceDiscountedDomainEventHandler> logger;
        private readonly IPublishEndpoint publishEndpoint;

        public ProductPriceDiscountedDomainEventHandler(ILogger<ProductPriceDiscountedDomainEventHandler> logger, IPublishEndpoint publishEndpoint)
        {
            this.logger = logger;
            this.publishEndpoint = publishEndpoint;
        }
        public async Task Handle(ProductPriceDiscountedDomainEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Ürün fiyatında indirim yapıldı. Eski fiyat: {notification.OldPrice} yeni fiyat ise {notification.NewPrice} ");

            logger.LogInformation("Domain olayı integration olayına dönüştü ve rabbitmq'ya gönderildi!");

            ProductPriceDiscountedEvent @event = new ProductPriceDiscountedEvent(notification.ProductId, notification.OldPrice, notification.NewPrice);

            await publishEndpoint.Publish(@event, cancellationToken);

           
        }
    }
}
