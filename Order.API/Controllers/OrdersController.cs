using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.API.Models;
using Order.API.Models.Entities;
using Order.API.ViewModels;
using Shared.Events;
using Shared.Messages;
using System.Reflection.Metadata.Ecma335;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        readonly OrderAPIDbContext _context;
        readonly IPublishEndpoint _publishEndpoint;    /*MassTransit üzerinden eventi publish etmemizi sağlayacak intance */          
        public OrdersController(OrderAPIDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderVM createOrder)
        {
            Order.API.Models.Entities.Order order = new()
            {
                OrderID = Guid.NewGuid(),
                BuyerId = createOrder.BuyerId,
                CreatedDate = DateTime.Now,
                OrderStatu = Models.Enums.OrderStatus.Suspend
            };

            order.OrderItems = createOrder.OrderItems.Select(x => new OrderItem
            {
                Count = x.Count,
                Price = x.Price,
                ProductId = x.ProductId
            }).ToList();

            order.TotalPrice = createOrder.OrderItems.Sum(x => (x.Price * x.Count));

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            /* Sipariş oluştu Event Fırlatmamız gerekiyor */
            OrderCreatedEvent orderCreatedEvent = new()
            {
                BuyerId = order.BuyerId,
                OrderId = order.OrderID,
                OrderItems = order.OrderItems.Select(x => new OrderItemMessage
                {
                    Count = x.Count,
                    ProductId = x.ProductId,
                }).ToList(),
                TotalPrice = order.TotalPrice,
            };

            /* Publish */
            await _publishEndpoint.Publish(orderCreatedEvent);
            return Ok();
        }
        
        
    }
}
