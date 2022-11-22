using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderItem
{
    /// <summary>
    /// Unique ID of Item
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Item ID number
    /// </summary>
    public int ProductId { get; set; }
    /// <summary>
    /// Order ID number 
    /// </summary>
   
    public string? NameProduct { get; set; }

    /// <summary>
    /// Price of Item
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Amount of Items
    /// </summary>
    public int AmountInOrder { get; set; }

    public double TotalPriceOfItem { get; set; }


    
    

    /// <summary>
    /// Printing method
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
    Order Item Id: {Id}
    Product Id - {ProductId}
    Name of Product  - {NameProduct}
    Price: {Price}
    Amount: {AmountInOrder}
    Total Price Of Item: {TotalPriceOfItem}";



}
