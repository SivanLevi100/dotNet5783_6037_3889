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
    public IEnumerable<BO.Order> GetOrderList();

    /// <summary>
    /// Order details request (for manager screen and buyer screen)
    /// </summary>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    public BO.Order GetProductDetails(int idOrder);

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


}
