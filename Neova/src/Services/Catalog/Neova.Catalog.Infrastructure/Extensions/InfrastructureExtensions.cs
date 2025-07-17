using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Neova.Catalog.Application.Features.Product.Queries.GetAllProducts;
using Neova.Catalog.Domain.Repositories;
using Neova.Catalog.Infrastructure.EventHandlers;
using Neova.Catalog.Infrastructure.Persistance;
using Neova.Catalog.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neova.Catalog.Infrastructure.Extensions
{
    public static  class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
          services.AddScoped<IProductRepository, ProductEFRepository>();
            var connectionString = configuration.GetConnectionString("CatalogDb");
            var defaultHost = configuration["DefaultHost"];
            var defaultPass = configuration["DefaultPass"];
            connectionString = connectionString.Replace("[HOST]", defaultHost);
            connectionString = connectionString.Replace("[PASS]", defaultPass);


            services.AddDbContext<CatalogDbContext>(opt => opt.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                //IRequest, IRequestHandler ile imzalad???n nesneler aras?nda arabuluculuk yapmak üzere hepsini tara ve belle?e kaydet!
                config.RegisterServicesFromAssemblyContaining<GetAllProductsRequest>();
                config.RegisterServicesFromAssemblyContaining<ProductPriceDiscountedDomainEventHandler>();
            });
            return services;
        }
    }
}
