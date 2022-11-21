
using DalApi;
using DO;

namespace Dal;


internal class DalProduct:IProduct
{
    /// <summary>
    /// An add object method that receives a product object and returns the ID number of the added product
    /// </summary>
    /// <param name="product1"></param>
    /// <returns></returns>
    /// <exception cref="DuplicateIdExceptions"></exception>
    public int Add(Product product1)
    {
        if (DataSource.productList.Exists(x => x.Id== product1.Id))
        {
            throw new DuplicateIdExceptions ("no place in List to add");
        }
        DataSource.productList.Add(product1);
        return product1.Id;
    }

    /// <summary>
    /// A request/call method of a single object that receives a product ID number and returns the appropriate product
    /// </summary>
    /// <param name="idProduct1"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundExceptions"></exception>
    public Product Get(int idProduct1)
    {
        if (DataSource.productList.Exists(x => x.Id== idProduct1))
            return DataSource.productList.Find(x => x.Id== idProduct1);
        throw new NotFoundExceptions("the product id is not exist in List");
    }

    /// <summary>
    /// Request/read method of the list of all objects of a product
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product> GetList()
    {
        return DataSource.productList.ToList();

        //return Array.FindAll(DataSource.productArray, p => p.Id != 0);
    }


    /// <summary>
    /// A method to delete a product object that receives a product ID number
    /// </summary>
    /// <param name="idProduct1"></param>
    public void Delete(int idProduct1)
    {
        if (DataSource.productList.Exists(x => x.Id == idProduct1))
        {
            Product productDelete = DataSource.productList.Find(x => x.Id == idProduct1);
            DataSource.productList.Remove(productDelete);
            return;
        }
        throw new NotFoundExceptions("The product is not exist in the List");
    }

    /// <summary>
    /// An object update method that will receive a new product
    /// </summary>
    /// <param name="product1"></param>
    public void Update(Product product1)
    {
        if (DataSource.productList.Exists(x => x.Id == product1.Id))
        {
            int j = DataSource.productList.IndexOf(DataSource.productList.Find(x => x.Id == product1.Id));
            DataSource.productList[j] = product1;
            return;
        }
        throw new NotFoundExceptions("the product id is not exist in List");

        //for (int i = 0; i < DataSource.productList.Count(); i++)
        //{
        //    if (DataSource.productList.Exists(x => x.Id == product1.Id))
        //    {
        //        int j = DataSource.productList.IndexOf(DataSource.productList.Find(x => x.Id == product1.Id));
        //        DataSource.productList[j] = product1;
        //        return;
        //    }
        //}
        //throw new NotFoundExceptions("the product id is not exist in array");

    }



}
