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

namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    private DalApi.IDal? Dal = DalApi.Factory.Get();

    public IEnumerable<BO.ProductForList> GetProductList()
    {
        try
        {
            return from item in Dal?.Product.GetAll()
                   where item != null
                   let product = ((DO.Product)item)!
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


    public BO.Product GetProductDetailsManager(int id)
    {
        if (id < 0) throw new BO.IncorrectDataExceptions("id order is invalid");

        try
        {
            DO.Product productOfDo = Dal?.Product.Get(id) ?? throw new BO.NotExiestsExceptions("The Product is not exiests");
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


    public BO.ProductItem GetProductDetailsBuyer(int id, BO.Cart cart)
    {
        if (id < 0) throw new BO.IncorrectDataExceptions("id order is invalid");

        try
        {
            DO.Product productOfDO = Dal?.Product.Get(id) ?? throw new BO.NotExiestsExceptions("The Product is not exiests");
            BO.ProductItem productItem = new BO.ProductItem
            {
                IdProduct = productOfDO.Id,
                Name = productOfDO.Name,
                Category = (BO.Category?)productOfDO.Category ?? throw new BO.NotExiestsExceptions("Category is Unavailable"),
                IsAvailable = (productOfDO.InStock > 0) ? true : false,
                AmountInCart = cart?.OrdersItemsList?.FindAll(orderItem => orderItem?.ProductId == id).Count() ?? throw new BO.NotExiestsExceptions("The list of order items in the shopping cart is null"),
                Price = productOfDO.Price
            };
            return productItem;
        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.IncorrectDataExceptions("Product request failed", str);
        }
    }


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
                    Category = (DO.Category?)product1.Category ?? throw new BO.NotExiestsExceptions("Category is Unavailable"),
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

    public void Delete(int id)
    {
        try
        {
            var orderItemD = (from item in Dal?.Order.GetAll() ?? throw new BO.NotExiestsExceptions("The List Of Product is not exiests")
                              let order = ((DO.Order)item)!
                              from item1 in Dal.OrderItem.GetAll()
                              let orderItem = ((DO.OrderItem)item1)!
                              where order.Id == orderItem.OrderId && orderItem.ProductId != id
                              select orderItem).FirstOrDefault();
            Dal.Product.Delete(orderItemD.ProductId);
        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("No such product exists at all", str);
        }



        //try
        //{
        //    foreach (DO.Order? order in Dal?.Order.GetAll() ?? throw new BO.NotExiestsExceptions("The List Of Product is not exiests"))//Loop through all orders 
        //    {
        //        if (Dal.OrderItem.GetAll(OrderItem => OrderItem.Value.OrderId == order?.Id).Any(orderItem => orderItem.Value.ProductId != id))//If the product is not in the list of order details in the basket
        //        {
        //            Dal?.Product.Delete(id);
        //            return;
        //        }
        //        else
        //            throw new BO.NotExiestsExceptions("This product appears on orders");
        //    }
        //}
        //catch (DO.NotFoundExceptions str)
        //{
        //    throw new BO.NotExiestsExceptions("No such product exists at all", str);
        //}
    }


    public void Update(BO.Product product1)
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


}


