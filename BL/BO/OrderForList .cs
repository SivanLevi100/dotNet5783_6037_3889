using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderForList
{
    public int OrderId { get; set; }
    public string? CustomerName { get; set; }
    public OrderStatus Status { get; set; }
    public int AmountItems { get; set; }
    public double TotalPrice { get; set; }
    public override string ToString() => $@"
    Order Id: {OrderId}
    Customer Name: {CustomerName}
    Status: {Status}
    Amount of Items: {AmountItems}
    Total Price: {TotalPrice}";


}
