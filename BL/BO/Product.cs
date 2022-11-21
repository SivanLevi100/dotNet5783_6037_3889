﻿using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Product
{
    /// <summary>
    /// Product ID number
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Name of Product
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Category of Product
    /// </summary>
    public Category Category { get; set; }
    /// <summary>
    /// Price of Product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// Amount in stock
    /// </summary>
    public int InStock { get; set; }

    /// <summary>
    /// Printing method
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
    Product Id: {Id}
    Name: {Name}
    category: {Category}
    Price: {Price}
    Amount in stock: {InStock}";


}
