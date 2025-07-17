using Microsoft.EntityFrameworkCore;
using Neova.Catalog.Application.Features.Product.Queries.GetAllProducts;
using Neova.Catalog.Domain.Repositories;
using Neova.Catalog.Infrastructure.EventHandlers;
using Neova.Catalog.Infrastructure.Persistance;
using Neova.Catalog.Infrastructure.Repositories;
using Neova.Catalog.Infrastructure.Extensions;
using RabbitMQ.Client;
using MassTransit;
using Neova.Shared.EventBus;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "Neova.Identity.API",
                        ValidAudience = "catalog",
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("burasi_guvenlik_icin_cok_kritik_en_az_128_bit")) // Güvenlik anahtarınızı buraya girin
                    };

                });

//builder.Services.AddScoped<IProductRepository, ProductEFRepository>();
//builder.Services.AddMediatR(config =>
//{
//    //IRequest, IRequestHandler ile imzalad???n nesneler aras?nda arabuluculuk yapmak üzere hepsini tara ve belle?e kaydet!
//    config.RegisterServicesFromAssemblyContaining<GetAllProductsRequest>();
//    config.RegisterServicesFromAssemblyContaining<ProductPriceDiscountedDomainEventHandler>();
//});

//builder.Services.AddDbContext<CatalogDbContext>(opt=> opt.UseSqlServer(builder.Configuration.GetConnectionString("CatalogDb")));,

builder.Services.AddMassTransit(configurator=> 
{
/*
  * Eğer göndeerici, rabbitmq'ya hiç gönderilemezse...; son olaylar db'ye kaydedilecek ve uygulama yeniden başlatıldığında bu olaylar tekrar publish edilecek. Bu pattern'a "Outbox Pattern" denir.*/

configurator.AddEntityFrameworkOutbox<CatalogDbContext>(o =>
    {
        o.UseSqlServer();
        o.UseBusOutbox(); // Outbox kullanmak için bu satırı ekliyoruz
        o.QueryDelay = TimeSpan.FromSeconds(120); // Outbox sorgu gecikmesi


    });


    configurator.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.Publish<ProductPriceDiscountedEvent>(x =>
        {
            x.Durable = true; // Mesaj kalıcı olsun
            x.AutoDelete = false; // Otomatik silinmesin
            x.ExchangeType = ExchangeType.Fanout; // Fanout exchange kullanılsın
        });

        /*
 * 
 * Aynı sorun için başka bir çözüm ise "Dead Letter Queue" kullanmaktır.
 * https://aws.amazon.com/what-is/dead-letter-queue/
 */


    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
