using MassTransit;
using Neova.Basket.API.Consumers;
using Neova.Basket.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();


builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<BasketProductPriceDiscountConsumer>();
    configurator.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("basket-service-discount-queue",e =>
        {
            e.ConfigureConsumer<BasketProductPriceDiscountConsumer>(context);
        });
    });

    
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");



app.Run();
