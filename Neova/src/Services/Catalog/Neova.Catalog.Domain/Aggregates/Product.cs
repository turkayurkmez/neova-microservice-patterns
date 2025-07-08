using Neova.Catalog.Domain.Events;
using Neova.Shared.Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neova.Catalog.Domain.Aggregates
{

    //Yarattığın uygulama evreninde NE VAR? 
    public class Product : AggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        
        public decimal Price { get; private set; }
        public int? Stock { get; private set; }

        //TODO 1: Category ile navigation property kurmayı unutma....

        public string? ImageUrl { get; set; } = "noImage.png";

        public int CategoryId { get; private set; }
        public Category Category { get; set; }

        public Product() { }

        public Product(string name, string description, decimal price, int? stock, string? imageUrl, int? categoryID)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            ImageUrl = imageUrl;
        }

        public void ApplyDiscount(decimal discountRate)
        {
            var oldPrice = Price;
            Price = Price * (1 - discountRate);

            //Olayı oluştur ve fırlatmak üzere ekle:
            ProductPriceDiscountedDomainEvent @event = new ProductPriceDiscountedDomainEvent(this.Id, oldPrice, Price);
            AddDomainEvent(@event);

        }
    }
}
