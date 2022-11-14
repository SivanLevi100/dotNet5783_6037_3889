
using DO;

namespace Dal;

public class DalOrder
{
    /// <summary>
    /// An add object method that accepts an order object and returns the ID number of the added order
    /// </summary>
    /// <param name="order1"></param>
    /// <returns></returns>
    public static int addOrders(Order order1)
    {
        for (int i = 0; i < DataSource.Config.OrderFreeIndex; i++)
        {
            if (DataSource.orderArray[i].Id == order1.Id)
            {
                //throw new Exception("no place in arr to add");
            }

        }
        DataSource.orderArray[DataSource.Config.OrderFreeIndex++] = new Order()
        {
            Id = DataSource.Config.OrderLastId,
            CustomerName= order1.CustomerName,
            CustomerEmail= order1.CustomerEmail,
            CustomerAdress= order1.CustomerAdress,
            OrderDate= order1.OrderDate,
            ShipDate= order1.ShipDate,
            DeliveryDate= order1.DeliveryDate,
        };
        return order1.Id;
    }

    /// <summary>
    /// A request/call method of a single object that receives an order ID number and returns the appropriate order
    /// </summary>
    /// <param name="idOrder1"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static Order getOrder(int idOrder1)
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
    public static Order[] getArrayOfOrder()
    {
        Order[] neworderArray=new Order[DataSource.Config.OrderFreeIndex];
        for (int i = 0; i < DataSource.Config.OrderFreeIndex; i++)
        {
            neworderArray[i] = new Order();
        }
        return neworderArray;

    }

    /// <summary>
    /// A method to delete an order object that receives an order ID number
    /// </summary>
    /// <param name="idOrder1"></param>
    public static void deleteOrder(int idOrder1)
    {
        for (int i = 0; i < DataSource.Config.OrderFreeIndex; i++)
        {

            if (DataSource.orderArray[i].Id == idOrder1)
            {
                for (int j = i; j < DataSource.Config.OrderFreeIndex - 1; j++)
                {
                    DataSource.orderArray[j] = DataSource.orderArray[j + 1];
                }
                DataSource.Config.OrderFreeIndex--;
                break;
            }
        }
        throw new Exception("The order is not exist in the array");

    }



/// <summary>
/// An object update method that will receive a new order
/// </summary>
/// <param name="order1"></param>
public static void updateOrder(Order order1)
    {

        for (int i = 0; i < DataSource.Config.OrderFreeIndex; i++)
        {
            if (DataSource.orderArray[i].Id == order1.Id)
            {
                DataSource.orderArray[i].Id = order1.Id;
                DataSource.orderArray[i].CustomerName= order1.CustomerName;
                DataSource.orderArray[i].CustomerEmail= order1.CustomerEmail;
                DataSource.orderArray[i].CustomerAdress= order1.CustomerAdress;
                DataSource.orderArray[i].OrderDate= order1.OrderDate;
                DataSource.orderArray[i].ShipDate= order1.ShipDate;
                DataSource.orderArray[i].DeliveryDate= order1.DeliveryDate;

            }
        }
    }


}



