using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlApi;
using BO;
using DalApi;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    private DalApi.IDal? Dal = DalApi.Factory.Get();

    public IEnumerable<BO.OrderForList> GetOrderList()
    {
        BO.OrderStatus status1 = new BO.OrderStatus();

       // var status2 = from order in Dal.Order.GetAll()

        foreach (DO.Order? order in Dal?.Order.GetAll() ?? throw new BO.NotExiestsExceptions("The Order is not exiests")) 
        {
            if (order?.OrderDate != null) //The order has been created
                status1 = BO.OrderStatus.Confirmed;
            if (order?.ShipDate != null) //The order has been sent
                status1 = BO.OrderStatus.shipped;
            if (order?.DeliveryDate != null) //The order has been delivered
                status1 = BO.OrderStatus.delivered;

        }
        return Dal?.Order.GetAll().Select(item => new BO.OrderForList
        {
            OrderId = item?.Id ?? throw new BO.NotExiestsExceptions("The Order is not exiests"),
            CustomerName = item?.CustomerName,
            Status = status1,
            AmountItems = Dal?.OrderItem.GetAll(orderItem => orderItem.Value.Id == item?.Id).Sum(orderItem => orderItem.Value.Amount) ?? throw new BO.NotExiestsExceptions("The OrderItem is not exiest in the order"),
            TotalPrice = Dal?.OrderItem.GetAll(orderItem => orderItem.Value.Id == item?.Id).Sum(orderItem => orderItem.Value.Price * orderItem.Value.Amount) ?? throw new BO.NotExiestsExceptions("The OrderItem is not exiest in the order")

        }) ?? throw new BO.NotExiestsExceptions("The Order is not exiests");


        //var orderList = Dal?.Order.GetAll( item => item != null) ?? throw new BO.NotExiestsExceptions("The Order is not exiests");
        //foreach (DO.Order? item in orderList)
        //{
        //    yield return new BO.OrderForList
        //    {
        //        OrderId = item?.Id ?? throw new BO.NotExiestsExceptions("The Order is not exiests"),
        //        CustomerName = item?.CustomerName,
        //        Status = status1,
        //        AmountItems = Dal?.OrderItem.GetAll(orderItem => orderItem.Value.Id == item?.Id).Sum(orderItem => orderItem.Value.Amount) ?? throw new BO.NotExiestsExceptions("The OrderItem is not exiest in the order"),
        //        TotalPrice = Dal?.OrderItem.GetAll(orderItem => orderItem.Value.Id == item?.Id).Sum(orderItem => orderItem.Value.Price * orderItem.Value.Amount) ?? throw new BO.NotExiestsExceptions("The OrderItem is not exiest in the order")
        //    };
        //}


    }

    public BO.Order GetOrderDetails(int idOrder)
    {
        try
        {
            if (idOrder > 0)
            {
                BO.OrderStatus status1 = new BO.OrderStatus();
                DO.Order orderDO = Dal?.Order.Get(idOrder) ?? throw new BO.NotExiestsExceptions("The Order is not exiests");  
                if (orderDO.OrderDate != null && orderDO.ShipDate == null && orderDO.DeliveryDate == null)//ההזמנה נוצרה
                    status1 = BO.OrderStatus.Confirmed;
                if (orderDO.OrderDate != null&& orderDO.ShipDate != null && orderDO.DeliveryDate == null)//ההזמנה נשלחה
                    status1 = BO.OrderStatus.shipped;
                if (orderDO.OrderDate != null&& orderDO.ShipDate != null && orderDO.DeliveryDate != null)//ההזמנה נמסרה
                    status1 = BO.OrderStatus.delivered;
                double sumOfPrices = 0;
                IEnumerable<DO.OrderItem?>? orderItemListDo = Dal.OrderItem.GetAll(orderitem =>orderitem.Value.OrderId == idOrder);
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
                    TotalPrice = sumOfPrices,
                    OrdersItemsList = getDOlistOfOrderItem(orderDO.Id)
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

    public BO.Order UpdateDelivery(int idOrder)//Order delivery update
    {
        DO.Order orderDO;
        if (idOrder <= 0)
            throw new BO.IncorrectDataExceptions("Order id is incorrect");
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


    public BO.Order UpdateShipping(int idOrder)//Order shipping update
    {
        DO.Order orderDO;
        if (idOrder <= 0)
            throw new BO.IncorrectDataExceptions("Order id is incorrect");
        try
        {
            orderDO = Dal?.Order.Get(idOrder) ?? throw new BO.NotExiestsExceptions("The Order is not exiests"); 
            if (orderDO.OrderDate != null && orderDO.ShipDate == null &&orderDO.DeliveryDate == null)//The order was not sent
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
                    OrdersItemsList = getDOlistOfOrderItem(idOrder),
                    TotalPrice = Dal?.OrderItem.GetAll(orderItem => orderItem.Value.OrderId == idOrder).Sum(orderItem => orderItem.Value.Price * orderItem.Value.Amount) ?? throw new BO.NotExiestsExceptions("The Order is not exiests")  
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
  
    public BO.OrderTracking Tracking(int idOrder) //Order Tracking
    {
        if (idOrder <= 0)
            throw new BO.IncorrectDataExceptions("Order id is incorrect");

        DO.Order orderTracking = new DO.Order();
        try
        {
            orderTracking = Dal?.Order.Get(idOrder) ?? throw new BO.NotExiestsExceptions("The Order is not exiests"); 
        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("Order request failed", str);
        }
        //List<Tuple<DateTime?, string?>>? track = new();
        List<Tuple<DateTime?, string>> track = new List<Tuple<DateTime?, string>>();
        BO.OrderStatus status1= new BO.OrderStatus();
        if (orderTracking.OrderDate != null)//The order has been created
        {
            track.Add(Tuple.Create(orderTracking.OrderDate, "The order has been created"));
            status1 = BO.OrderStatus.Confirmed;
        }
        if (orderTracking.ShipDate != null)//The order has been sent
        {
            track.Add(Tuple.Create(orderTracking.ShipDate, "The order has been sent"));
            status1 = BO.OrderStatus.shipped;
        }
        if (orderTracking.DeliveryDate != null)//The order has been delivered
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
        foreach (DO.OrderItem doOrderItem in Dal?.OrderItem.GetAll(orderItem => orderItem.Value.OrderId == id) ?? throw new BO.NotExiestsExceptions("The Order is not exiests"))
        {
            listBo.Add(new BO.OrderItem
            {
                Id = doOrderItem.Id,
                NameProduct=Dal.Product.Get(doOrderItem.Id).Value.Name,
                ProductId = doOrderItem.ProductId,
                Price = doOrderItem.Price,
                AmountInOrder = doOrderItem.Amount,
                TotalPriceOfItem = doOrderItem.Price * doOrderItem.Amount,

            });
        }
        return listBo;
    }

}
