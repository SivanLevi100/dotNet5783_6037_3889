﻿
using DalApi;
using DO;

namespace Dal;

internal class DalOrder:IOrder
{
    DataSource _dstaSource = DataSource.s_instance;

    /// <summary>
    /// An add object method that accepts an order object and returns the ID number of the added order
    /// </summary>
    /// <param name="order1"></param>
    /// <returns></returns>
    public int Add(Order order1)
    {
        if (_dstaSource.OrderList.Exists(x => x.Id == order1.Id))
            throw new DuplicateIdExceptions("no place in List to add");
        _dstaSource.OrderList.Add(order1);
        return order1.Id;
    }

    /// <summary>
    /// A request/call method of a single object that receives an order ID number and returns the appropriate order
    /// </summary>
    /// <param name="idOrder1"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundExceptions"></exception>
    public Order Get(int idOrder1)
    {
        foreach (Order order in _dstaSource.OrderList)
        {
            if (order.Id == idOrder1)
                return order;
        }
        throw new NotFoundExceptions("The order id is not exist in List");
    }


    /// <summary>
    /// Request/read method of the list of all objects of an order
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order> GetList()
    {
        return _dstaSource.OrderList.ToList();
    }

    /// <summary>
    /// A method to delete an order object that receives an order ID number
    /// </summary>
    /// <param name="idOrder1"></param>
    public void Delete(int idOrder1)
    {
        foreach (Order order in _dstaSource.OrderList)
        {
            if (idOrder1 == order.Id)
            {
                _dstaSource.OrderList.Remove(order);
                return;
            }
        }
        throw new NotFoundExceptions("The order is not exist in the List");
    }



    /// <summary>
    /// An object update method that will receive a new order
    /// </summary>
    /// <param name="order1"></param>
    public void Update(Order order1)
    {
        if (_dstaSource.OrderList.Exists(x => x.Id == order1.Id))
        {
            int j = _dstaSource.OrderList.IndexOf(_dstaSource.OrderList.Find(x => x.Id == order1.Id));
            _dstaSource.OrderList[j] = order1;
            return;
        }
        throw new NotFoundExceptions("the order id is not exist in List");
    }


}



