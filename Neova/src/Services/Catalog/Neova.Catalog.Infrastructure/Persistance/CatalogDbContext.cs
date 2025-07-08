using MediatR;
using Microsoft.EntityFrameworkCore;
using Neova.Catalog.Domain.Aggregates;
using Neova.Shared.Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neova.Catalog.Infrastructure.Persistance
{
    public class CatalogDbContext : DbContext
    {

        private readonly IMediator mediator;
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options, IMediator mediator) : base(options)
        {
            this.mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Product>()
                        .HasOne(p => p.Category)
                        .WithMany(c => c.Products)
                        .HasForeignKey(p => p.CategoryId)
                        .OnDelete(DeleteBehavior.NoAction);

            //3 Kategori ekle :

            modelBuilder.Entity<Category>().HasData(
                new Category(1, "Elektronik", "Elektronik ürünler"),
                new Category(2, "Giyim", "Giyim ürünleri"),
                new Category(3, "Ev & Yaşam", "Ev ve yaşam ürünleri")
            );


            //her kategoriye 1 ürün ekle:

            modelBuilder.Entity<Product>().HasData(
                new Product("Akıllı Telefon", "Yüksek performanslı akıllı telefon", 999.99m, 50, "phone.png", 1),
                new Product("Tişört", "Pamuklu tişört", 19.99m, 100, "tshirt.png", 2),
                new Product("Koltuk Takımı", "Konforlu koltuk takımı", 2999.99m, 20, "sofa.png", 3)
            );




        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //return await base.SaveChangesAsync(cancellationToken);

            //TODO 4: Domain eventleri burada yakala ve publish et. 
            //1. kayedilen entity'ler içindeki domain eventleri dolu olanları al.

            var domainEvents = ChangeTracker.Entries<IAggregateRoot>()
                .Where(e => e.Entity.DomainEvents != null && e.Entity.DomainEvents.Any())
                .SelectMany(e => e.Entity.DomainEvents)
                .ToList();

            
            var output = await base.SaveChangesAsync(cancellationToken);
            //2. domain eventleri publish et.
            foreach (var domainEvent in domainEvents)
            {
               await mediator.Publish(domainEvent, cancellationToken);
            }

            return output;









        }
    }
}
