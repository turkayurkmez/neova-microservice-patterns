namespace Neova.Shared.Library.Domain
{
    //Geçmişte olmuş ve bitmiş ve sistemin takip etmesi gerektiğini düşündüğünüz her eylem bir domainevent'tir.
    public abstract class DomainEvent : IDomainEvent
    {
        public Guid Id { get; protected set; }

        public DateTime OccuredOn{ get; protected set; }

        protected DomainEvent()
        {
            Id = Guid.NewGuid();
            OccuredOn = DateTime.UtcNow;
        }
    }
}
