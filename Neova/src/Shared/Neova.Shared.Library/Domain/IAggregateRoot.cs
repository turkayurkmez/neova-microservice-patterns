using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neova.Shared.Library.Domain
{
    public interface IAggregateRoot
    {
        //Aggregate'lerin olay koleksiyonu olabilir. Bir aggragete içersinde bir olay meydana gelebilir.
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
        void ClearDomainEvent();
    }
}
