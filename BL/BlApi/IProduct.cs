using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IProduct
{
    //להוסיף מתודות
    public IEnumerable<Product> GetProductList();
    public Product GetProductDetailsManager();
    public Product GetProductDetailsBuyer();
    public void Add(Product product1);
    public void Delete(int id);
    public void Update(Product product1);


}
