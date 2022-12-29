using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using BlApi;
//using BO;
using DalApi;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    private DalApi.IDal? Dal = DalApi.Factory.Get();


    public IEnumerable<BO.OrderForList> GetOrderList()
    {

        try
        {
            return from item in Dal?.Order.GetAll()
                   where item != null
                   let order = ((DO.Order)item)!
                   let listOrderItems = Dal!.OrderItem.GetAll(o => ((DO.OrderItem)o!).OrderId == order.Id).Cast<DO.OrderItem>()
                   select new BO.OrderForList
                   {
                       OrderId = order.Id,
                       CustomerName = order.CustomerName,
                       AmountItems = (from orderitem in listOrderItems select orderitem).Count(),
                       /*(from orderitem in listOrderItems select orderitem).Sum(or => or.Amount)*/
                       TotalPrice = listOrderItems.Sum(orderItem => orderItem.Amount * orderItem.Price),
                       Status = statusFromDate(order)
                   };

        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("Order request failed", str);
        }

    }

    public BO.Order GetOrderDetails(int idOrder)
    {
        return new BO.Order();

        //if (idOrder < 0) throw new BO.IncorrectDataExceptions("id order is invalid");
        //try
        //{
        //    return from item in Dal?.Order.GetAll()
        //           where item != null
        //           let order = ((DO.Order)item)!
        //           let listOrderItems = Dal.OrderItem.GetAll(orderitem => orderitem?.OrderId == idOrder).Cast<DO.OrderItem>()
        //           select new BO.Order
        //           {
        //               Id = order.Id,
        //               CustomerName = order.CustomerName,
        //               CustomerAdress = order.CustomerAdress,
        //               CustomerEmail = order.CustomerEmail,
        //               OrderDate = order.OrderDate,
        //               ShipDate = order.ShipDate,
        //               DeliveryDate = order.DeliveryDate,
        //               Status = statusFromDate(order),
        //               TotalPrice = listOrderItems.Sum(orderItem => orderItem.Amount * orderItem.Price),
        //               OrdersItemsList = //getBOlistOfOrderItem()
        //           };
        //}
        //catch (DO.NotFoundExceptions str)
        //{
        //    throw new BO.NotExiestsExceptions("Order request failed", str);
        //}
    }

    public BO.Order UpdateDelivery(int idOrder)//Order delivery update
    {
        DO.Order orderDO;
        if (idOrder <= 0) throw new BO.IncorrectDataExceptions("Order id is incorrect");
        try
        {
            orderDO = Dal?.Order.Get(idOrder) ?? throw new BO.NotExiestsExceptions("The Order is not exiests");
            if (orderDO.OrderDate != null && orderDO.DeliveryDate == null && orderDO.ShipDate != null) //The order was not delivered
            {
                orderDO.DeliveryDate = DateTime.Now;
                Dal.Order.Update(orderDO); //Update in the data layer
                BO.Order orderBO = new BO.Order
                {
                    Id = orderDO.Id,
                    CustomerName = orderDO.CustomerName,
                    CustomerAdress = orderDO.CustomerAdress,
                    CustomerEmail = orderDO.CustomerEmail,
                    OrderDate = orderDO.OrderDate,
                    ShipDate = orderDO.ShipDate,
                    DeliveryDate = DateTime.Now,
                    Status = BO.OrderStatus.delivered,
                    OrdersItemsList = getBOlistOfOrderItem(idOrder),
                    TotalPrice = Dal.OrderItem.GetAll(orderItem => ((DO.OrderItem)orderItem!).OrderId == idOrder)
                    .Sum(orderItem => ((DO.OrderItem)orderItem!).Price * ((DO.OrderItem)orderItem!).Amount)
                };
                return orderBO;
            }
            else
                throw new BO.NotExiestsExceptions("The order has not been sent so it is not possible to update order delivery\n");
        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("Order request failed", str);
        }
    }


    public BO.Order UpdateShipping(int idOrder)//Order shipping update
    {
        DO.Order orderDO;
        if (idOrder <= 0)
            throw new BO.IncorrectDataExceptions("Order id is incorrect");
        try
        {
            orderDO = Dal?.Order.Get(idOrder) ?? throw new BO.NotExiestsExceptions("The Order is not exiests");
            if (orderDO.OrderDate != null && orderDO.ShipDate == null && orderDO.DeliveryDate == null)//The order was not sent
            {
                orderDO.ShipDate = DateTime.Now;
                Dal?.Order.Update(orderDO); //Update in the data layer
                BO.Order orderBO = new BO.Order
                {
                    Id = orderDO.Id,
                    CustomerName = orderDO.CustomerName,
                    CustomerAdress = orderDO.CustomerAdress,
                    CustomerEmail = orderDO.CustomerEmail,
                    OrderDate = orderDO.OrderDate,
                    ShipDate = DateTime.Now,
                    DeliveryDate = orderDO.DeliveryDate,
                    Status = BO.OrderStatus.shipped,
                    OrdersItemsList = getBOlistOfOrderItem(idOrder),
                    TotalPrice = Dal?.OrderItem.GetAll(orderItem => ((DO.OrderItem)orderItem!).OrderId == idOrder)
                    .Sum(orderItem => ((DO.OrderItem)orderItem!).Price * ((DO.OrderItem)orderItem!).Amount)
                    ?? throw new BO.NotExiestsExceptions("The Order is not exiests")
                };

                return orderBO;
            }
            else
                throw new BO.NotExiestsExceptions("The order has been delivered so it is not possible to update shipping\n");
        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("Order request failed", str);
        }
    }

    public BO.OrderTracking Tracking(int idOrder) //Order Tracking
    {
        if (idOrder <= 0)
            throw new BO.IncorrectDataExceptions("Order id is incorrect");

        DO.Order order = new DO.Order();
        try
        {
            order = Dal?.Order.Get(idOrder) ?? throw new BO.NotExiestsExceptions("The Order is not exiests");
        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("Order request failed", str);
        }
        List<Tuple<DateTime?, string>> track = new List<Tuple<DateTime?, string>>();
        if (order.DeliveryDate != null)//The order has been delivered
        {
            track.Add(Tuple.Create(order.DeliveryDate, "The order has been delivered"));
        }
        if (order.ShipDate != null)//The order has been sent
        {
            track.Add(Tuple.Create(order.ShipDate, "The order has been sent"));
        }
        if (order.OrderDate != null)//The order has been created
        {
            track.Add(Tuple.Create(order.OrderDate, "The order has been created"));
        }

        return new BO.OrderTracking
        {
            OrderId = idOrder,
            Status = statusFromDate(order),
            Tracking = track,
        };
    }


    //Helper function to return a list of order details
    private List<BO.OrderItem?>? getBOlistOfOrderItem(int id)
    {

        List<BO.OrderItem?>? listBo = new();

        foreach (DO.OrderItem doOrderItem in Dal?.OrderItem.GetAll(orderItem => orderItem.Value.OrderId == id) ?? throw new BO.NotExiestsExceptions("The Order is not exiests"))
        {
            listBo.Add(new BO.OrderItem
            {
                Id = doOrderItem.Id,
                NameProduct = Dal.Product.Get(doOrderItem.Id).Value.Name,
                ProductId = doOrderItem.ProductId,
                Price = doOrderItem.Price,
                AmountInOrder = doOrderItem.Amount,
                TotalPriceOfItem = doOrderItem.Price * doOrderItem.Amount,

            });
        }
        return listBo;
    }

    private BO.OrderStatus statusFromDate(DO.Order order)
    {

        if (order.DeliveryDate != null) //The order has been delivered
            return BO.OrderStatus.delivered;

        if (order.ShipDate != null) //The order has been sent
            return BO.OrderStatus.shipped;

        if (order.OrderDate != null) //The order has been created
            return BO.OrderStatus.Confirmed;

        return BO.OrderStatus.Unknown;
    }


}
