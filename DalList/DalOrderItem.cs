
using DalApi;
using DO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Dal;

public class DalOrderItem
{
    /// <summary>
    /// An add object method that receives an object of an order item and returns the ID number of the added order item
    /// </summary>
    /// <param name="orderItem1"></param>
    /// <returns></returns>
    public int addOrderItems(OrderItem orderItem1)
    {
        if (DataSource.orderItemList.Exists(x => x.Id == orderItem1.Id))
        {
            throw new DuplicateIdExceptions("no place in List to add");
        }
        DataSource.orderItemList.Add(orderItem1);
        return orderItem1.Id;
    }

    /// <summary>
    /// A request/call method of a single object that receives an order item ID number and returns the appropriate order item
    /// </summary>
    /// <param name="idOrderItem1"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundExceptions"></exception>
    public OrderItem getOrderItem(int idOrderItem1)
    {
        if (DataSource.orderItemList.Exists(x => x.Id == idOrderItem1))
            return DataSource.orderItemList.Find(x => x.Id == idOrderItem1);
        throw new NotFoundExceptions("the orderItem id is not exist in List");
    }

    /// <summary>
    /// A request/read method of the list of all order item objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem> /*List<OrderItem>*/ getArrayOfOrderItem()
    {
        return DataSource.orderItemList.ToList();

    }

    /// <summary>
    /// A method to delete an order items object that receives an order item ID number
    /// </summary>
    /// <param name="idOrderItem1"></param>
    public void deleteOrderItem(int idOrderItem1)
    {
        if (DataSource.orderItemList.Exists(x => x.Id == idOrderItem1))
        {
            OrderItem orderItemDelete = DataSource.orderItemList.Find(x => x.Id == idOrderItem1);
            DataSource.orderItemList.Remove(orderItemDelete);
            return;
        }
        throw new NotFoundExceptions("The orderItem is not exist in the List");
    }



    /// <summary>
    /// An object update method that will receive a new order item
    /// </summary>
    /// <param name="orderItem1"></param>
    public void updateOredrItem(OrderItem orderItem1)
    {
        if (DataSource.orderItemList.Exists(x => x.Id == orderItem1.Id))
        {
            int j = DataSource.orderItemList.IndexOf(DataSource.orderItemList.Find(x => x.Id == orderItem1.Id));
            DataSource.orderItemList[j] = orderItem1;
            return;
        }
        throw new NotFoundExceptions("the orderItem id is not exist in List");
    }


    /// <summary>
    /// Request/call method based on two identifiers (ID) - product ID and order ID,
    /// the method returns the object of an item in the corresponding order
    /// </summary>
    /// <param name="idOrderItem1"></param>
    /// <param name="idOrderItem2"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundExceptions"></exception>
    public OrderItem getOrderItemofTwoId(int idOrderItem1, int idOrderItem2)
    {
        if (DataSource.orderItemList.Exists(x => x.OrderId == idOrderItem1 && x.ProductId == idOrderItem2))
            return DataSource.orderItemList.Find(x => x.OrderId == idOrderItem1 && x.ProductId == idOrderItem2);
        throw new NotFoundExceptions("the orderItem id is not exist in List");
    }

    /// <summary>
    /// Method of request/reading of a list/array of order details according to the ID number of an order
    /// </summary>
    /// <param name="myOrderId"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundExceptions"></exception>
    public IEnumerable<OrderItem>/*List<OrderItem>*/ getArrayOfOrderItemOfOrder(int myOrderId)
    {
        if(DataSource.orderItemList.Exists(x => x.OrderId == myOrderId))
        {
            return DataSource.orderItemList.FindAll(x => x.OrderId == myOrderId).ToList();
        }
        throw new Exception("the order is not exist in List");
    }


}
