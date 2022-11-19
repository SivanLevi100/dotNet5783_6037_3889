using DO;

namespace DalApi;

public interface IOrderItem: ICrud<OrderItem>
{
    OrderItem GetOrderItemofTwoId();
    OrderItem[] GetArrayOfOrderItemOfOrder();
}
