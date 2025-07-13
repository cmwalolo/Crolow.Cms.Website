using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crolow.Cms.Core.Models.Enumerations
{
    public enum CrolowOrderStatus
    {
        Created = 0,
        PaymentOnHold = 1,
        Paid = 2,
        PartiallyDelivered,
        Delivered = 4
    }

    public enum CrolowOrderLineStatus
    {
        Created = 0,
        Ordered = 1,
        Delivered = 2,
        Cancelled = 3
    }
}
