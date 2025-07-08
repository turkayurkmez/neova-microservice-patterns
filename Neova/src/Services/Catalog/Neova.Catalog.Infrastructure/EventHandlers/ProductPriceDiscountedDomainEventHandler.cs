using MediatR;
using Microsoft.Extensions.Logging;
using Neova.Catalog.Domain.Events;
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

        public ProductPriceDiscountedDomainEventHandler(ILogger<ProductPriceDiscountedDomainEventHandler> logger)
        {
            this.logger = logger;
        }
        public Task Handle(ProductPriceDiscountedDomainEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Ürün fiyatında indirim yapıldı. Eski fiyat: {notification.OldPrice} yeni fiyat ise {notification.NewPrice} ");

            logger.LogWarning("Daha sonra burada microservice ile iletişim kuracağız.");
            return Task.CompletedTask;
        }
    }
}
