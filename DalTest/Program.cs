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
        Product myProduct;
        int myIdOfProduct;
        //Product[] productsArray = new Product[50];
        //char ch;
        Console.WriteLine(@"a - Option to add an object to a product list
            b - Object display option by ID 
            c - The option to view the list of products
            d - Option to update object data
            e - Option to delete an object from a product list");
        int ch;
        ch= Console.Read();
        while (ch!=0)
        {
            switch (ch)
            {
                case 'a':
                    Console.WriteLine("please enter: id, name, category, price, inStock");
                    myIdOfProduct = int.Parse(Console.ReadLine());
                   // myIdOfProduct = Int32.Parse(Console.ReadLine());
                    string? name = Console.ReadLine();
                    Category category = (Category)Enum.Parse(typeof(Category), Console.ReadLine() ?? "Unavailable");
                    double price = Double.Parse(Console.ReadLine());
                    int instock = Int32.Parse(Console.ReadLine());

                    myProduct = new Product
                    {
                        //Id = myIdOfProduct,
                        Price = price,
                        Name = name ?? "Unknown",
                        Category = category,
                        InStock = instock
                    };
                    try
                    {
                        myIdOfProduct = DalProduct.addProducts(myProduct);
                    }
                    catch (FormatException str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case 'b':
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
                case 'c':
                    Product[] productsArray = new Product[50];//////////////////////////

                    productsArray = DalProduct.getArrayOfProduct();
                    foreach (Product product1 in productsArray)
                    { Console.WriteLine(productsArray); };
                    break;
                case 'd':
                    Console.WriteLine("please enter: id, name, category, price, inStock");
                    myIdOfProduct = int.Parse(Console.ReadLine());
                    string? name1 = Console.ReadLine();
                    Category category1 = (Category)Enum.Parse(typeof(Category), Console.ReadLine()?? "Unavailable");
                    double price1 = Double.Parse(Console.ReadLine() ?? "0.0");
                    int instock1 = Int32.Parse(Console.ReadLine() ?? "0");

                    myProduct = new Product
                    {
                        Id = myIdOfProduct,
                        Price = price1,
                        Name = name1 ?? "Unknown",
                        Category = category1,
                        InStock = instock1
                    };

                    try
                    {
                        DalProduct.updateProduct(myProduct);
                        Console.WriteLine(myProduct);
                        Product newProduct = new Product();                     //קולט ערכים חדשים
                        Console.WriteLine("Enter new Product");

                        //myIdOfProduct = int.Parse(Console.ReadLine());
                        //string? name = Console.ReadLine();
                        //Category category = (Category)Enum.Parse(typeof(Category), Console.ReadLine() ?? "Unavailable");
                        //double price = Double.Parse(Console.ReadLine() ?? "0.0");
                        //int instock = Int32.Parse(Console.ReadLine() ?? "0");

                        //myProduct = new Product
                        //{
                        //    Id = myIdOfProduct,
                        //    Price = price,
                        //    Name = name ?? "Unknown",
                        //    Category = category,
                        //    InStock = instock
                        //};
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case 'e':
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
            //int.TryParse(Console.ReadLine(), out ch);
            ch = Console.Read();

        }
    }

    /// <summary>
    /// method for each order sub-menu
    /// </summary>
    public static void SubMenuOrder()
    {
        Order myOrder;
        Order[] orderArray = new Order[100];
        int idOrder=0;
        //char ch;
        Console.WriteLine(@"a - Option to add an object to an order list
            b - Object display option by ID 
            c - The option to view the list of orders
            d - Option to update object data
            e - Option to delete an object from an order list");
        // ch = (char)Console.Read();
        int ch;
        ch = Console.Read();
        while (ch != 0)
        {
            switch (ch)
            {
                case 'a':
                    Console.WriteLine("please enter: Id, CustomerName, CustomerAdress, CustomerEmail");
                    idOrder = int.Parse(Console.ReadLine());
                    string? customerName1 = Console.ReadLine();
                    string? CustomerEmail = Console.ReadLine();
                    string? CustomerAdress = Console.ReadLine();

                    myOrder = new Order
                    {
                        Id = idOrder,
                        CustomerName = customerName1,
                        CustomerAdress = CustomerAdress,
                        CustomerEmail = CustomerEmail,
                    };
                    try
                    {
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
                    idOrder = int.Parse(Console.ReadLine());
                    string? customerName2 = Console.ReadLine();
                    string? CustomerEmail2 = Console.ReadLine();
                    string? CustomerAdress2 = Console.ReadLine();

                    myOrder = new Order
                    {
                        Id = idOrder,
                        CustomerName = customerName2,
                        CustomerAdress = CustomerAdress2,
                        CustomerEmail = CustomerEmail2,
                    };
                    try
                    {
                        DalOrder.updateOrder(myOrder);
                        Console.WriteLine(myOrder);
                        //Order newOrder = new Order();                     //קולט ערכים חדשים
                       // Console.WriteLine("Enter new Order");
                       
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
            ch = Console.Read();

        }


    }

    /// <summary>
    /// Method for sub-menu of each order items
    /// </summary>
    public static void SubMenuOrderItem()
    {
        Order myOrder=new Order();  
        OrderItem myOrderItem ;
        OrderItem[] orderItemArray = new OrderItem[200];
        int idOrderItem = 0;
        int idOrderItem1=0, idOrderItem2=0;
        //char ch;
        Console.WriteLine(@"a - Option to add an object to an orderItem list
            b - Object display option by ID 
            c - The option to view the list of orderItems
            d - Option to update object data
            e - Option to delete an object from an orderItem list
            f - Object display option according to product ID and order ID
            g - The option to display the list of an entity according to the ID number of the order ");
        // ch = (char)Console.Read();
        int ch;
        ch = Console.Read();

        while (ch != 0)
        {
            switch (ch)
            {
                case 'a':
                    Console.WriteLine("please enter: Id, ProductId, OrderId, Price,Amount");
                    idOrderItem = int.Parse(Console.ReadLine());
                    int productId = int.Parse(Console.ReadLine());
                    int orderId = int.Parse(Console.ReadLine());
                    double price = double.Parse(Console.ReadLine());
                    int amount = int.Parse(Console.ReadLine());

                    myOrderItem = new OrderItem
                    {
                        Id = idOrderItem,
                        ProductId = productId,
                        OrderId = orderId,
                        Price = price,
                        Amount = amount,
                    };
                    try
                    {
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
                    idOrderItem = int.Parse(Console.ReadLine());
                    int productId1 = int.Parse(Console.ReadLine());
                    int orderId1 = int.Parse(Console.ReadLine());
                    double price1 = double.Parse(Console.ReadLine());
                    int amount1 = int.Parse(Console.ReadLine());

                    myOrderItem = new OrderItem
                    {
                        Id = idOrderItem,
                        ProductId = productId1,
                        OrderId = orderId1,
                        Price = price1,
                        Amount = amount1,
                    };

                    try
                    {
                        DalOrderItem.updateOredrItem(myOrderItem);
                        Console.WriteLine(myOrderItem);
                        //OrderItem newOrderItem = new OrderItem();   //קולט ערכים חדשים
                        //Console.WriteLine("Enter new OrderItem");
                        
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
            ch = Console.Read();


        }
    }


}


