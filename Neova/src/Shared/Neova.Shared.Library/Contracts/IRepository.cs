using Neova.Shared.Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neova.Shared.Library.Contracts
{
    public interface IRepository <T, TId> where T : IAggregateRoot
                                          where TId :struct, IEquatable<TId>
    {
        Task<IList<T>> GetAllAsync();
        Task<T> GetByIdAsync(TId id);
        Task<T> AddAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<T> DeleteAsync(T item);

    }
}
