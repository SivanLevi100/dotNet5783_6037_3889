//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using Dal;
using DO;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

class Program
{
    private DalProduct product = new DalProduct();
    private DalOrder order = new DalOrder();
    private DalOrderItem orderItem = new DalOrderItem();

    static void Main(string[] args)
    {
        //int choose;
        char choose;
        Console.WriteLine(@"0 - Exit
         1 - Product testing   
         2 - Order check
         3 - Checking an item in an order ");
        Console.WriteLine(@"Enter your choice");
        choose = (char)Console.Read();
        switch(choose)
        {
            case '0':
                return;
            case '1':
                SubMenuProduct();
                break;
            case '2':
                SubMenuOrder();
                break;
            case '3':
                SubMenuOrderItem();
                break;
            // default:
            default:
                break;
        }
        choose = (char)Console.Read();
    }

    /// <summary>
    /// method for each product sub-menu
    /// </summary>
    public static void SubMenuProduct()
    {
        Product myProduct = new Product();
        int myIdOfProduct;
        //Product[] productsArray = new Product[50];
        //char ch;
        Console.WriteLine(@"a - Option to add an object to a product list
            b - Object display option by ID 
            c - The option to view the list of products
            d - Option to update object data
            e - Option to delete an object from a product list");
        char ch;
        ch = (char)Console.Read();

        //ch= Console.Read();
        while (ch!=0)
        {
            switch (ch)
            {
                case '1':
                    try
                    {
                        Console.WriteLine("please enter: name, category, price, inStock");
                        myProduct.Id = Console.Read();
                        myProduct.Name = Console.ReadLine();
                        myProduct.Category = Console.ReadLine();
                        myProduct.Price = Console.Read();
                        myProduct.InStock = Console.Read();
                        myIdOfProduct = DalProduct.addProducts(myProduct);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case '2':
                    Console.WriteLine("please enter: Id of product");
                    myIdOfProduct = Console.Read();
                    try
                    {
                        myProduct = DalProduct.getProduct(myIdOfProduct);
                        Console.WriteLine(myProduct);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case '3':
                    Product[] productsArray = new Product[50];//////////////////////////

                    productsArray = DalProduct.getArrayOfProduct();
                    foreach (Product product1 in productsArray)
                    { Console.WriteLine(productsArray); };
                    break;
                case '4':
                    Console.WriteLine("please enter: name, category, price, inStock");
                    myProduct.Id = Console.Read();//כי זה מוגרל
                    myProduct.Name = Console.ReadLine();
                    myProduct.Category = Console.ReadLine();
                    myProduct.Price = Console.Read();
                    myProduct.InStock = Console.Read();
                    try
                    {
                        DalProduct.updateProduct(myProduct);
                        Console.WriteLine(myProduct);
                        Product newProduct = new Product();                     //קולט ערכים חדשים
                        Console.WriteLine("Enter new Product");
                        // newProduct.Id=Console.Read();//כי זה מוגרל
                        newProduct.Name = Console.ReadLine();
                        newProduct.Category = Console.ReadLine();
                        newProduct.Price = Console.Read();
                        newProduct.InStock = Console.Read();
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case '5':
                    Console.WriteLine("please enter: Id of product");
                    myIdOfProduct = Console.Read();
                    try
                    {
                        DalProduct.deleteProduct(myIdOfProduct);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                default:
                    break;
            }

        }
    }

    /// <summary>
    /// method for each order sub-menu
    /// </summary>
    public static void SubMenuOrder()
    {
        Order myOrder=new Order();
        Order[] orderArray = new Order[100];
        int idOrder=0;
        char ch;
        Console.WriteLine(@"a - Option to add an object to an order list
            b - Object display option by ID 
            c - The option to view the list of orders
            d - Option to update object data
            e - Option to delete an object from an order list");
        ch = (char)Console.Read();
        while (ch != 0)
        {
            switch (ch)
            {
                case 'a':
                    try
                    {
                        Console.WriteLine("please enter: Id, CustomerName, CustomerAdress, CustomerEmail");
                        //myOrder.Id = Console.Read(); //כי זה רץ אוטומטי
                        myOrder.CustomerName = Console.ReadLine();
                        myOrder.CustomerAdress = Console.ReadLine();
                        myOrder.CustomerEmail = Console.ReadLine();
                        idOrder = DalOrder.addOrders(myOrder);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case 'b':
                    Console.WriteLine("please enter: Id of order");
                    idOrder = Console.Read();
                    try
                    {
                        myOrder = DalOrder.getOrder(idOrder);
                        Console.WriteLine(myOrder);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case 'c':
                    orderArray = DalOrder.getArrayOfOrder();
                    foreach (Order oredr1 in orderArray)
                    { Console.WriteLine(orderArray); };
                    break;
                case 'd':
                    Console.WriteLine("please enter: Id, CustomerName, CustomerAdress, CustomerEmail");
                    //myOrder.Id = Console.Read();//כי זה רץ אוטומטי
                    myOrder.CustomerName = Console.ReadLine();
                    myOrder.CustomerAdress = Console.ReadLine();
                    myOrder.CustomerEmail = Console.ReadLine();
                    try
                    {
                        DalOrder.updateOrder(myOrder);
                        Console.WriteLine(myOrder);
                        Order newOrder = new Order();                     //קולט ערכים חדשים
                        Console.WriteLine("Enter new Order");
                        //newOrder.Id = Console.Read();//כי זה רץ אוטומטי
                        newOrder.CustomerName = Console.ReadLine();
                        newOrder.CustomerAdress = Console.ReadLine();
                        newOrder.CustomerEmail = Console.ReadLine();
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case 'e':
                    Console.WriteLine("please enter: Id of order");
                    idOrder = Console.Read();
                    try
                    {
                        DalOrder.deleteOrder(idOrder);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
            }
        }


    }

    /// <summary>
    /// Method for sub-menu of each order items
    /// </summary>
    public static void SubMenuOrderItem()
    {
        Order myOrder=new Order();  
        OrderItem myOrderItem = new OrderItem();
        OrderItem[] orderItemArray = new OrderItem[200];
        int idOrderItem = 0;
        int idOrderItem1=0, idOrderItem2=0;
        char ch;
        Console.WriteLine(@"a - Option to add an object to an orderItem list
            b - Object display option by ID 
            c - The option to view the list of orderItems
            d - Option to update object data
            e - Option to delete an object from an orderItem list
            f - Object display option according to product ID and order ID
            g - The option to display the list of an entity according to the ID number of the order ");
        ch = (char)Console.Read();
        while (ch != 0)
        {
            switch (ch)
            {
                case 'a':
                    try
                    {
                        Console.WriteLine("please enter: Id, ProductId, OrderId, Price,Amount");
                        //myOrderItem.Id = Console.Read();
                        myOrderItem.ProductId = Console.Read();
                        myOrderItem.OrderId = Console.Read();
                        myOrderItem.Price = Console.Read();
                        //myOrderItem.Amount = Console.Read();
                        idOrderItem = DalOrderItem.addOrderItems(myOrderItem);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case 'b':
                    Console.WriteLine("please enter: Id of orderItem");
                    idOrderItem = Console.Read();
                    try
                    {
                        myOrderItem = DalOrderItem.getOrderItem(idOrderItem);
                        Console.WriteLine(myOrderItem);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case 'c':
                    orderItemArray = DalOrderItem.getArrayOfOrderItem();
                    foreach (OrderItem oredrItem1 in orderItemArray)
                    { Console.WriteLine(orderItemArray); };
                    break;
                case 'd':
                    Console.WriteLine("please enter: Id, ProductId, OrderId, Price,Amount");
                    myOrderItem.Id = Console.Read();
                    myOrderItem.ProductId = Console.Read();
                    myOrderItem.OrderId = Console.Read();
                    myOrderItem.Price = Console.Read();
                    myOrderItem.Amount = Console.Read();

                    try
                    {
                        DalOrderItem.updateOredrItem(myOrderItem);
                        Console.WriteLine(myOrderItem);
                        OrderItem newOrderItem = new OrderItem();   //קולט ערכים חדשים
                        Console.WriteLine("Enter new OrderItem");
                        newOrderItem.Id = Console.Read();
                        newOrderItem.ProductId = Console.Read();
                        newOrderItem.OrderId = Console.Read();
                        newOrderItem.Price = Console.Read();
                        newOrderItem.Amount = Console.Read();
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case 'e':
                    Console.WriteLine("please enter: Id of orderItem");
                    idOrderItem = Console.Read();
                    try
                    {
                        DalOrderItem.deleteOrderItem(idOrderItem);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case 'f':
                    Console.WriteLine("please enter: Id of order and Id of product");
                    idOrderItem1 = Console.Read();
                    idOrderItem2 = Console.Read();
                    try
                    {
                        myOrderItem = DalOrderItem.getOrderItemofTwoId(idOrderItem1, idOrderItem2);
                        Console.WriteLine(myOrderItem);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case 'g':
                    orderItemArray = DalOrderItem.getArrayOfOrderItemOfOrder(myOrder);
                    foreach (OrderItem oredrItem1 in orderItemArray)
                    { Console.WriteLine(orderItemArray); };
                    break;
            }

        }
    }


}


