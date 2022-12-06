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
using Dal;
using DalApi;

namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    private IDal Dal = new DalList();

    public IEnumerable<BO.ProductForList> GetProductList()
    {

        //List<ProductForList> p = new List<ProductForList>();
        //foreach (DO.Product product in Dal.Product.GetList())
        //{
        //    BO.ProductForList productForList = new BO.ProductForList
        //    {

        //        IdProduct = product.Id,
        //        Name = product.Name,
        //        Price = product.Price,
        //        Category = (BO.Category)product.Category
        //    };
        //    p.Add(productForList);
        //};
        //return p.AsEnumerable();

        return Dal.Product.GetList().Select(product => new BO.ProductForList
        {
            IdProduct = product.Id,
            Name = product.Name,
            Price = product.Price,
            Category = (BO.Category)product.Category
        });
    }


    public BO.Product GetProductDetailsManager(int id)
    {
        try
        {
            if (id > 0)
            {
                DO.Product productOfDo = Dal.Product.Get(id);
               // DO.Product productOfDo = Dal?.Product.Get(id) ?? throw new BO.IncorrectDataExceptions("id not positive");
                BO.Product product = new BO.Product
                {
                    Id = productOfDo.Id,
                    Name = productOfDo.Name,
                    Category = (BO.Category)productOfDo.Category,
                    Price = productOfDo.Price,
                    InStock = productOfDo.InStock,
                };
                return product;
            }
            else
                throw new BO.NotExiestsExceptions("Product request failed");
        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.IncorrectDataExceptions("Product request failed", str);
        }
    }


    public BO.ProductItem GetProductDetailsBuyer(int id, BO.Cart cart)
    {
        try
        {
            if (id > 0)
            {
                DO.Product productOfDO = Dal.Product.Get(id);
                BO.ProductItem productItem = new BO.ProductItem
                {
                    IdProduct = productOfDO.Id,
                    Name = productOfDO.Name,
                    Category = (BO.Category)productOfDO.Category,
                    IsAvailable = (productOfDO.InStock > 0) ? true : false,
                    AmountInCart = cart.OrdersItemsList.FindAll(orderItem => orderItem.ProductId == id).Count(), 
                    Price = productOfDO.Price
                };
                return productItem;
            }
            else
                throw new BO.NotExiestsExceptions("Product request failed");

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
                    Category = (DO.Category)product1.Category,
                };
                Dal.Product.Add(productOfDO);
            }
            catch (DO.NotFoundExceptions str)
            {
                throw new BO.IncorrectDataExceptions("Failed to add product", str);
            }
        }
        else
            throw new BO.IncorrectDataExceptions("The product data received is incorrect");//חוסר תקינות הנתונים שהתקבלו כפרמטר
    }


    public void Delete(int id)
    {
        try
        {
            foreach (DO.Order order in Dal.Order.GetList())//עוברים על כל ההזמנות
            {
                if (Dal.OrderItem.GetListOfOrderItemOfOrder(order.Id).Any(orderItem => orderItem.ProductId != id))//אם המוצר לא נמצא ברשימת פרטי הזמנה בסל
                {
                    Dal.Product.Delete(id);
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
                    Category = (DO.Category)product1?.Category,
                };
                Dal.Product.Update(productOfDO);

            }
            catch (DO.NotFoundExceptions str)
            {
                throw new BO.IncorrectDataExceptions("Failed to update product", str);
            }
        }
        else
            throw new BO.IncorrectDataExceptions("The product data received is incorrect");//חוסר תקינות הנתונים שהתקבלו כפרמטר
    }


}


