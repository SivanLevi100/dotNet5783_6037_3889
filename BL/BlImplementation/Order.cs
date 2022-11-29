using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class Order: BlApi.IOrder
{ 
    private IDal Dal = new DalList();

    public IEnumerable<BO.Order> GetOrderList()
    {
        return Dal.Order.GetList().Select(order => new BO.OrderForList
        {
            OrderId = order.Id,
            CustomerName = order.CustomerName,
            Status = order.,
            AmountItems = Dal.OrderItem.Get(order.Id).Amount,
            TotalPrice = (Dal.OrderItem.Get(order.Id).Amount) * (Dal.Product.Get(Dal.OrderItem.Get(order.Id).ProductId).Price)
        }) ;
    }
    public BO.Order GetProductDetails(int idOrder)
    {

    }
    public BO.Order UpdateDelivery(int idOrder)
    {

    }
    public BO.Order UpdateShipping(int idOrder)
    {

    }
    public BO.OrderTracking Tracking(int idOrder)
    {

    }

}
