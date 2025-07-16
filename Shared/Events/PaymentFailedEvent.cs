using Shared.Events.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class PaymentFailedEvent: IEvent
    {
        /* Ödeme başarızı olursa Order Servise gönderilecek event bilgileri  */
        public Guid OrderId { get; set; }

        public string Message { get; set; }
    }
}
