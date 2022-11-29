using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Cart
{
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAdress { get; set; }
    public /*IEnumerable*/List<OrderItem?>? OrdersItemsList { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
    Customer Name: {CustomerName}
    Customer Email: {CustomerEmail}
    Customer Adress: {CustomerAdress}
    List of OrdersItems: {OrdersItemsList}
    Total Price Of Item: {TotalPrice}";


}
