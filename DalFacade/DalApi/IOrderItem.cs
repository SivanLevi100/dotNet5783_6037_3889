using DO;
namespace DalApi;

public interface IOrderItem: ICrud<OrderItem>
{
    OrderItem GetOrderItemofTwoId(int id1,int id2);
    IEnumerable<OrderItem> GetListOfOrderItemOfOrder(int id);
}
