using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using BlApi;
using BO;
using Dal;
using DalApi;
using DO;

namespace BlImplementation;

internal class Product:IProduct
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

        if (id > 0)
        {
            DO.Product productOfDo= Dal.Product.Get(id);
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
            throw new NotExiestsExceptions("The Product is not Exiests");
    }


    public BO.ProductItem GetProductDetailsBuyer(int id, BO.Cart cart)
    {
        if (id > 0)
        {
            try
            {
                DO.Product productOfDo = Dal.Product.Get(id);
                BO.ProductItem productItem = new BO.ProductItem
                {
                    IdProduct = productOfDo.Id,
                    Name = productOfDo.Name,
                    Category = (BO.Category)productOfDo.Category,
                    IsAvailable = (productOfDo.InStock > 0) ? true : false,
                    AmountInCart = //

                };
                return productItem;
            }
            catch (NotFoundExceptions s)
            {
                Console.WriteLine(s);
            }
        }
        else
            throw new NotExiestsExceptions("The Product is not Exiests");
    }


    public void Add(BO.Product product1)
    {
        //Dal.Product.GetList();

    }
    public void Delete(int id)
    {

    }
    public void Update(Product product1)
    {

    }
}
