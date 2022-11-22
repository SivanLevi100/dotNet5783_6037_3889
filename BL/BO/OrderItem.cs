using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string? NameProduct { get; set; }
    public double Price { get; set; }
    public int AmountInOrder { get; set; }
    public double TotalPriceOfItem { get; set; }

    public override string ToString() => $@"
    Order Item Id: {Id}
    Product Id - {ProductId}
    Name of Product  - {NameProduct}
    Price: {Price}
    Amount: {AmountInOrder}
    Total Price Of Item: {TotalPriceOfItem}";



}
