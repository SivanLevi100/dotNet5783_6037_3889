using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class Product:IProduct
{
    private IDal Dal = new DalList();


    public IEnumerable<BO.Product> GetProductList()
    {
        //int i = 0;
        //IEnumerable<DO.Product> newProduct=Dal.Product.GetList();
        //newProduct.ToArray()
        //new BO.Product() { Id = newProduct.First().Id }

       /// Dal=DalApi.IDal.Product.Get();
        // DO.Product product1 = Dal.Product.Get();
        //new BO.ProductForList() { }
    }
    public BO.Product GetProductDetailsManager()
    {

    }
    public BO.Product GetProductDetailsBuyer()
    {

    }
    public void Add(Product product1)
    {
        Dal.Product.GetList();
    }
    public void Delete(int id)
    {

    }
    public void Update(Product product1)
    {

    }
}
