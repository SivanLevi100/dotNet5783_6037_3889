//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using Dal;
using DO;

class Program
{
    static DalProduct dalProduct = new DalProduct();
    static DalOrderItem dalOrderItem = new DalOrderItem();
    static DalOrder dalOrder = new DalOrder();

    static void Main(string[] args)
    {
        int choose;
        //char choose;
        Console.WriteLine(@"
         0 - Exit
         1 - Product testing   
         2 - Order check
         3 - Checking an item in an order ");
        //Console.WriteLine(@"Enter your choice");
        int.TryParse(Console.ReadLine(), out choose);
        while (choose != 0)
        {
            try
            {
                switch (choose)
                {
                    case 0:
                        return;
                    case 1:
                        SubMenuProduct();
                        break;
                    case 2:
                        SubMenuOrder();
                        break;
                    case 3:
                        SubMenuOrderItem();
                        break;
                    // default:
                    default:
                        break;
                }

            }
            catch (Exception str)
            {
                Console.WriteLine(str.Message);
            }
            Console.WriteLine(@"
             0 - Exit
             1 - Product testing   
             2 - Order check
             3 - Checking an item in an order ");
            Console.WriteLine(@"Enter your choice");
            int.TryParse(Console.ReadLine(), out choose);

        }
    }

    /// <summary>
    /// method for each product sub-menu
    /// </summary>
    public static void SubMenuProduct()
    {
        Product myProduct;
        int id;
        //Product[] productsArray = new Product[50];
        //char ch;
        string menu = "\ta - Option to add an object to a product list\n";
        menu += "\tb - Object display option by ID\n";
        menu += "\tc - The option to view the list of products\n";
        menu += "\td - Option to update object data\n";
        menu += "\te - Option to delete an object from a product list";

        char ch= 'a';
        bool flag;
 
        while (ch != 'x')
        {
            Console.WriteLine(menu);
            flag = Char.TryParse(Console.ReadLine(), out ch);
            switch (ch)
            {
                case 'a':
                    Console.WriteLine("please enter: id");
                    id = int.Parse(Console.ReadLine() ?? "0");
                    Console.WriteLine("please enter: name");
                    string? name = Console.ReadLine();
                    Console.WriteLine("please enter: category");
                    Category category = (Category)Enum.Parse(typeof(Category), Console.ReadLine() ?? "Unavailable");
                    Console.WriteLine("please enter: price");
                    double price = Double.Parse(Console.ReadLine() ?? "0");
                    Console.WriteLine("please enter: inStock");
                    int instock = Int32.Parse(Console.ReadLine() ?? "0");

                    myProduct = new Product
                    {
                        Id = id,
                        Price = price,
                        Name = name ?? "Unknown",
                        Category = category,
                        InStock = instock
                    };
                    id = dalProduct.addProducts(myProduct);
                    break;
                case 'b':
                    Console.WriteLine("please enter: Id of product");
                    id = Int32.Parse(Console.ReadLine() ?? "0");
                    //myProduct = DalProduct.getProduct(myIdOfProduct);
                    myProduct = dalProduct.getProduct(id);
                    Console.WriteLine(myProduct);
                    break;
                case 'c':
                    //Product[] productsArray = new Product[50];//////////////////////////

                    //productsArray = DalProduct.getArrayOfProduct();
                    //foreach (Product product1 in productsArray)
                    //{ Console.WriteLine(productsArray); };

                    foreach (Product item in dalProduct.getArrayOfProduct())
                    { 
                        Console.WriteLine(item); 
                    };


                    break;
                case 'd':
                    Console.WriteLine("please enter: id, name, category, price, inStock");
                    id = int.Parse(Console.ReadLine());
                    string? name1 = Console.ReadLine();
                    Category category1 = (Category)Enum.Parse(typeof(Category), Console.ReadLine() ?? "Unavailable");
                    double price1 = Double.Parse(Console.ReadLine() ?? "0.0");
                    int instock1 = Int32.Parse(Console.ReadLine() ?? "0");

                    myProduct = new Product
                    {
                        Id = id,
                        Price = price1,
                        Name = name1 ?? "Unknown",
                        Category = category1,
                        InStock = instock1
                    };
                    dalProduct.updateProduct(myProduct);
                    //  DalProduct.updateProduct(myProduct);
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
                    break;
                case 'e':
                    Console.WriteLine("please enter: Id of product");
                    Int32.TryParse(Console.ReadLine(),out id);
                    //DalProduct.deleteProduct(myIdOfProduct);
                    dalProduct.deleteProduct(id);
                    break;
                default:
                    ch = 'x';
                    break;
            }
         }
    }

    /// <summary>
    /// method for each order sub-menu
    /// </summary>
    public static void SubMenuOrder()
    {
        Order myOrder;
        Order[] orderArray = new Order[100];
        int idOrder = 0;
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
                        idOrder = dalOrder.addOrders(myOrder);
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
                        myOrder = dalOrder.getOrder(idOrder);
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
                        dalOrder.updateOrder(myOrder);
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
                        dalOrder.deleteOrder(idOrder);
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
        Order myOrder = new Order();
        OrderItem myOrderItem;
        OrderItem[] orderItemArray = new OrderItem[200];
        int idOrderItem = 0;
        int idOrderItem1 = 0, idOrderItem2 = 0;
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


