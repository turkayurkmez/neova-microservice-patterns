using Neova.Catalog.Application.Features.Product.Queries.GetAllProducts;
using Neova.Catalog.Domain.Repositories;
using Neova.Catalog.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductRepository, ProductMockingRepository>();
builder.Services.AddMediatR(config =>
{
    //IRequest, IRequestHandler ile imzalad???n nesneler aras?nda arabuluculuk yapmak üzere hepsini tara ve belle?e kaydet!
    config.RegisterServicesFromAssemblyContaining<GetAllProductsRequest>();
});

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
