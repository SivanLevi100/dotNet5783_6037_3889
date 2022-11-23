using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        if(id > 0)
        {

            BO.Product product = new BO.Product();
            product.Id = Dal.Product.Get(id).Id;
            product.Name = Dal.Product.Get(id).Name;
            product.Category = (BO.Category)Dal.Product.Get(id).Category;
            product.Price = Dal.Product.Get(id).Price;

            return product;

        }
        if (id < 0)
        {
            throw new NotExiestsExceptions("The Product is not Exiests");
        }



    }
    public BO.Product GetProductDetailsBuyer()
    {

    }
    public void Add(Product product1)
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
