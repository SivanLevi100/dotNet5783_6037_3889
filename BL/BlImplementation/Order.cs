using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlApi;
using BO;
using Dal;
using DalApi;
using DO;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    private IDal Dal = new DalList();

    public IEnumerable<BO.OrderForList> GetOrderList()
    {
        return Dal.Order.GetList().Select(order => new BO.OrderForList
        {
            OrderId = order.Id,
            CustomerName = order.CustomerName,
            Status = BO.OrderStatus.Ordered,
            AmountItems = Dal.OrderItem.Get(order.Id).Amount,
            TotalPrice = (Dal.OrderItem.Get(order.Id).Amount) * (Dal.Product.Get(Dal.OrderItem.Get(order.Id).ProductId).Price)
        });
    }
    public BO.Order GetProductDetails(int idOrder)
    {
        try
        {
            if (idOrder > 0)
            {
                OrderStatus status1 = new OrderStatus();
                DO.Order orderDO = Dal.Order.Get(idOrder);
                if (orderDO.OrderDate != DateTime.MinValue)//ההזמנה נוצרה
                {
                    status1 = OrderStatus.Ordered;
                }
                if (orderDO.ShipDate != DateTime.MinValue)//ההזמנה נשלחה
                {
                    status1 = OrderStatus.shipped;
                }
                if (orderDO.DeliveryDate != DateTime.MinValue)//ההזמנה נמסרה
                {
                    status1 = OrderStatus.delivered;
                }

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
        catch (NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("Order request failed", str);
        }

    }

    //public OrderTracking Tracking(int idOrder)
    //{
    //    throw new NotImplementedException();
    //}

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
        catch (NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("Order request failed", str);
        }

        if (flag == true) //  ההזמנה קיימת, נשלחה ולא נמסרה
        {
            DO.Order orderDo = new DO.Order(); //יצירת אוביקט שכבת נתנונים מעודכן
            orderDo.DeliveryDate = DateTime.Now;
            Dal.Order.Update(orderDo); //עדכון בשכבת הנתונים
            BO.Order order = new BO.Order
            {
                DeliveryDate = DateTime.Now,
                Status = BO.OrderStatus.delivered,
            };
            return order;
        }
        else
            throw new BO.NotExiestsExceptions("Order request failed");
    }

    //public BO.Order UpdateShipping(int idOrder)
    //{
    //    throw new NotImplementedException();
    //}

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
        catch (NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("Order request failed", str);
        }

        if (flag == true) //ההזמנה קיימת ועוד לא נשלחה
        {
            DO.Order orderDo = new DO.Order(); //יצירת אוביקט שכבת נתנונים מעודכן
            orderDo.ShipDate = DateTime.Now;
            Dal.Order.Update(orderDo); //עדכון בשכבת הנתונים
            BO.Order order = new BO.Order
            {
                ShipDate = DateTime.Now,
                Status = BO.OrderStatus.shipped,
            };
            return order;
        }
        else
            throw new BO.NotExiestsExceptions("Order request failed");
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
        catch (NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("Order request failed", str);
        }
        List<Tuple<DateTime, string>> track = new List<Tuple<DateTime, string>>();
        OrderStatus status1= new OrderStatus();
        if (orderTracking.OrderDate != DateTime.MinValue)//ההזמנה נוצרה
        {
            track.Add(Tuple.Create(orderTracking.OrderDate, "The order has been created"));
            status1 = OrderStatus.Ordered;
        }
        if (orderTracking.ShipDate != DateTime.MinValue)//ההזמנה נשלחה
        {
            track.Add(Tuple.Create(orderTracking.ShipDate, "The order has been sent"));
            status1 = OrderStatus.shipped;
        }
        if (orderTracking.DeliveryDate != DateTime.MinValue)//ההזמנה נמסרה
        {
            track.Add(Tuple.Create(orderTracking.DeliveryDate, "The order has been delivered"));
            status1 = OrderStatus.delivered;
        }
        return new BO.OrderTracking  
        {
            OrderId = idOrder,
            Status = status1,
            Tracking = track,
        };
    }

    //////
    /////////************************
    ///11111111111111111111111111111111
}
