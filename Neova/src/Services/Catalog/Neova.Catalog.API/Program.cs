using Microsoft.EntityFrameworkCore;
using Neova.Catalog.Application.Features.Product.Queries.GetAllProducts;
using Neova.Catalog.Domain.Repositories;
using Neova.Catalog.Infrastructure.EventHandlers;
using Neova.Catalog.Infrastructure.Persistance;
using Neova.Catalog.Infrastructure.Repositories;
using Neova.Catalog.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

//builder.Services.AddScoped<IProductRepository, ProductEFRepository>();
//builder.Services.AddMediatR(config =>
//{
//    //IRequest, IRequestHandler ile imzalad???n nesneler aras?nda arabuluculuk yapmak üzere hepsini tara ve belle?e kaydet!
//    config.RegisterServicesFromAssemblyContaining<GetAllProductsRequest>();
//    config.RegisterServicesFromAssemblyContaining<ProductPriceDiscountedDomainEventHandler>();
//});

//builder.Services.AddDbContext<CatalogDbContext>(opt=> opt.UseSqlServer(builder.Configuration.GetConnectionString("CatalogDb")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
