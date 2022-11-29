using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public Category? Category { get; set; }
    public double Price { get; set; }
    public int InStock { get; set; }

    public override string ToString() => $@"
    Product Id: {Id}
    Name: {Name}
    category: {Category}
    Price: {Price}
    Amount in stock: {InStock}";


}
