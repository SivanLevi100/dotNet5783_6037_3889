using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

sealed internal class Bl:IBl
{
    public Bl()
    {
        Product = new Product();
        Order = new Order();
        Cart = new Cart();
    }
    public IProduct Product { get; }
    public IOrder Order { get;}
    public ICart Cart { get; }

    //public IProduct Product => new Product();
    //public IOrder Order => new Order();
    //public ICart Cart  => new Cart();

}
