using Shared.Events.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class PaymentComletedEvent : IEvent
    {
        /* Ödeme başarılı olduktan sonra tetiklenecek olan event bilgileri */
        public Guid OrderId { get; set; }
    }
}
