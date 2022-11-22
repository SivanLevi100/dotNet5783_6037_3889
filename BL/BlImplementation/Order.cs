using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlApi;
//using BO;
using Dal;
using DalApi;

namespace BlImplementation;

internal class Order:IOrder
{
    private IDal Dal = new DalList();


    public IEnumerable<Order> GetOrderList()
    {

    }
    public Order GetProductDetails(int id)
    {

    }
    public Order UpdateDelivery(Order order1)
    {

    }
    public Order UpdateShipping(Order order1)
    {

    }
    public OrderTracking Tracking(int id)
    {

    }

}
