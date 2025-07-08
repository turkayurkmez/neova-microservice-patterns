using Microsoft.EntityFrameworkCore;
using Neova.Catalog.Domain.Aggregates;
using Neova.Catalog.Domain.Repositories;
using Neova.Catalog.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neova.Catalog.Infrastructure.Repositories
{
    public class ProductEFRepository(CatalogDbContext catalogDbContext) : IProductRepository
    {

        public async Task<Product> AddAsync(Product item)
        {
            catalogDbContext.Products.Add(item);
            await catalogDbContext.SaveChangesAsync();
            return item;


        }

        public async Task<Product> DeleteAsync(Product item)
        {
            catalogDbContext.Products.Remove(item);
            await catalogDbContext.SaveChangesAsync();
            return item;

        }

        public async Task<IList<Product>> GetAllAsync()
        {
          
            return await catalogDbContext.Products               
                .ToListAsync();

        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await catalogDbContext.Products
                .FirstOrDefaultAsync(p => p.Id == id);

        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int id)
        {
            return await catalogDbContext.Products.Where(p=>p.CategoryId==id).ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchByNameAsync(string name)
        {
            return await catalogDbContext.Products
                .Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();

        }

        public async Task<Product> UpdateAsync(Product item)
        {
            catalogDbContext.Products.Update(item);
            await catalogDbContext.SaveChangesAsync();
            return item;

        }
    }
}
