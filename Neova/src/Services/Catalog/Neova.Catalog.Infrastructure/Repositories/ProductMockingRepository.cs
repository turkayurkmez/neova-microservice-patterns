using Neova.Catalog.Domain.Aggregates;
using Neova.Catalog.Domain.Repositories;
using Neova.Shared.Library.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neova.Catalog.Infrastructure.Repositories
{
    public class ProductMockingRepository : IProductRepository
    {
        public Task<Product> AddAsync(Product item)
        {
            throw new NotImplementedException();
        }

        public Task<Product> DeleteAsync(Product item)
        {
            throw new NotImplementedException();
        }

        async Task<IList<Product>> GetAllAsync()
        {
            //TODO 3: 1 yerine CategoryId kullanılacak!!!!!
            return await Task.FromResult(new List<Product>() {

               new Product("Test ürün", "Test açıklama",1,null,null)
            });
        }

        public Task<Product> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> SearchByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateAsync(Product item)
        {
            throw new NotImplementedException();
        }

        Task<IList<Product>> IRepository<Product, Guid>.GetAllAsync()
        {
            return GetAllAsync();
        }
    }
}
