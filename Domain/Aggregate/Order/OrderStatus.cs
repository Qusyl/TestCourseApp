using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregate.Order
{
    public enum OrderStatus
    {
        Pending, 
        Paid,
        Cancelled
    }
}
