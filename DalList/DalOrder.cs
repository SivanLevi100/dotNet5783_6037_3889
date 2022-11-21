
using DalApi;
using DO;

namespace Dal;

internal class DalOrder:IOrder
{
    /// <summary>
    /// An add object method that accepts an order object and returns the ID number of the added order
    /// </summary>
    /// <param name="order1"></param>
    /// <returns></returns>
    public int Add(Order order1)
    {
        if (DataSource.orderList.Exists(x => x.Id == order1.Id))
        {
            throw new DuplicateIdExceptions("no place in List to add");
        }
        DataSource.orderList.Add(order1);
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
        if (DataSource.orderList.Exists(x => x.Id == idOrder1))
            return DataSource.orderList.Find(x => x.Id == idOrder1);
        throw new NotFoundExceptions("the order id is not exist in List");
    }


    /// <summary>
    /// Request/read method of the list of all objects of an order
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order> GetList()
    {
        return DataSource.orderList.ToList();

    }

    /// <summary>
    /// A method to delete an order object that receives an order ID number
    /// </summary>
    /// <param name="idOrder1"></param>
    public void Delete(int idOrder1)
    {
        if (DataSource.orderList.Exists(x => x.Id == idOrder1))
        {
            Order orderDelete = DataSource.orderList.Find(x => x.Id == idOrder1);
            DataSource.orderList.Remove(orderDelete);
            return;
        }
        throw new NotFoundExceptions("The order is not exist in the List");
    }



    /// <summary>
    /// An object update method that will receive a new order
    /// </summary>
    /// <param name="order1"></param>
    public void Update(Order order1)
    {
        if (DataSource.orderList.Exists(x => x.Id == order1.Id))
        {
            int j = DataSource.orderList.IndexOf(DataSource.orderList.Find(x => x.Id == order1.Id));
            DataSource.orderList[j] = order1;
            return;
        }
        throw new NotFoundExceptions("the order id is not exist in List");


        //for (int i = 0; i < DataSource.Config.OrderFreeIndex; i++)
        //{
        //    if (DataSource.orderArray[i].Id == order1.Id)
        //    {
        //        DataSource.orderArray[i] = order1;
        //        return;
        //    }
        //}
        //throw new Exception("the order id is not exist in array");
    }


}



