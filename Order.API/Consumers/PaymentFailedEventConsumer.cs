using MassTransit;
using Order.API.Models;
using Shared.Events;

namespace Order.API.Consumers
{
    public class PaymentFailedEventConsumer: IConsumer<PaymentFailedEvent>
    {
        readonly OrderAPIDbContext _orderAPIDbContext;

        public PaymentFailedEventConsumer(OrderAPIDbContext orderAPIDbContext)
        {
            _orderAPIDbContext = orderAPIDbContext;
        }

        async Task IConsumer<PaymentFailedEvent>.Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            Order.API.Models.Entities.Order order = _orderAPIDbContext.Orders.FirstOrDefault(o => o.OrderID == context.Message.OrderId);

            order.OrderStatu = Models.Enums.OrderStatus.Failed;

            await _orderAPIDbContext.SaveChangesAsync();

        }
    }
}
