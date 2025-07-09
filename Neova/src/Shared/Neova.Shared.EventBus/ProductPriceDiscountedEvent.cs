namespace Neova.Shared.EventBus
{

    /*
     * Bu olay, Catalog mikroservisi tarafından yayınlanır ve ürün fiyatı indirimi yapıldığında tetiklenir.
     * Alıcıları; Basket ve Order mikroservisleridir.
     */
    public record ProductPriceDiscountedEvent(Guid ProductId, decimal OldPrice, decimal NewPrice) : IntegrationEvent;
}
