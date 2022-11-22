using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public enum Category
{
    Unavailable = 0,
    Kitchen, //kitchen products
    Computer,//Screens and computers
    Cooling, //Air conditioners and fans
    Cleaning, //Cleaning and washing machines
    Refrigerator //Refrigerators and freezers
}


public enum OrderStatus
{
    Initiated,
    Ordered,
    Paid,
    Shipped,
    Delivered
}