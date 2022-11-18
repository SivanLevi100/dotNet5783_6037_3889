
using DO;

namespace Dal;

public class DalOrder
{
    /// <summary>
    /// An add object method that accepts an order object and returns the ID number of the added order
    /// </summary>
    /// <param name="order1"></param>
    /// <returns></returns>
    public int addOrders(Order order1)
    {
        for (int i = 0; i < DataSource.Config.OrderFreeIndex; i++)
        {
            if (DataSource.orderArray[i].Id == order1.Id)
            {
                throw new Exception("no place in arr to add");
            }

        }
        DataSource.orderArray[DataSource.Config.OrderFreeIndex++] = order1;
        return order1.Id;
    }

    /// <summary>
    /// A request/call method of a single object that receives an order ID number and returns the appropriate order
    /// </summary>
    /// <param name="idOrder1"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Order getOrder(int idOrder1)
    {
        for (int i = 0; i < DataSource.Config.OrderFreeIndex; i++)
        {

            if (DataSource.orderArray[i].Id == idOrder1)
            {
                return DataSource.orderArray[i];

            }
        }
        throw new Exception("the order id is not exist in array");
    }


    /// <summary>
    /// Request/read method of the list of all objects of an order
    /// </summary>
    /// <returns></returns>
    public Order[] getArrayOfOrder()
    {
        return DataSource.orderArray.ToArray();

    }

    /// <summary>
    /// A method to delete an order object that receives an order ID number
    /// </summary>
    /// <param name="idOrder1"></param>
    public void deleteOrder(int idOrder1)
    {
        for (int i = 0; i < DataSource.orderArray.Length; i++)
        {

            if (DataSource.orderArray[i].Id == idOrder1)
            {
                for (int j = i; j < DataSource.orderArray.Length - 1; j++)
                {
                    DataSource.orderArray[j] = DataSource.orderArray[j + 1];
                }
                DataSource.Config.OrderFreeIndex--;
                return;
            }
        }
        throw new Exception("The order is not exist in the array");
    }



    /// <summary>
    /// An object update method that will receive a new order
    /// </summary>
    /// <param name="order1"></param>
    public void updateOrder(Order order1)
    {

        for (int i = 0; i < DataSource.Config.OrderFreeIndex; i++)
        {
            if (DataSource.orderArray[i].Id == order1.Id)
            {
                DataSource.orderArray[i] = order1;
                return;
            }
        }
        throw new Exception("the order id is not exist in array");
    }


}



