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
    private DalXml() { }

    public IProduct Product => new Dal.Product();
    public IOrder Order => new Dal.Order();
    public IOrderItem OrderItem => new Dal.OrderItem();

}
