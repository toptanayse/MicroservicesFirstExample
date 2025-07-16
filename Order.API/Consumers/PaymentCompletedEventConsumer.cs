using MassTransit;
using Order.API.Models;
using Shared.Events;

namespace Order.API.Consumers
{
    public class PaymentCompletedEventConsumer : IConsumer<PaymentComletedEvent>
    {
        readonly OrderAPIDbContext _orderAPIDbContext;

        public PaymentCompletedEventConsumer(OrderAPIDbContext orderAPIDbContext)
        {
            _orderAPIDbContext = orderAPIDbContext;
        }

        async Task IConsumer<PaymentComletedEvent>.Consume(ConsumeContext<PaymentComletedEvent> context)
        {
            Order.API.Models.Entities.Order order = _orderAPIDbContext.Orders.FirstOrDefault(o => o.OrderID == context.Message.OrderId);

            order.OrderStatu = Models.Enums.OrderStatus.Comleted;

            await _orderAPIDbContext.SaveChangesAsync();

        }
    }
}
