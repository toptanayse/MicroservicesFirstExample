using Shared.Events.Common;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class OrderCreatedEvent: IEvent
    {
        /* Stok kontrolü için gönderilecek bilgileri içeren event */
        public Guid OrderId { get; set; }
        public Guid BuyerId { get; set; }
        public List<OrderItemMessage> OrderItems { get; set; }

        /* Stoktan sonra payment API da gerekli total price */
        public decimal TotalPrice { get; set; }

    }
}
