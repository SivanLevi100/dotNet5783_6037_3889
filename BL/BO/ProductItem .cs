using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class ProductItem
{
    public int IdProduct { get; set; } 
    public string? Name { get; set; }
    public double Price { get; set; }
    public Category Category { get; set; }
    public bool IsAvailable { get; set; }
    public int AmountInCart { get; set; }

    public override string ToString() => $@"
    Product Id: {IdProduct}
    Name: {Name}
    category: {Category}
    Price: {Price}
    IsAvailable: {IsAvailable}
    Amount Item In Cart: {AmountInCart}";

}
