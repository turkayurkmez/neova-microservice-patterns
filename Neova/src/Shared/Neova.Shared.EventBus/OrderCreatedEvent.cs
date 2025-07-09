using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neova.Shared.EventBus
{
    //public record OrderCreatedEvent()


    /*
     * Hem happy hem de unhappy path'leri için event'ler tanımladık. Elbette en ideali ayrı ayrı dosyalar içinde tanımlamak ama burada örnek olması açısından tek dosyada tanımladık.
     */

    public record OrderItemInEvent(string ProductId, int Quantity, decimal Price);

    public record OrderCreatedCommand(int OrderId, string CustomerId, List<OrderItemInEvent> OrderItems, string CreditCardInfo);

    public record OrderCreatedEvent(OrderCreatedCommand Command) : IntegrationEvent;


    public record StockAvailableEvent(StockAvailableCommand Command):IntegrationEvent;
       
    public record StockAvailableCommand(int OrderId, string CustomerId, string CreditCardInfo, decimal? TotalPrice);

    //Stock not available event
    public record StockNotAvailableEvent(int OrderId, string Reason) : IntegrationEvent;

    //Ödeme başarılıysa; orderId'yi içeren bir event gönderilecek
    public record PaymentSuccessfulEvent(int OrderId) : IntegrationEvent;

    //Ödeme başarısızsa; orderId'yi ve sebebini içeren bir event gönderilecek
    public record PaymentFailedEvent(int OrderId, string Reason) : IntegrationEvent;



}
