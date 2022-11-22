using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using BO;
namespace BlApi;

public interface ICart
{

    public Cart AddProduct(Cart cart1, int id);
    public Cart UpdateAmountOfProduct(Cart cart1, int id, int newAmount);

    public void Confirm(Cart cart1, string CustomerName, string CustomerEmail,string CustomerAdress);


    //void AddItem();
    //void RemoveItem();

    //להוסיף מתודות

}
