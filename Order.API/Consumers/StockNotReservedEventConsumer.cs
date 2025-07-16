using MassTransit;
using Order.API.Models;
using Shared.Events;

namespace Order.API.Consumers
{
    public class StockNotReservedEventConsumer : IConsumer<StockNotReservedEvent>
    {
        readonly OrderAPIDbContext _orderAPIDbContext;

        public StockNotReservedEventConsumer(OrderAPIDbContext orderAPIDbContext)
        {
            _orderAPIDbContext = orderAPIDbContext;
        }

        async Task IConsumer<StockNotReservedEvent>.Consume(ConsumeContext<StockNotReservedEvent> context)
        {
            Order.API.Models.Entities.Order order = _orderAPIDbContext.Orders.FirstOrDefault(o => o.OrderID == context.Message.OrderId);

            order.OrderStatu = Models.Enums.OrderStatus.Failed;

            await _orderAPIDbContext.SaveChangesAsync();

        }
    }
}
