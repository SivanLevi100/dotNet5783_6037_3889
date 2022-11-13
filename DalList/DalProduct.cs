
using DO;

namespace Dal;

public class DalProduct
{
    public int addProducts(Product product1)
    {
       for (int i = 0; i < DataSource.Config.ProductFreeIndex; i++)
        {
            if(DataSource.productArray[i].Id==product1.Id)
            {
                //throw new Exception("no place in arr to add");
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

    public  Product getProduct(int idProduct1)
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

    public Product[] getArrayOfProduct()
    {
        Product[] newProductArray = new Product[DataSource.Config.ProductFreeIndex];
        for (int i = 0; i < DataSource.Config.ProductFreeIndex; i++)
        {
            newProductArray[i] = DataSource.productArray[i];
        }
        return newProductArray;
    }

    public void deleteProduct(int idProduct1)
    {
        int indexOfProduct = 0;
        for (int i = 0; i < DataSource.Config.ProductFreeIndex; i++)
        {

            if (DataSource.productArray[i].Id == idProduct1)
            {
                indexOfProduct = i;

                /////////////מחיקה
            }
        }


    }

    public void updateProduct(Product product1)
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
    }



}
