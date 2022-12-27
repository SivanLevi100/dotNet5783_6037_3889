using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

sealed internal class DalList : IDal
{
    private static IDal instance = new DalList();
    public static IDal Instance { get { return instance; } }
    private DalList()
    {
        Product = new DalProduct();
        Order = new DalOrder();
        OrderItem = new DalOrderItem();
    }

    public IProduct Product { get; }
    public IOrder Order { get; }
    public IOrderItem OrderItem { get; }


    //public IProduct Product => new DalProduct();
    //public IOrder Order => new DalOrder();
    //public IOrderItem OrderItem => new DalOrderItem();

}

