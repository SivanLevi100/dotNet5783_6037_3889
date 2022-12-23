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
                if (orderDO.OrderDate != null&& orderDO.ShipDate != null && orderDO.DeliveryDate == null)//ההזמנה נשלחה
                    status1 = BO.OrderStatus.shipped;
                if (orderDO.OrderDate != null&& orderDO.ShipDate != null && orderDO.DeliveryDate != null)//ההזמנה נמסרה
                    status1 = BO.OrderStatus.delivered;
                double sumOfPrices = 0;
                IEnumerable<DO.OrderItem?>? orderItemListDo = Dal.OrderItem.GetAll(orderitem => orderitem?/*Value*/.OrderId == idOrder);
                foreach (DO.OrderItem orderItem in orderItemListDo)
                {
                    sumOfPrices += orderItem.Price * orderItem.Amount;
                }

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
        DO.Order orderDO;
        if (idOrder <= 0)
            throw new BO.IncorrectDataExceptions("Order id is incorrect");
        try
        {
            orderDO=Dal.Order.Get(idOrder);
            if (orderDO.DeliveryDate == null)//ההזמנה לא נמסרה
            {
                orderDO.DeliveryDate = DateTime.Now;
                Dal.Order.Update(orderDO); //עדכון בשכבת הנתונים
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
                    OrdersItemsList = getDOlistOfOrderItem(idOrder),
                    TotalPrice = Dal.OrderItem.GetAll(orderItem => orderItem.Value.OrderId == idOrder).Sum(orderItem => orderItem.Value.Price * orderItem.Value.Amount)
                };
                return orderBO;
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
        DO.Order orderDO;
        if (idOrder <= 0)
            throw new BO.IncorrectDataExceptions("Order id is incorrect");
        try
        {
            orderDO = Dal.Order.Get(idOrder);
            if (orderDO.ShipDate == null)//ההזמנה לא נמסרה
            {
                orderDO.ShipDate = DateTime.Now;
                Dal.Order.Update(orderDO); //עדכון בשכבת הנתונים
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
                    OrdersItemsList = getDOlistOfOrderItem(idOrder),
                    TotalPrice = Dal.OrderItem.GetAll(orderItem => orderItem.Value.OrderId == idOrder).Sum(orderItem => orderItem.Value.Price * orderItem.Value.Amount)
                };
                return orderBO;
            }
            else
                throw new BO.NotExiestsExceptions("The Order request faile");
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
        //List<Tuple<DateTime?, string?>>? track = new();
        List <Tuple<DateTime?, string>> track = new List<Tuple<DateTime?, string>>();
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

    //Helper function to return a list of order details
    public List<BO.OrderItem?>? getDOlistOfOrderItem(int id)
    {
        List<BO.OrderItem?>? listBo=new List<BO.OrderItem>();
        foreach(DO.OrderItem doOrderItem in Dal.OrderItem.GetAll(orderItem=>orderItem.Value.OrderId==id))
        {
            listBo.Add(new BO.OrderItem
            {
                Id = doOrderItem.Id,
                NameProduct=Dal.Product.Get(doOrderItem.Id).Name,
                ProductId = doOrderItem.ProductId,
                Price = doOrderItem.Price,
                AmountInOrder = doOrderItem.Amount,
                TotalPriceOfItem = doOrderItem.Price * doOrderItem.Amount,

            });
        }
        return listBo;
    }

}
