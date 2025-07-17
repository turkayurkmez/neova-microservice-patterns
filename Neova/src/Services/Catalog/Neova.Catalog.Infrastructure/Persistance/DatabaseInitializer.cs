
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neova.Catalog.Infrastructure.Persistance
{
    public static  class DatabaseInitializer
    {
        public static async Task CreateDatabaseAsync(IHost host)
        {
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<CatalogDbContext>();
            await dbContext.Database.MigrateAsync();

            
         
        }
    }
}
