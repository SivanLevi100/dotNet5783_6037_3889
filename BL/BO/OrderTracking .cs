using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderTracking
{
    public int OrderId { get; set; }
    public OrderStatus? Status { get; set; }
    public List<Tuple<DateTime?,string?>>? Tracking { get; set; }

    public override string ToString()
    {
        string str = "";
        str += $"Order Id: {OrderId} \n";
        str+= $"Status: { Status}\n";
        str+=$"Tracking: ";
        foreach (var item in Tracking)
        {
            str +=$"{item}\n";
        }
        return str;
    }
}

