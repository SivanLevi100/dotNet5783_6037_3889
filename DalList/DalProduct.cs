
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
    public int addProducts(Product product1)
    {
        for (int i = 0; i < DataSource.Config.ProductFreeIndex; i++)
        {
            if (DataSource.productArray[i].Id == product1.Id)
            {
                throw new FormatException /*Exception*/("no place in arr to add");

            }

        }
        DataSource.productArray[DataSource.Config.ProductFreeIndex++] = product1;

       // DataSource.productArray[++DataSource.Config.ProductFreeIndex] = product1;


        return product1.Id;
    }

    /// <summary>
    /// A request/call method of a single object that receives a product ID number and returns the appropriate product
    /// </summary>
    /// <param name="idProduct1"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product getProduct(int idProduct1)
    {
        for (int i = 0; i < DataSource.Config.ProductFreeIndex; i++)
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
    public Product[] getArrayOfProduct()
    {
        return DataSource.productArray.ToArray();

        //return Array.FindAll(DataSource.productArray, p => p.Id != 0);
    }


    /// <summary>
    /// A method to delete a product object that receives a product ID number
    /// </summary>
    /// <param name="idProduct1"></param>
    public void deleteProduct(int idProduct1)
    {
        for (int i = 0; i < DataSource.productArray.Length; i++)
        {
            if (DataSource.productArray[i].Id == idProduct1)
            {
                for (int j = i; j < DataSource.productArray.Length - 1; j++)
                {
                    DataSource.productArray[j] = DataSource.productArray[j + 1];
                }
                DataSource.Config.ProductFreeIndex--;
                return;
            }
        }

        throw new Exception("The product is not exist in the array");

    }

    /// <summary>
    /// An object update method that will receive a new product
    /// </summary>
    /// <param name="product1"></param>
    public void updateProduct(Product product1)
    {

        for (int i = 0; i < DataSource.Config.ProductFreeIndex; i++)
        {
            if (DataSource.productArray[i].Id == product1.Id)
            {
                DataSource.productArray[i] = product1;
                return;
            }
        }
         throw new Exception("the product id is not exist in array");
    }



}
