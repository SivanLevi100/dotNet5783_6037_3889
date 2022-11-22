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

    public void Confirm(Cart cart1, int dbxd);///dbxd= פרטי קונה


    //void AddItem();
    //void RemoveItem();

    //להוסיף מתודות

}
