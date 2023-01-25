//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//using BlApi;
//using BO;
using DalApi;
using System.Reflection.Emit;

namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    private DalApi.IDal? Dal = DalApi.Factory.Get();

    /// <summary>
    /// get method of product list
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.NotExiestsExceptions"></exception>
    /// <exception cref="BO.IncorrectDataExceptions"></exception>
    public IEnumerable<BO.ProductForList> GetProductList()
    {
        try
        {
            //Going through all the items in the product list in DAL
            //and returning a product according to a supplier ID
            return from item in Dal?.Product.GetAll()
                   where item != null
                   let product = ((DO.Product)item)!
                   orderby product.Id
                   select new BO.ProductForList
                   {
                       IdProduct = product.Id,
                       Name = product.Name,
                       Price = product.Price,
                       Category = (BO.Category?)product.Category ?? throw new BO.NotExiestsExceptions("Category is Unavailable")
                   };
        }
        catch (DO.DoesNotExistException str)
        {
            throw new BO.IncorrectDataExceptions("Product request failed", str);
        }

    }

    /// <summary>
    /// get method of product item list
    /// </summary>
    /// <param name="cart"></param>
    /// <returns></returns>
    /// <exception cref="BO.NotExiestsExceptions"></exception>
    /// <exception cref="BO.IncorrectDataExceptions"></exception>
    public IEnumerable<BO.ProductItem?> GetProductItemList(BO.Cart cart)
    {
        try
        {
            //Going through all the items in the product list in DAL
            //and returning a product item according to a supplier ID
            return from item in Dal?.Product.GetAll()
                   where item != null
                   let product = ((DO.Product)item)!
                   select new BO.ProductItem
                   {
                       IdProduct = product.Id,
                       Name = product.Name,
                       Price = product.Price,
                       Category = (BO.Category?)product.Category ?? throw new BO.NotExiestsExceptions("Category is Unavailable"),
                       IsAvailable = product.InStock > 0 ? true : false,
                       AmountInCart = cart?.OrdersItemsList == null ? 0 : cart?.OrdersItemsList?.FindAll(os => os?.ProductId == product.Id)
                       .Sum(o => o?.AmountInOrder) ?? throw new BO.NotExiestsExceptions("The list of order items in the shopping cart is null")
                   };
        }
        catch (DO.DoesNotExistException str)
        {
            throw new BO.IncorrectDataExceptions("Product request failed", str);
        }


    }

    /// <summary>
    /// A method that receives a product ID number and returns the product's details as requested by the manager
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.IncorrectDataExceptions"></exception>
    /// <exception cref="BO.NotExiestsExceptions"></exception>
    public BO.Product GetProductDetailsManager(int id)
    {
        if (id < 0) throw new BO.IncorrectDataExceptions("id order is invalid");

        try
        {
            DO.Product productOfDo = Dal?.Product.Get(id) ?? throw new BO.NotExiestsExceptions("The Product is not exiests");
            //Creating an instance of a product with the features of the product with the received ID number           
            BO.Product product = new BO.Product
            {
                Id = productOfDo.Id,
                Name = productOfDo.Name,
                Category = (BO.Category?)productOfDo.Category ?? throw new BO.NotExiestsExceptions("Category is Unavailable"),
                Price = productOfDo.Price,
                InStock = productOfDo.InStock,
            };
            return product;
        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.IncorrectDataExceptions("Product request failed", str);
        }
    }

    /// <summary>
    /// A method that receives a product's ID number and a cart
    /// and returns the product's details as requested by the buyer
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cart"></param>
    /// <returns></returns>
    /// <exception cref="BO.IncorrectDataExceptions"></exception>
    /// <exception cref="BO.NotExiestsExceptions"></exception>
    public BO.ProductItem GetProductDetailsBuyer(int id, BO.Cart cart)
    {
        if (id < 0) throw new BO.IncorrectDataExceptions("id order is invalid");

        try
        {
            DO.Product productOfDO = Dal?.Product.Get(id) ?? throw new BO.NotExiestsExceptions("The Product is not exiests");
            //Creating an instance of a product item with the features of the product with the received ID number           

            BO.ProductItem productItem = new BO.ProductItem
            {
                IdProduct = productOfDO.Id,
                Name = productOfDO.Name,
                Category = (BO.Category?)productOfDO.Category ?? throw new BO.NotExiestsExceptions("Category is Unavailable"),
                IsAvailable = (productOfDO.InStock > 0) ? true : false,
                AmountInCart = cart?.OrdersItemsList == null ? 0 : cart?.OrdersItemsList?.FindAll(orderItem => orderItem?.ProductId == id)
                .Sum(o => o.AmountInOrder) ?? throw new BO.NotExiestsExceptions("The list of order items in the shopping cart is null"),
                Price = productOfDO.Price
            };
            return productItem;
        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.IncorrectDataExceptions("Product request failed", str);
        }
    }

    /// <summary>
    /// Method of adding a product to the product's list in the catalog
    /// </summary>
    /// <param name="product1"></param>
    /// <exception cref="BO.IncorrectDataExceptions"></exception>
    public void Add(BO.Product product1)
    {
        if (product1.Id > 0 && !string.IsNullOrWhiteSpace(product1.Name) && product1.Price > 0 && product1.InStock >= 0)
        {
            try
            {
                DO.Product productOfDO = new DO.Product
                {
                    Id = product1.Id,
                    Name = product1.Name,
                    Price = product1.Price,
                    InStock = product1.InStock,
                    Category = (DO.Category?)product1.Category ?? throw new BO.IncorrectDataExceptions("Category is Unavailable"),
                };
                Dal?.Product.Add(productOfDO);
            }
            catch (DO.NotFoundExceptions str)
            {
                throw new BO.IncorrectDataExceptions("Failed to add product", str);
            }
        }
        else
            throw new BO.IncorrectDataExceptions("The product data received is incorrect");//The incorrectness of the data received as a parameter
    }

    /// <summary>
    /// A method of deleting a product from the product's list in the catalog
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="BO.NotExiestsExceptions"></exception>
    public void Delete(int id)
    {
        try
        {
            foreach (DO.Order? order in Dal?.Order.GetAll() ?? throw new BO.NotExiestsExceptions("The List Of Product is not exiests"))//Loop through all orders 
            {
                //If the product is not in the list of order details in the basket
                if (Dal.OrderItem.GetAll(OrderItem => ((DO.OrderItem)OrderItem!).OrderId == order?.Id).Any(orderItem => ((DO.OrderItem)orderItem!).ProductId != id))
                {
                    Dal?.Product.Delete(id);
                    return;
                }
                else
                    throw new BO.NotExiestsExceptions("This product appears on orders");
            }
        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("No such product exists at all", str);
        }


    }

    /// <summary>
    /// A method of updating a product from the product's list in the catalog
    /// </summary>
    /// <param name="product1"></param>
    /// <exception cref="BO.NotExiestsExceptions"></exception>
    /// <exception cref="BO.IncorrectDataExceptions"></exception>
    public void Update(BO.Product product1)
    {
        //If the product is not negative or 0, the name is not null,
        //the product's price is not negative or 0 and it's amount in stock is not negative
        if (product1.Id > 0 && !string.IsNullOrWhiteSpace(product1.Name) && product1.Price > 0 && product1.InStock >= 0)
        {
            try
            {
                DO.Product productOfDO = new DO.Product
                {
                    Id = product1.Id,
                    Name = product1.Name,
                    Price = product1.Price,
                    InStock = product1.InStock,
                    Category = (DO.Category?)product1?.Category ?? throw new BO.NotExiestsExceptions("Category is Unavailable"),
                };
                Dal?.Product.Update(productOfDO);

            }
            catch (DO.NotFoundExceptions str)
            {
                throw new BO.IncorrectDataExceptions("Failed to update product", str);
            }
        }
        else
            throw new BO.IncorrectDataExceptions("The product data received is incorrect");//The incorrectness of the data received as a parameter
    }

    ////פונקציה במחזירה את הid של כל המוצרים
    public List<int> getids()
    {
        List<int> ids = new();
        foreach(var item in Dal.Product.GetAll())
        {
            ids.Add(item.Value.Id);
        }
        return ids;
    
    }


}


