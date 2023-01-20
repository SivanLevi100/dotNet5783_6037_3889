//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using BO;
namespace BlApi;

public interface IOrder
{
    /// <summary>
    /// Order list request (admin screen)
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList> GetOrderList();

    /// <summary>
    /// Order details request (for manager screen and buyer screen)
    /// </summary>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    public BO.Order GetOrderDetails(int idOrder);

    /// <summary>
    /// Order Shipping Update (Manager Order Management Screen)
    /// </summary>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    public BO.Order UpdateDelivery(int idOrder);

    /// <summary>
    /// Order Delivery Update (Admin Order Management Screen)
    /// </summary>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    public BO.Order UpdateShipping(int idOrder);

    /// <summary>
    /// Order Tracking (Manager Order Management Screen)
    /// </summary>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    public BO.OrderTracking Tracking(int idOrder);

    /// <summary>
    /// A function that adds a product to an already existing order
    /// </summary>
    /// <param name="order"></param>
    /// <param name="idProduct"></param>
    /// <param name="Amount"></param>
    /// <returns></returns>
    public BO.Order AddItemForOrder(BO.Order order, int idProduct, int Amount);


    /// <summary>
    /// The method of selecting the next order to handle
    /// </summary>
    /// <returns></returns>
    public int? GetNextOrder();

}
