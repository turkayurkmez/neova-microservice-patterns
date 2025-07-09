using MassTransit;
using Neova.Shared.EventBus;

namespace Neova.Stock.API.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        public Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var command = context.Message.Command;
            bool isAvailable = checkStock(command.OrderItems);
        }

        private bool checkStock(List<OrderItemInEvent> orderItems)
        {
            
        }
    }
}
