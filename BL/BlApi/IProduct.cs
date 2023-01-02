//using DO;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using BO;
namespace BlApi;

public interface IProduct
{
    /// <summary>
    /// Product list request (for manager screen and for buyer's catalog screen)
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.ProductForList?> GetProductList();

    /// <summary>
    /// Product details request (for manager screen)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Product GetProductDetailsManager(int id);

    /// <summary>
    /// Product details request (for buyer's catalog screen)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cart"></param>
    /// <returns></returns>
    public BO.ProductItem GetProductDetailsBuyer(int id, BO.Cart cart);

    /// <summary>
    /// Adding a product (for admin screen)
    /// </summary>
    /// <param name="product1"></param>
    public void Add(BO.Product product1);

    /// <summary>
    /// Product deletion (for admin screen)
    /// </summary>
    /// <param name="id"></param>
    public void Delete(int id);

    /// <summary>
    /// Update product data (for admin screen)
    /// </summary>
    /// <param name="product1"></param>
    public void Update(BO.Product product1);

    public IEnumerable<BO.ProductItem?> GetProductItemList();

}
