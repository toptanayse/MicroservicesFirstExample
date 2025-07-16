using MassTransit;
using Shared.Events;
using Shared.Messages;
using Stock.API.Models;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Stock.API.Consumer
{
    /*OrderCreatedEvent türünden bir event geldiğinde bunu yakala ve consume fonksiyonunu çalıştır */
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        readonly StockAPIDbContext _context;
        readonly ISendEndpointProvider _sendEndpointProvider;
        readonly IPublishEndpoint _publishEndpoint;


        public OrderCreatedEventConsumer(StockAPIDbContext context, ISendEndpointProvider sendEndpointProvider, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _sendEndpointProvider = sendEndpointProvider;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            /* Stok işlemleri yürütülecek */
            Console.WriteLine(context.Message.OrderId + "-" + context.Message.BuyerId);

            List<bool> stockResult = new();

            foreach (OrderItemMessage item in context.Message.OrderItems)
            {
                stockResult.Add((await _context.Stocks.AnyAsync(s => s.ProductId == item.ProductId && s.Count >= item.Count)));
            }

            if(stockResult.TrueForAll(sr => sr.Equals(true)))
            {
                /* Stok kontrolü başarılı ise */
                foreach (OrderItemMessage item in context.Message.OrderItems)
                {
                    Stock.API.Models.Entities.Stock stockItem = (await _context.Stocks.FirstAsync(t => t.ProductId == item.ProductId));

                    stockItem.Count -= item.Count;

                    _context.Stocks.Update(stockItem);

                    _context.SaveChanges();
                }

                /* Stok işlemlerini tamamlanınca payment ı tetikle  */
                StockReservedEvent stockReservedEvent = new()
                {
                    BuyerId = context.Message.BuyerId,
                    OrderId = context.Message.OrderId,
                    TotalPrice = context.Message.TotalPrice
                };

                 ISendEndpoint sendEndpoint =  await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{ RabbitMQSettings.Payment_StockReservedEventQueue }"));
                await sendEndpoint.Send(stockReservedEvent);

                Console.WriteLine("Stok İşlemleri başarılı");
            }
            else
            {
                /* Stok kontrolü başarısız */
                /* Stok işlemlerini tamamlanınca payment ı tetikle  */
                StockNotReservedEvent stockNotReservedEvent = new()
                {
                    BuyerId = context.Message.BuyerId,
                    OrderId = context.Message.OrderId,
                    Message = "..."
                };

                await _publishEndpoint.Publish(stockNotReservedEvent);

                Console.WriteLine("Stok İşlemleri başarısız");
            }

        }
    }
}
