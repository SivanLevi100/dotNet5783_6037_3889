
using DO;

namespace Dal;

public class DalProduct
{
    /// <summary>
    /// An add object method that receives a product object and returns the ID number of the added product
    /// </summary>
    /// <param name="product1"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static int addProducts(Product product1)
    {
       for (int i = 0; i < DataSource.Config.ProductFreeIndex; i++)
        {
            if(DataSource.productArray[i].Id==product1.Id)
            {
                throw new Exception("no place in arr to add");
            }
          
        }
        DataSource.productArray[DataSource.Config.ProductFreeIndex++] = new Product()
        {
            Id = product1.Id,
            Name = product1.Name,
            Category = product1.Category,
            Price = product1.Price,
            InStock = product1.InStock
        };
        return product1.Id;
    }

    /// <summary>
    /// A request/call method of a single object that receives a product ID number and returns the appropriate product
    /// </summary>
    /// <param name="idProduct1"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static Product getProduct(int idProduct1)
    {
       //bool flag = false;
      //  Product p=new Product();
        for (int i=0; i < DataSource.Config.ProductFreeIndex; i++)
        {

            if (DataSource.productArray[i].Id == idProduct1)
            {
                return DataSource.productArray[i];

            }
        }  
        throw new Exception("the product id is not exist in array");
    }

    /// <summary>
    /// Request/read method of the list of all objects of a product
    /// </summary>
    /// <returns></returns>
    public static Product[] getArrayOfProduct()
    {
        Product[] newProductArray = new Product[DataSource.Config.ProductFreeIndex];
        for (int i = 0; i < DataSource.Config.ProductFreeIndex; i++)
        {
            newProductArray[i] = DataSource.productArray[i];
        }
        return newProductArray;
    }


    /// <summary>
    /// A method to delete a product object that receives a product ID number
    /// </summary>
    /// <param name="idProduct1"></param>
    public static void deleteProduct(int idProduct1)
    {
        int indexOfProduct = 0;
        for (int i = 0; i < DataSource.Config.ProductFreeIndex; i++)
        {

            if (DataSource.productArray[i].Id == idProduct1)
            {
                indexOfProduct = i;

                /////////////מחיקה
            }
            //להזיז את האוביקטים אחרורה ואת האחרון למחוק ע"י דריבה באוביקט חדש ולהקטין את המצביע לסוף המערך ב1


        }


    }

    /// <summary>
    /// An object update method that will receive a new product
    /// </summary>
    /// <param name="product1"></param>
    public static void updateProduct(Product product1)
    {

        for (int i = 0; i < DataSource.Config.ProductFreeIndex; i++)
        {
            if(DataSource.productArray[i].Id== product1.Id)
            {
                DataSource.productArray[i].Id = product1.Id;
                DataSource.productArray[i].Name = product1.Name;
                DataSource.productArray[i].Category= product1.Category;
                DataSource.productArray[i].Price = product1.Price;
                DataSource.productArray[i].InStock = product1.InStock;
            }
        }
        throw new Exception("the product id is not exist in array");

    }



}
