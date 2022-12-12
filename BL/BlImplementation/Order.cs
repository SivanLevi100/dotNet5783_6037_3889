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
        foreach (DO.Order order in Dal.Order.GetList())
        {
            if (order.OrderDate != DateTime.MinValue)//ההזמנה נוצרה
                status1 = BO.OrderStatus.Confirmed;
            if (order.ShipDate != DateTime.MinValue)//ההזמנה נשלחה
                status1 = BO.OrderStatus.shipped;
            if (order.DeliveryDate != DateTime.MinValue)//ההזמנה נמסרה
                status1 = BO.OrderStatus.delivered;

        }
        return Dal.Order.GetList().Select(order => new BO.OrderForList
        {
            OrderId = order.Id,
            CustomerName = order.CustomerName,
            Status = status1,
            AmountItems = Dal?.OrderItem.GetListOfOrderItemOfOrder(order.Id).Sum(orderItem => orderItem.Amount) ?? throw new BO.NotExiestsExceptions("The OrderItem is not exiest in the order"), 
            TotalPrice = Dal?.OrderItem.GetListOfOrderItemOfOrder(order.Id).Sum(orderItem => orderItem.Price * orderItem.Amount) ?? throw new BO.NotExiestsExceptions("The OrderItem is not exiest in the order")

        });
    }

    public BO.Order GetOrderDetails(int idOrder)
    {
        try
        {
            if (idOrder > 0)
            {
                BO.OrderStatus status1 = new BO.OrderStatus();
                DO.Order orderDO = Dal.Order.Get(idOrder);
                if (orderDO.OrderDate != DateTime.MinValue&& orderDO.ShipDate == DateTime.MinValue&& orderDO.DeliveryDate == DateTime.MinValue)//ההזמנה נוצרה
                    status1 = BO.OrderStatus.Confirmed;
                if (orderDO.ShipDate != DateTime.MinValue && orderDO.DeliveryDate == DateTime.MinValue)//ההזמנה נשלחה
                    status1 = BO.OrderStatus.shipped;
                if (orderDO.DeliveryDate != DateTime.MinValue)//ההזמנה נמסרה
                    status1 = BO.OrderStatus.delivered;
                double sumOfPrices = 0;
                IEnumerable<DO.OrderItem> orderItemListDo = Dal.OrderItem.GetListOfOrderItemOfOrder(idOrder);
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
        if (idOrder <= 0)
            throw new BO.IncorrectDataExceptions("Order id is incorrect");

        bool flag = false;
        try
        {
            IEnumerable<DO.Order> orderListDo = Dal.Order.GetList();
            foreach (DO.Order order in orderListDo)
            {
                if (order.Id == idOrder && order.ShipDate != DateTime.MinValue && order.DeliveryDate == DateTime.MinValue)//תבדוק האם הזמנה קיימת (בשכבת נתונים), כבר נשלחה אך עוד לא סופקה
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
                    OrdersItemsList = (List<BO.OrderItem?>?)Dal.OrderItem.GetListOfOrderItemOfOrder(idOrder),////////זורק חריגת מערכת בגלל ההמרה
                    TotalPrice= Dal.OrderItem.GetListOfOrderItemOfOrder(idOrder).Sum(orderItem=>orderItem.Price*orderItem.Amount)

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
            IEnumerable<DO.Order> orderListDo = Dal.Order.GetList();
            foreach (DO.Order order in orderListDo)
            {
                if (order.Id == idOrder && order.ShipDate == DateTime.MinValue)//הזמנה קיימת (בשכבת נתונים) ועוד לא נשלחה
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
                    OrdersItemsList = (List<BO.OrderItem?>)Dal.OrderItem.GetListOfOrderItemOfOrder(idOrder),
                    TotalPrice = Dal.OrderItem.GetListOfOrderItemOfOrder(idOrder).Sum(orderItem => orderItem.Price * orderItem.Amount)

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
        List<Tuple<DateTime, string>> track = new List<Tuple<DateTime, string>>();
        BO.OrderStatus status1= new BO.OrderStatus();
        if (orderTracking.OrderDate != DateTime.MinValue)//ההזמנה נוצרה
        {
            track.Add(Tuple.Create(orderTracking.OrderDate, "The order has been created"));
            status1 = BO.OrderStatus.Confirmed;
        }
        if (orderTracking.ShipDate != DateTime.MinValue)//ההזמנה נשלחה
        {
            track.Add(Tuple.Create(orderTracking.ShipDate, "The order has been sent"));
            status1 = BO.OrderStatus.shipped;
        }
        if (orderTracking.DeliveryDate != DateTime.MinValue)//ההזמנה נמסרה
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
