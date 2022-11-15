
using DO;
using System.Diagnostics;

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
        for (int i = 0; i < DataSource.Config.OrderItemFreeIndex; i++)
        {
            if (DataSource.orderItemArray[i].Id == orderItem1.Id)
            {
                //throw new Exception("no place in arr to add");
            }

        }
        DataSource.orderItemArray[DataSource.Config.OrderItemFreeIndex++] = orderItem1;
        return orderItem1.Id;
    }

    /// <summary>
    /// A request/call method of a single object that receives an order item ID number and returns the appropriate order item
    /// </summary>
    /// <param name="idOrderItem1"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem getOrderItem(int idOrderItem1)
    {
        for (int i = 0; i < DataSource.Config.OrderItemFreeIndex; i++)
        {

            if (DataSource.orderItemArray[i].Id == idOrderItem1)
            {
                return DataSource.orderItemArray[i];

            }
        }
        throw new Exception("the orderItem id is not exist in array");
    }

    /// <summary>
    /// A request/read method of the list of all order item objects
    /// </summary>
    /// <returns></returns>
    public OrderItem[] getArrayOfOrderItem()
    {
        /*OrderItem[] neworderItemArray = new OrderItem[DataSource.Config.OrderItemFreeIndex];
        for (int i = 0; i < DataSource.Config.OrderItemFreeIndex; i++)
        {
            neworderItemArray[i] = getOrderItem(i);
        }
        return neworderItemArray;*/
        return DataSource.orderItemArray.ToArray();
    }

    /// <summary>
    /// A method to delete an order items object that receives an order item ID number
    /// </summary>
    /// <param name="idOrderItem1"></param>
    public void deleteOrderItem(int idOrderItem1)
    {
        for (int i = 0; i < DataSource.Config.OrderItemFreeIndex; i++)
        {

            if (DataSource.orderItemArray[i].Id == idOrderItem1)
            {
                for (int j = i; j < DataSource.Config.OrderItemFreeIndex - 1; j++)
                {
                    DataSource.orderItemArray[j] = DataSource.orderItemArray[j + 1];
                }
                DataSource.Config.OrderItemFreeIndex--;
                break;
            }
        }
        throw new Exception("The orderItem is not exist in the array");

    }



/// <summary>
/// An object update method that will receive a new order item
/// </summary>
/// <param name="orderItem1"></param>
public void updateOredrItem(OrderItem orderItem1)
    {

        for (int i = 0; i < DataSource.Config.OrderItemFreeIndex; i++)
        {
            if (DataSource.orderItemArray[i].Id == orderItem1.Id)
            {
                DataSource.orderItemArray[i].Id = orderItem1.Id;
                DataSource.orderItemArray[i].ProductId=orderItem1.ProductId;
                DataSource.orderItemArray[i].OrderId=orderItem1.OrderId;
                DataSource.orderItemArray[i].Price=orderItem1.Price;
                DataSource.orderItemArray[i].Amount = orderItem1.Amount;
            }
        }
    }
    /// <summary>
    /// Request/call method based on two identifiers (ID) - product ID and order ID,
    /// the method returns the object of an item in the corresponding order
    /// </summary>
    /// <param name="idOrderItem1"></param>
    /// <param name="idOrderItem2"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem getOrderItemofTwoId(int idOrderItem1, int idOrderItem2)
    {
        for (int i = 0; i < DataSource.Config.OrderItemFreeIndex; i++)
        {
            if ((DataSource.orderItemArray[i].OrderId == idOrderItem1) && (DataSource.orderItemArray[i].ProductId == idOrderItem2))
            {
                return DataSource.orderItemArray[i];

            }

        }

        throw new Exception("the orderItem id is not exist in array");

    }

    /// <summary>
    /// Method of request/reading of a list/array of order details according to the ID number of an order
    /// </summary>
    /// <param name="idorder1"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem[] getArrayOfOrderItemOfOrder(/*Order*/ int idorder1)
    {
        for (int i = 0; i < DataSource.Config.OrderFreeIndex; i++)
        {
            if (DataSource.orderArray[i].Id == idorder1/*idorder1.Id*/)
                return DataSource.orderItemArray;
        }
        throw new Exception("the order is not exist in array");

        //להחזיר עותק של המערך
        //  return DataSource.orderItemArray.ToArray();


        //OrderItem[] neworderItemArray = new OrderItem[DataSource.Config.OrderItemFreeIndex];
        //for (int i = 0; i < DataSource.Config.OrderItemFreeIndex; i++)
        //{
        //    neworderItemArray[i] = getOrderItem(i);
        //}
        //return neworderItemArray;

    }
    ////////////////////////////////////////////////////////////////////////////

}
