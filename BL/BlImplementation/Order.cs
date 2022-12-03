﻿using System;
using System.Collections.Generic;
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
                double sumOfPrices = 0;
                IEnumerable<DO.OrderItem> orderItemListDo = Dal.OrderItem.GetListOfOrderItemOfOrder(idOrder);
                foreach (DO.OrderItem orderItem in orderItemListDo)
                {
                    sumOfPrices += orderItem.Price * orderItem.Amount;
                }
                DO.Order orderDO = Dal.Order.Get(idOrder);
                BO.Order order = new BO.Order
                {
                    Id = orderDO.Id,
                    CustomerName = orderDO.CustomerName,
                    CustomerAdress = orderDO.CustomerAdress,
                    CustomerEmail = orderDO.CustomerEmail,
                    OrderDate = orderDO.OrderDate,
                    ShipDate = orderDO.ShipDate,
                    DeliveryDate = orderDO.DeliveryDate,
                    Status = BO.OrderStatus.Ordered,
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

    public OrderTracking Tracking(int idOrder)
    {
        throw new NotImplementedException();






    }

    public BO.Order UpdateDelivery(int idOrder)
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

    public BO.Order UpdateShipping(int idOrder)
    {
        throw new NotImplementedException();
    }

    public BO.OrderTracking Tracking(int idOrder)
    {

    }


}
