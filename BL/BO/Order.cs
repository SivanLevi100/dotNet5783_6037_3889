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
    public  OrderStatus? Status { get; set; }
    public IEnumerable<OrderItem?>? OrdersItemsList { get; set; }
    public double TotalPrice { get; set; }


    public override string ToString() => $@"
    Order Id: {Id}
    Customer Name: {CustomerName}
    Customer Email: {CustomerEmail}
    Customer Adress: {CustomerAdress}
    Order Date: {OrderDate}
    Ship Date: {ShipDate}
    Delivery Date: {DeliveryDate}
    status: {Status}
    List of ordersItems: {OrdersItemsList}
    Total Price: {TotalPrice}";


}
