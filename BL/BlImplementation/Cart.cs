using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class Cart:ICart
{
    private IDal Dal = new DalList();

    public BO.Cart AddProduct(BO.Cart cart1, int id)
    {

        //DO.Product product
        //Dal.Product.
        //new BO.Cart() {   };//אתחול מהיר
        BO.Cart c=new BO.Cart();
        return c;
        //////////
        ///

    }
    public BO.Cart UpdateAmountOfProduct(BO.Cart cart1, int id, int newAmount)
    {
        BO.Cart c = new BO.Cart();
        return c;
        //////

    }

    public void Confirm(BO.Cart cart1, string CustomerName, string CustomerEmail, string CustomerAdress)
    {

    }





}
