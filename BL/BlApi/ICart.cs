using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using BO;
namespace BlApi;

public interface ICart
{

    /// <summary>
    /// Adding a product to the shopping cart (for catalog screen, product details screen)
    /// </summary>
    /// <param name="cart1"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public Cart AddProduct(Cart cart1, int id);

    /// <summary>
    /// Updating the quantity of a product in the shopping cart (for the shopping cart screen)
    /// </summary>
    /// <param name="cart1"></param>
    /// <param name="id"></param>
    /// <param name="newAmount"></param>
    /// <returns></returns>
    public Cart UpdateAmountOfProduct(Cart cart1, int id, int newAmount);

    /// <summary>
    /// Basket confirmation for order \ placing order (for shopping basket screen or order completion screen)
    /// </summary>
    /// <param name="cart1"></param>
    public void Confirm(Cart cart1);

}
