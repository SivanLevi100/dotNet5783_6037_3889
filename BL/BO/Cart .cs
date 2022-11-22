using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

internal class Cart
{
    /// <summary>
    /// Name of Customer
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    ///  Email of Customer
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    ///  Adress of Customer
    /// </summary>
    public string? CustomerAdress { get; set; }

    public IEnumerable<OrderItem?>? OrdersItemsList { get; set; }

    public double TotalPriceOfItem { get; set; }

    public override string ToString() => $@"
    Customer Name: {CustomerName}
    Customer Email: {CustomerEmail}
    Customer Adress: {CustomerAdress}
    List of OrdersItems: {OrdersItemsList}
    Total Price Of Item: {TotalPriceOfItem}";


}
