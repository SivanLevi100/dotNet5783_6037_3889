
using DO;
using System.Diagnostics;

namespace Dal;

public class DalOrderItem
{
    public int addOrderItems(OrderItem orderItem1)
    {
        for (int i = 0; i < DataSource.Config.OrderItemFreeIndex; i++)
        {
            if (DataSource.orderItemArray[i].Id == orderItem1.Id)
            {
                //throw new Exception("no place in arr to add");
            }

        }
        DataSource.orderItemArray[DataSource.Config.OrderItemFreeIndex++] = new OrderItem()
        {
           Id = DataSource.Config.OrderItemLastId,
           ProductId =orderItem1.ProductId,
           OrderId=orderItem1.OrderId,
           Price = orderItem1.Price,
           Amount = orderItem1.Amount,
        };
        return orderItem1.Id;
    }

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


    public OrderItem[] getArrayOfOrderItem()
    {
        OrderItem[] neworderItemArray = new OrderItem[DataSource.Config.OrderItemFreeIndex];
        for (int i = 0; i < DataSource.Config.OrderItemFreeIndex; i++)
        {
            neworderItemArray[i] = getOrderItem(i);
        }
        return neworderItemArray;
    }


    public void deleteOrderItem(int idOrderItem1)
    {
        for (int i = 0; i < DataSource.Config.OrderItemFreeIndex; i++)
        {

            if (DataSource.orderItemArray[i].Id == idOrderItem1)
            {
                /////////////מחיקה

            }

        }
    }

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

    //מתודת בקשה\קריאה של רשימת\מערך פרטי הזמנה ע"פ מספר מזהה של הזמנה
    public OrderItem[] getArrayOfOrderItemOfOrder(Order idorder1)
    {
        for (int i = 0; i < DataSource.Config.OrderFreeIndex; i++)
        {
            if (DataSource.orderArray[i].Id == idorder1.Id)
                return DataSource.orderItemArray;
        }
        throw new Exception("the order is not exist in array");




        //OrderItem[] neworderItemArray = new OrderItem[DataSource.Config.OrderItemFreeIndex];
        //for (int i = 0; i < DataSource.Config.OrderItemFreeIndex; i++)
        //{
        //    neworderItemArray[i] = getOrderItem(i);
        //}
        //return neworderItemArray;

    }
    ////////////////////////////////////////////////////////////////////////////

}
