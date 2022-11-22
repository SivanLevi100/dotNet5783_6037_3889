using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Order
{
    /// <summary>
    /// Order ID number
    /// </summary>
    public int Id { get; set; }
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
    /// <summary>
    ///  Date of Order
    /// </summary>
    public DateTime? OrderDate { get; set; }
    /// <summary>
    /// date of shipment 
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// Date of delivery
    /// </summary>
    public DateTime? DeliveryDate { get; set; }

    /// <summary>
    /// Printing method
    /// </summary>
    /// <returns></returns>
    /// 
    public  OrderStatus Status { get; set; }
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
