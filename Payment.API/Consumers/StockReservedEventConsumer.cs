using MassTransit;
using Shared.Events;

namespace Payment.API.Consumers
{
    public class StockReservedEventConsumer : IConsumer<StockReservedEvent>
    {
        readonly IPublishEndpoint _endpoint;

        public StockReservedEventConsumer(IPublishEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        public Task Consume(ConsumeContext<StockReservedEvent> context)
        {
            /* Ödeme işlemleri */
            if (true)
            {
                /* Ödeme başarılıysa ilgili Order ın Id sini alıp OrderId ye gönderecek ve onun statusunu değiştirecek */
                PaymentComletedEvent paymentComletedEvent = new()
                {
                    OrderId = context.Message.OrderId
                };

                _endpoint.Publish(paymentComletedEvent);

                Console.WriteLine("Ödeme Başarılı"); 
            }
            else
            {
                /* Ödeme başarılı değilse */
                PaymentFailedEvent paymentFailedEvent = new()
                {
                    OrderId = context.Message.OrderId,
                    Message = "Bakiye yetersiz"
                };
                _endpoint.Publish(paymentFailedEvent);

                Console.WriteLine("Ödeme Başarız");
            }
            return Task.CompletedTask;
        }
    }
}
