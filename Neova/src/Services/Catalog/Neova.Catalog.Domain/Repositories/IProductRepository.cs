using Neova.Catalog.Domain.Aggregates;
using Neova.Shared.Library.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neova.Catalog.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product,Guid>
    {
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int id);
        Task<IEnumerable<Product>> SearchByNameAsync(string name);
    }
}
