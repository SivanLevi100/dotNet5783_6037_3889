using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    private IDal Dal = DalList.Instance;

    public IEnumerable<BO.OrderForList> GetOrderList()
    {
        BO.OrderStatus status1 = new BO.OrderStatus();
        foreach (DO.Order order in Dal.Order.GetAll())
        {
            if (order.OrderDate != null)//ההזמנה נוצרה
                status1 = BO.OrderStatus.Confirmed;
            if (order.ShipDate != null)//ההזמנה נשלחה
                status1 = BO.OrderStatus.shipped;
            if (order.DeliveryDate != null)//ההזמנה נמסרה
                status1 = BO.OrderStatus.delivered;

        }
        var orderList = Dal.Order.GetAll( item => item != null);
        foreach (DO.Order item in orderList)
        {
            yield return new BO.OrderForList
            {
                OrderId = item.Id,
                CustomerName = item.CustomerName,
                Status = status1,
                AmountItems = Dal?.OrderItem.GetAll(orderItem => orderItem.Value.Id == item.Id).Sum(orderItem => orderItem.Value.Amount) ?? throw new BO.NotExiestsExceptions("The OrderItem is not exiest in the order"),
                TotalPrice = Dal?.OrderItem.GetAll(orderItem => orderItem.Value.Id == item.Id).Sum(orderItem => orderItem.Value.Price * orderItem.Value.Amount) ?? throw new BO.NotExiestsExceptions("The OrderItem is not exiest in the order")
            };
        }
        //return orderList.Select( order => new BO.OrderForList
        //{
        //    OrderId = order.Id,
        //    CustomerName = order.CustomerName,
        //    Status = status1,
        //    AmountItems = Dal?.OrderItem.GetListOfOrderItemOfOrder(order.Id).Sum(orderItem => orderItem.Amount) ?? throw new BO.NotExiestsExceptions("The OrderItem is not exiest in the order"), 
        //    TotalPrice = Dal?.OrderItem.GetListOfOrderItemOfOrder(order.Id).Sum(orderItem => orderItem.Price * orderItem.Amount) ?? throw new BO.NotExiestsExceptions("The OrderItem is not exiest in the order")
        //});
    }

    public BO.Order GetOrderDetails(int idOrder)
    {
        try
        {
            if (idOrder > 0)
            {
                BO.OrderStatus status1 = new BO.OrderStatus();
                DO.Order orderDO = Dal.Order.Get(idOrder);
                if (orderDO.OrderDate != null && orderDO.ShipDate == null && orderDO.DeliveryDate == null)//ההזמנה נוצרה
                    status1 = BO.OrderStatus.Confirmed;
                if (orderDO.ShipDate != null && orderDO.DeliveryDate == null)//ההזמנה נשלחה
                    status1 = BO.OrderStatus.shipped;
                if (orderDO.DeliveryDate != null)//ההזמנה נמסרה
                    status1 = BO.OrderStatus.delivered;
                double sumOfPrices = 0;
                IEnumerable<DO.OrderItem?>? orderItemListDo = Dal.OrderItem.GetAll(orderitem => orderitem?/*Value*/.OrderId == idOrder);
                foreach (DO.OrderItem orderItem in orderItemListDo)
                {
                    sumOfPrices += orderItem.Price * orderItem.Amount;
                }
                //DO.Order orderDO = Dal.Order.Get(idOrder);
                BO.Order order = new BO.Order
                {
                    Id = orderDO.Id,
                    CustomerName = orderDO.CustomerName,
                    CustomerAdress = orderDO.CustomerAdress,
                    CustomerEmail = orderDO.CustomerEmail,
                    OrderDate = orderDO.OrderDate,
                    ShipDate = orderDO.ShipDate,
                    DeliveryDate = orderDO.DeliveryDate,
                    Status = status1,
                    TotalPrice = sumOfPrices

                };
                return order;
            }
            else
                throw new BO.NotExiestsExceptions("Order request failed");
        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("Order request failed", str);
        }

    }

    public BO.Order UpdateDelivery(int idOrder)//עדכון אספקת הזמנה
    {
        //List<BO.OrderItem?>? listBo = new List<BO.OrderItem?>();
        //foreach (DO.OrderItem doOrderItem in Dal.OrderItem.GetListOfOrderItemOfOrder(idOrder))
        //{
        //    listBo.Add(new BO.OrderItem()
        //    {
        //        Id = doOrderItem.Id,
        //        ProductId = doOrderItem.ProductId,
        //        Price = doOrderItem.Price,
        //        AmountInOrder = doOrderItem.Amount,
        //        TotalPriceOfItem = doOrderItem.Price * doOrderItem.Amount,
        //    });
        //}

        if (idOrder <= 0)
            throw new BO.IncorrectDataExceptions("Order id is incorrect");

        bool flag = false;
        try
        {
            IEnumerable<DO.Order?> orderListDo = Dal.Order.GetAll();
            foreach (DO.Order order in orderListDo)
            {
                if (order.Id == idOrder && order.ShipDate != null && order.DeliveryDate == null)//תבדוק האם הזמנה קיימת (בשכבת נתונים), כבר נשלחה אך עוד לא סופקה
                    flag = true;
            }
        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("Order request failed", str);
        }

        try
        {
            if (flag == true) //  ההזמנה קיימת, נשלחה ולא נמסרה
            {

                DO.Order orderDo = Dal.Order.Get(idOrder);
                orderDo.DeliveryDate = DateTime.Now;
                Dal.Order.Update(orderDo); //עדכון בשכבת הנתונים

                BO.Order order = new BO.Order  //עדכון ביישות הלוגית
                {
                    Id= idOrder,
                    CustomerName= orderDo.CustomerName,
                    CustomerAdress= orderDo.CustomerAdress,
                    CustomerEmail= orderDo.CustomerEmail,
                    OrderDate= orderDo.OrderDate,
                    ShipDate= orderDo.ShipDate,
                    DeliveryDate = DateTime.Now,
                    Status = BO.OrderStatus.delivered,
                    OrdersItemsList = /*listBo*/ (List<BO.OrderItem?>?)Dal.OrderItem.GetAll(orderItem => orderItem.Value.OrderId == idOrder),
                    TotalPrice= Dal.OrderItem.GetAll(orderItem => orderItem.Value.OrderId== idOrder).Sum(orderItem => orderItem.Value.Price * orderItem.Value.Amount)
                };
                return order;
            }
            else
                throw new BO.NotExiestsExceptions("The Order request faile");
        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("Order request failed", str);
        }
    }


    
    public BO.Order UpdateShipping(int idOrder)//עדכון שילוח הזמנה
    {
        if (idOrder <= 0)
            throw new BO.IncorrectDataExceptions("Order id is incorrect");

        bool flag = false;
        try
        {
            IEnumerable<DO.Order?>? orderListDo = Dal.Order.GetAll();
            foreach (DO.Order order in orderListDo)
            {
                if (order.Id == idOrder && order.ShipDate == null)//הזמנה קיימת (בשכבת נתונים) ועוד לא נשלחה
                    flag = true;
            }
        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("Order request failed", str);
        }
        try
        {
            if (flag == true) //ההזמנה קיימת ועוד לא נשלחה
            {
                DO.Order orderDo = Dal.Order.Get(idOrder);
                orderDo.ShipDate = DateTime.Now;
                Dal.Order.Update(orderDo); //עדכון בשכבת הנתונים

                BO.Order order = new BO.Order  //עדכון ביישות הלוגית
                {
                    Id = idOrder,
                    CustomerName = orderDo.CustomerName,
                    CustomerAdress = orderDo.CustomerAdress,
                    CustomerEmail = orderDo.CustomerEmail,
                    OrderDate = orderDo.OrderDate,
                    ShipDate = DateTime.Now,
                    DeliveryDate = orderDo.DeliveryDate,
                    Status = BO.OrderStatus.shipped,
                    OrdersItemsList = (List<BO.OrderItem?>)Dal.OrderItem.GetAll(orderItem => orderItem.Value.OrderId == idOrder),
                    TotalPrice = Dal.OrderItem.GetAll(orderItem => orderItem.Value.OrderId == idOrder).Sum(orderItem => orderItem.Value.Price * orderItem.Value.Amount)
                };
                return order;
            }
            else
                throw new BO.NotExiestsExceptions("Order request failed");

        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("Order request failed", str);
        }
    }
  
    public BO.OrderTracking Tracking(int idOrder)//מעקב הזמנה
    {
        if (idOrder <= 0)
            throw new BO.IncorrectDataExceptions("Order id is incorrect");

        DO.Order orderTracking = new DO.Order();
        try
        {
            orderTracking = Dal.Order.Get(idOrder);
        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("Order request failed", str);
        }
        List<Tuple<DateTime?, string>> track = new List<Tuple<DateTime?, string>>();
        BO.OrderStatus status1= new BO.OrderStatus();
        if (orderTracking.OrderDate != null)//ההזמנה נוצרה
        {
            track.Add(Tuple.Create(orderTracking.OrderDate, "The order has been created"));
            status1 = BO.OrderStatus.Confirmed;
        }
        if (orderTracking.ShipDate != null)//ההזמנה נשלחה
        {
            track.Add(Tuple.Create(orderTracking.ShipDate, "The order has been sent"));
            status1 = BO.OrderStatus.shipped;
        }
        if (orderTracking.DeliveryDate != null)//ההזמנה נמסרה
        {
            track.Add(Tuple.Create(orderTracking.DeliveryDate, "The order has been delivered"));
            status1 = BO.OrderStatus.delivered;
        }
        return new BO.OrderTracking  
        {
            OrderId = idOrder,
            Status = status1,
            Tracking = track,
        };
    }

}
