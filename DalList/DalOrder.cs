
using DO;

namespace Dal;

public class DalOrder
{
    public int addOrders(Order order1)
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



    public Order[] getArrayOfOrder()
    {
        Order[] neworderArray=new Order[DataSource.Config.OrderFreeIndex];
        for (int i = 0; i < DataSource.Config.OrderFreeIndex; i++)
        {
            neworderArray[i] = new Order();
        }
        return neworderArray;

    }


    public void deleteOrder(int idOrder1)
    {
        for (int i = 0; i < DataSource.Config.OrderFreeIndex; i++)
        {

            if (DataSource.orderArray[i].Id == idOrder1)
            {
                /////////////מחיקה

            }

        }
    }
    public void updateOrder(Order order1)
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



