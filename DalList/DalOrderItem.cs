
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
    public OrderItem? Get(int id)
    {
        return _dstaSource.OrderItemList?.FirstOrDefault(orderItem => ((DO.OrderItem)orderItem!).Id == id)
              ?? throw new NotFoundExceptions("The orderItem id is not exist in List");
    }

    public OrderItem GetF(Func<OrderItem?, bool>? filter)
    {
        return _dstaSource.OrderItemList?.FirstOrDefault(orderItem => filter(orderItem))
          ?? throw new NotFoundExceptions("The orderItem id is not exist in List");
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
        if (_dstaSource.OrderItemList?.RemoveAll(orderItem => ((DO.OrderItem)orderItem!).Id == idOrderItem1) == 0)
            throw new NotFoundExceptions("The OrderItem is not exist in the List");
    }



    /// <summary>
    /// An object update method that will receive a new order item
    /// </summary>
    /// <param name="orderItem1"></param>
    public void Update(OrderItem orderItem1)
    {
        if (_dstaSource.OrderItemList.Exists(x => x?.Id == orderItem1.Id))
        {
            _dstaSource.OrderItemList.RemoveAll(orderItem => ((DO.OrderItem)orderItem!).Id == orderItem1.Id);
            _dstaSource.OrderItemList.Add(orderItem1);
            return;
        }
        throw new NotFoundExceptions("the orderItem id is not exist in List");
    }


}
