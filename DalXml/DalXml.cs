using DalApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

sealed internal class DalXml : IDal
{
    private static IDal instance = new DalXml();
    public static IDal Instance { get { return instance; } }
    private DalXml() 
    {
        Product= new Dal.Product();
        Order =new Dal.Order();
        OrderItem = new Dal.OrderItem();
    }

    public IProduct Product { get; }
    public IOrder Order { get; }
    public IOrderItem OrderItem { get; }

}
