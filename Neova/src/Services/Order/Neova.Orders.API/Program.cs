using MassTransit;
using Neova.Orders.API.Consumers;
using Neova.Shared.EventBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<OrderProductPriceDiscountConsumer>(c =>
    {
        c.UseMessageRetry(r =>
        {
            r.Intervals(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(15)); // 3 kez dene, her seferinde 5 saniye bekle 
            //r.Exponential(5, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(1)); // 3 kez dene, her seferinde 5 saniye bekle
            r.Handle<TimeoutException>(); // TimeoutException hatas?nda retry yap

            //r.ConnectRetryObserver(); // Retry işlemlerini izlemek için bir observer ekle
        });

        c.UseCircuitBreaker(cb => {
            cb.TrackingPeriod = TimeSpan.FromSeconds(30); // 30 saniye boyunca başarısızlıkları takip et
            cb.TripThreshold = 5; // 5 kez başarısız olursa devre kesilsin
            cb.ActiveThreshold = 3; // Devre kesici aktif olmadan önce gereken deneme sayısı
            cb.ResetInterval = TimeSpan.FromMinutes(1); // Devre kesildikten sonra 1 dakika bekle
        });



    });

    configurator.AddConsumer<PaymentSuccessEventConsumer>();
    configurator.AddConsumer<PaymentFailedEventConsumer>();
    configurator.AddConsumer<StockNotAvilableEventConsumer>();

    /*
     * 
     * https://microservices.io/patterns/data/saga.html
     */


    configurator.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("order-service-discount-queue", e =>
        {
            e.ConfigureConsumer<OrderProductPriceDiscountConsumer>(context);
        });

        cfg.ConfigureEndpoints(context);
    });

    /*
 * İlk seferde ulaşamazsa retry yapacak.
 * Eğer hiç ulaşamazsa; son olaylar db'ye kaydedilecek ve uygulama yeniden başlatıldığında bu olaylar tekrar publish edilecek. Bu pattern'a "Outbox Pattern" denir.
 * 
 * Aynı sorun için başka bir çözüm ise "Dead Letter Queue" kullanmaktır.
 * https://aws.amazon.com/what-is/dead-letter-queue/
 */



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
