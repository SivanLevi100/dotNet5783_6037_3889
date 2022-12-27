using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Order
{
    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAdress { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public OrderStatus? Status { get; set; }

    public List<OrderItem?>? OrdersItemsList { get; set; }
    public double TotalPrice { get; set; }


    public override string ToString()
    {
        string str = "";
        str += $"Order Id: {Id} \n";
        str += $"Customer Name: {CustomerName} \n";
        str += $"Customer Email: {CustomerEmail} \n";
        str += $"Customer Adress: {CustomerAdress} \n";
        str += $"Order Date: {OrderDate} \n";
        str += $"Ship Date: {ShipDate} \n";
        str += $"Delivery Date: {DeliveryDate} \n";
        str += $"status: {Status} \n";
        str += $"Total Price: {TotalPrice} \n";
        str += "List of ordersItems: \n";
        foreach (OrderItem? item in OrdersItemsList ?? throw new BO.NotExiestsExceptions("The list of orderItem is not exiest"))
        {
            str += item;
        }
        return str;
    }
}
