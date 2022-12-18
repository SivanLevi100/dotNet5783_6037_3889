
using DalApi;
using DO;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Runtime.CompilerServices;

namespace Dal;

internal class DalOrderItem : IOrderItem
{

    DataSource _dstaSource = DataSource.S_instance;

    /// <summary>
    /// An add object method that receives an object of an order item and returns the ID number of the added order item
    /// </summary>
    /// <param name="orderItem1"></param>
    /// <returns></returns>
    public int Add(OrderItem orderItem1)
    {
        if (_dstaSource.OrderItemList.Exists(x => x?.Id == orderItem1.Id))
            throw new DuplicateIdExceptions("no place in List to add");
        _dstaSource.OrderItemList.Add(orderItem1);
        return orderItem1.Id;
    }

    /// <summary>
    /// A request/call method of a single object that receives an order item ID number and returns the appropriate order item
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundExceptions"></exception>
    public OrderItem Get(int id)
    {
        foreach (OrderItem orderItem in _dstaSource.OrderItemList)
        {
            if (orderItem.Id == id)
                return orderItem;
        }
        throw new NotFoundExceptions("The orderItem id is not exist in List");
    }

    public OrderItem GetF(Func<OrderItem?, bool>? filter)
    {
        foreach (OrderItem orderItem in _dstaSource.OrderItemList)
        {
            if (filter(orderItem))
                return orderItem;
        }
        throw new NotFoundExceptions("The orderItem is not exist in List");
    }




    /// <summary>
    /// A request/read method of the list of all order item objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter) =>
        (filter == null ? _dstaSource.OrderItemList?.Select(item => item) 
        : _dstaSource.OrderItemList?.Where(item => filter(item)) 
        ?? throw new DoesNotExistException("Missing orderitem"))
        ?? throw new DataCorruptionException("Missing ordritem list");



    /// <summary>
    /// A method to delete an order items object that receives an order item ID number
    /// </summary>
    /// <param name="idOrderItem1"></param>
    public void Delete(int idOrderItem1)
    {
        foreach (OrderItem orderItem in _dstaSource.OrderItemList)
        {
            if (idOrderItem1 == orderItem.Id)
            {
                _dstaSource.OrderItemList.Remove(orderItem);
                return;
            }
        }
        throw new NotFoundExceptions("The orderItem is not exist in the List");
    }



    /// <summary>
    /// An object update method that will receive a new order item
    /// </summary>
    /// <param name="orderItem1"></param>
    public void Update(OrderItem orderItem1)
    {
        if (_dstaSource.OrderItemList.Exists(x => x?.Id == orderItem1.Id))
        {
            int j = _dstaSource.OrderItemList.IndexOf(_dstaSource.OrderItemList.Find(x => x?.Id == orderItem1.Id));
            _dstaSource.OrderItemList[j] = orderItem1;
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
    //public OrderItem GetOrderItemofTwoId(int idOrderItem1, int idOrderItem2)
    //{
    //    foreach (OrderItem orderItem in _dstaSource.OrderItemList)
    //    {
    //        if (orderItem.OrderId == idOrderItem1 && orderItem.ProductId == idOrderItem2)
    //            return orderItem;
    //    }
    //    throw new NotFoundExceptions("The orderItem id is not exist in List");

    //}

    ///// <summary>
    ///// Method of request/reading of a list/array of order details according to the ID number of an order
    ///// </summary>
    ///// <param name="myOrderId"></param>
    ///// <returns></returns>
    ///// <exception cref="NotFoundExceptions"></exception>
    //public IEnumerable<OrderItem?> GetListOrderItems(int myOrderId)
    //{
    //    //if(_dstaSource.OrderItemList.Exists(x => x.OrderId == myOrderId))
    //    //{
    //    //    return _dstaSource.OrderItemList.FindAll(x => x.OrderId == myOrderId).ToList();
    //    //}
    //    //throw new NotFoundExceptions("the order is not exist in List");

    //    return  _dstaSource.OrderItemList.Where(elem => myOrderId == elem?.OrderId) 
    //        ?? throw new NotFoundExceptions("the order is not exist in List");
    //}


}
