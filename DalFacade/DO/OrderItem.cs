

using System.Xml.Linq;

namespace DO;
/// <summary>
/// struct of Order Item
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// Unique ID of Item
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Item ID number
    /// </summary>
    public int  ProductId { get; set; }
    /// <summary>
    /// Order ID number 
    /// </summary>
    public int OrderId { get; set; }
    /// <summary>
    /// Price of Item
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// Amount of Items
    /// </summary>
    public int Amount { get; set; } 

    public override string ToString() => $@"
    Order Item Id={Id}
    Product Id - {ProductId}
    Order Id - {OrderId}
    Price: {Price}
    Amount: {Amount}
";



}

