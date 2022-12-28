
using DalApi;
using DO;

namespace Dal;


internal class DalProduct : IProduct
{
    DataSource ds = DataSource.S_instance;

    /// <summary>
    /// An add object method that receives a product object and returns the ID number of the added product
    /// </summary>
    /// <param name="product1"></param>
    /// <returns></returns>
    /// <exception cref="DuplicateIdExceptions"></exception>
    public int Add(Product product1)
    {
        if (ds.ProductList.Exists(x => x?.Id == product1.Id))
            throw new DuplicateIdExceptions("No place in List to add");
        ds.ProductList.Add(product1);
        return product1.Id;
    }

    /// <summary>
    /// A request/call method of a single object that receives a product ID number and returns the appropriate product
    /// </summary>
    /// <param name="idProduct1"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundExceptions"></exception>
    public Product? Get(int idProduct1)
    {
        return ds.ProductList?.FirstOrDefault(product => ((DO.Product)product!).Id== idProduct1)
            ?? throw new NotFoundExceptions("The product id is not exist in List");
    }

    public Product GetF(Func<Product?, bool>? filter)
    {
        return ds.ProductList?.FirstOrDefault(product => filter(product))
           ?? throw new NotFoundExceptions("The product id is not exist in List");
    }


    /// <summary>
    /// Request/read method of the list of all objects of a product
    /// </summary>
    /// <returns></returns>
    public IEnumerable<DO.Product?> GetAll(Func<Product?, bool>? filter)=>
        (filter == null ? ds.ProductList?.Select(item => item)
        : ds.ProductList?.Where(item => filter(item))
        ?? throw new DoesNotExistException("Missing product"))
        ?? throw new DoesNotExistException("Missing product list");


    /// <summary>
    /// A method to delete a product object that receives a product ID number
    /// </summary>
    /// <param name="idProduct1"></param>
    public void Delete(int idProduct1)
    {
        if (ds.ProductList?.RemoveAll(product => ((DO.Product)product!).Id == idProduct1) == 0)
            throw new NotFoundExceptions("The product is not exist in the List");
    }


    /// <summary>
    /// An object update method that will receive a new product
    /// </summary>
    /// <param name="product1"></param>
    public void Update(Product product1)
    {
        if (ds.ProductList.Exists(x => x?.Id == product1.Id))
        {
            ds.ProductList.RemoveAll(product => ((DO.Product)product!).Id == product1.Id);
            ds.ProductList.Add(product1);
            return;
        }
        throw new NotFoundExceptions("the product id is not exist in List");
    }



}
