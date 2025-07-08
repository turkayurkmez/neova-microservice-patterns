namespace Neova.Shared.Library.Domain
{

    /*
     * Aggregate, toplu entity demektir. Başka varlıkları biraraya getirerek yöneten bir varlıktır.
     * 
     *  order.OrderItems.Add();
     */
    public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot where T: struct,IEquatable<T>
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
           
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvent()
        {
            _domainEvents.Clear();
        }
    }
}
