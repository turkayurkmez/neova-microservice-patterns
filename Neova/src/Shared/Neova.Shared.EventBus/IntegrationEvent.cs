using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neova.Shared.EventBus
{
    public record IntegrationEvent 
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
        public Guid Id { get; private set; }
        public DateTime CreationDate { get; private set; }

    }

    public record ProductPriceDiscountedEvent(Guid ProductId, decimal OldPrice, decimal NewPrice) : IntegrationEvent;
}
