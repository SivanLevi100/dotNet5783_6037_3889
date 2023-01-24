
using DalApi;
using DO;
using System.Runtime.CompilerServices;
using static Dal.DataSource;

namespace Dal;

internal class DalOrder:IOrder
{
    DataSource _dstaSource1 = DataSource.S_instance;

    [MethodImpl(MethodImplOptions.Synchronized)]
    /// <summary>
    /// An add object method that accepts an order object and returns the ID number of the added order
    /// </summary>
    /// <param name="order1"></param>
    /// <returns></returns>
    public int Add(Order order1)
    {
        if (_dstaSource1.OrderList.Exists(x => x?.Id == order1.Id))
            throw new DuplicateIdExceptions("no place in List to add");
        order1.Id = Config.OrderLastId;
        _dstaSource1.OrderList.Add(order1);
        return order1.Id;

    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    /// <summary>
    /// A request/call method of a single object that receives an order ID number and returns the appropriate order
    /// </summary>
    /// <param name="idOrder1"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundExceptions"></exception>
    public Order? Get(int idOrder1)
    {
        return _dstaSource1.OrderList?.FirstOrDefault(order => ((DO.Order)order!).Id == idOrder1)
          ?? throw new NotFoundExceptions("The order id is not exist in List");
    }


    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order? GetF(Func<Order?, bool>? filter)
    {
        return _dstaSource1.OrderList?.FirstOrDefault(order => filter(order))
          ?? throw new NotFoundExceptions("The order id is not exist in List");
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    /// <summary>
    /// Request/read method of the list of all objects of an order
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter) =>
        (filter == null ? _dstaSource1.OrderList?.Select(item => item)
        : _dstaSource1.OrderList?.Where(item => filter(item))
        ?? throw new DoesNotExistException("Missing order"))
        ?? throw new DataCorruptionException("Missing order list");


    [MethodImpl(MethodImplOptions.Synchronized)]

    /// <summary>
    /// A method to delete an order object that receives an order ID number
    /// </summary>
    /// <param name="idOrder1"></param>
    public void Delete(int idOrder1)
    {
        if (_dstaSource1.OrderList?.RemoveAll(order => ((DO.Order)order!).Id == idOrder1) == 0)
            throw new NotFoundExceptions("The order is not exist in the List");
    }


    [MethodImpl(MethodImplOptions.Synchronized)]
    /// <summary>
    /// An object update method that will receive a new order
    /// </summary>
    /// <param name="order1"></param>
    public void Update(Order order1)
    {
        if (_dstaSource1.OrderList.Exists(x => x?.Id == order1.Id))
        {
            _dstaSource1.OrderList.RemoveAll(order => ((DO.Order)order!).Id == order1.Id);
            _dstaSource1.OrderList.Add(order1);
            return;
        }
        throw new NotFoundExceptions("the order id is not exist in List");
    }


}



