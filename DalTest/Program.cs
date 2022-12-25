//namespace DalApi;
///namespace Dal; 

using DalApi;

using Dal;
using DO;
using System.Diagnostics.CodeAnalysis;

namespace DalTest;

public class Program
{
    static DalApi.IDal? dal = DalApi.Factory.Get();

    static void Main(string[] args)
    {

    int choose;
        Console.WriteLine(" 0 - Exit \n 1 - Product testing \n 2 - Order check \n 3 - Checking an item in an order \n ");
        Console.WriteLine(@"Enter your choice");
        int.TryParse(Console.ReadLine(), out choose);
        
        while (choose != 0)
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
                default:
                    break;
            }
            Console.WriteLine(" 0 - Exit \n 1 - Product testing \n 2 - Order check \n 3 - Checking an item in an order \n ");
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
            try
            {
                switch (ch)
                {
                    case 'c':
                        foreach (var item in dal?.Product.GetAll()??throw new DO.DoesNotExistException("The List Of Product is not exists"))
                        {
                            Console.WriteLine(item);
                        };
                        break;
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
                        id =dal?.Product.Add(myProduct) ?? throw new DO.DoesNotExistException("The Product is not exiests");
                        Console.WriteLine($@"id number of the added product {id}");
                        break;
                    case 'b':
                        Console.WriteLine("please enter: Id of product");
                        id = Int32.Parse(Console.ReadLine() ?? "0");
                        myProduct = dal?.Product.Get(id)?? throw new DO.DoesNotExistException("The Product is not exiests");
                        Console.WriteLine(myProduct);
                        break;
                    case 'd':
                        Console.WriteLine("please enter: id");
                        id = int.Parse(Console.ReadLine() ?? "0");
                        myProduct = dal?.Product.Get(id)??throw new DO.DoesNotExistException("The Product is not exiests");
                        Console.WriteLine(myProduct);

                        Console.WriteLine("Enter new values ​​to update the object"); 
                        Console.WriteLine("please enter: id"); 
                        id = int.Parse(Console.ReadLine() ?? "0");
                        Console.WriteLine("please enter: name");
                        string? name1 = Console.ReadLine();
                        Console.WriteLine("please enter: category");
                        Category category1 = (Category)Enum.Parse(typeof(Category), Console.ReadLine() ?? "Unavailable");
                        Console.WriteLine("please enter: price");
                        double price1 = Double.Parse(Console.ReadLine() ?? "0");
                        Console.WriteLine("please enter: inStock");
                        int instock1 = Int32.Parse(Console.ReadLine() ?? "0");

                        myProduct = new Product
                        {
                            Id = id,
                            Price = price1,
                            Name = name1 ?? "Unknown",
                            Category = category1,
                            InStock = instock1
                        };
                        dal.Product.Update(myProduct);
                        break;
                    case 'e':
                        Console.WriteLine("please enter: Id of product");
                        Int32.TryParse(Console.ReadLine(), out id);
                        dal?.Product.Delete(id);
                        break;
                    default:
                        ch = 'x';
                        break;
                }

            }
            catch (Exception str)
            {
                Console.WriteLine(str.Message);
            }
         }
    }

    /// <summary>
    /// method for each order sub-menu
    /// </summary>
    public static void SubMenuOrder()
    {
        Order myOrder;
        int idOrder;
        string menu = "\ta - Option to add an object to a order list\n";
        menu += "\tb - Object display option by ID\n";
        menu += "\tc - The option to view the list of orders\n";
        menu += "\td - Option to update object data\n";
        menu += "\te - Option to delete an object from a order list";
        char ch = 'a';
        bool flag;

        while (ch != 'x')
        {
            Console.WriteLine(menu);
            flag = Char.TryParse(Console.ReadLine(), out ch);
            try
            {
                switch (ch)
                {
                    case 'a':
                        Console.WriteLine("please enter: Id");
                        idOrder = int.Parse(Console.ReadLine() ?? "0");
                        Console.WriteLine("please enter: CustomerName");
                        string? customerName1 = Console.ReadLine();
                        Console.WriteLine("please enter: CustomerAdress");
                        string? CustomerAdress = Console.ReadLine();
                        Console.WriteLine("please enter: CustomerEmail");
                        string? CustomerEmail = Console.ReadLine();

                        myOrder = new Order
                        {
                            Id = idOrder,
                            CustomerName = customerName1 ?? "Unknown",
                            CustomerAdress = CustomerAdress ?? "Unknown",
                            CustomerEmail = CustomerEmail ?? "Unknown",
                        };
                        idOrder = dal?.Order.Add(myOrder)?? throw new DO.DoesNotExistException("The Order is not exiests");
                        Console.WriteLine($@"id number of the added order {idOrder}");
                        break;
                    case 'b':
                        Console.WriteLine("please enter: Id of order");
                        idOrder = Int32.Parse(Console.ReadLine() ?? "0");
                        myOrder = dal?.Order.Get(idOrder) ?? throw new DO.DoesNotExistException("The Order is not exiests");
                        Console.WriteLine(myOrder);
                        break;
                    case 'c':
                        foreach (Order? item in dal?.Order.GetAll() ?? throw new DO.DoesNotExistException("The Order is not exiests"))
                        {
                            Console.WriteLine(item);
                        };
                        break;
                    case 'd':
                        Console.WriteLine("please enter: Id");
                        idOrder = int.Parse(Console.ReadLine() ?? "0");
                        myOrder = dal?.Order.Get(idOrder) ?? throw new DO.DoesNotExistException("The Order is not exiests");
                        Console.WriteLine(myOrder);

                        Console.WriteLine("Enter new values ​​to update the object");
                        Console.WriteLine("please enter: Id");
                        idOrder = int.Parse(Console.ReadLine() ?? "0");
                        Console.WriteLine("please enter: CustomerName");
                        string? customerName2 = Console.ReadLine();
                        Console.WriteLine("please enter: CustomerAdress");
                        string? CustomerAdress2 = Console.ReadLine();
                        Console.WriteLine("please enter: CustomerEmail");
                        string? CustomerEmail2 = Console.ReadLine();

                        myOrder = new Order
                        {
                            Id = idOrder,
                            CustomerName = customerName2 ?? "Unknown",
                            CustomerAdress = CustomerAdress2 ?? "Unknown",
                            CustomerEmail = CustomerEmail2 ?? "Unknown",
                        };
                        dal.Order.Update(myOrder);
                        break;
                    case 'e':
                        Console.WriteLine("please enter: Id of order");
                        Int32.TryParse(Console.ReadLine(), out idOrder);
                        dal?.Order.Delete(idOrder);
                        break;
                    default:
                        ch = 'x';
                        break;

                }

            }
            catch (Exception str)
            {
                Console.WriteLine(str.Message);
            }
        }
    }

    /// <summary>
    /// Method for sub-menu of each order items
    /// </summary>
    public static void SubMenuOrderItem()
    {
        OrderItem myOrderItem;
        int idOrderItem = 0;
        int idOrderItem1 = 0, idOrderItem2 = 0;
        string menu = "\ta - Option to add an object to a orderItem list\n";
        menu += "\tb - Object display option by ID\n";
        menu += "\tc - The option to view the list of orderItems\n";
        menu += "\td - Option to update object data\n";
        menu += "\te - Option to delete an object from a orderItem list\n";
        menu += "\tf - Request method based on two identifiers (ID) - product ID and order ID\n";
        menu += "\tg - Method of requesting an array of order details by order ID number";

        char ch = 'a';
        bool flag;

        while (ch != 'x')
        {
            Console.WriteLine(menu);
            flag = Char.TryParse(Console.ReadLine(), out ch);
            try
            {
                switch (ch)
                {
                    case 'a':
                        Console.WriteLine("please enter: Id");
                        idOrderItem = int.Parse(Console.ReadLine() ?? "0");
                        Console.WriteLine("please enter: ProductId");
                        int productId = int.Parse(Console.ReadLine() ?? "0");
                        Console.WriteLine("please enter: OrderId");
                        int orderId = int.Parse(Console.ReadLine() ?? "0");
                        Console.WriteLine("please enter: Price");
                        double price = Double.Parse(Console.ReadLine() ?? "0");
                        Console.WriteLine("please enter: Amount");
                        int amount = int.Parse(Console.ReadLine() ?? "0");

                        myOrderItem = new OrderItem
                        {
                            Id = idOrderItem,
                            ProductId = productId,
                            OrderId = orderId,
                            Price = price,
                            Amount = amount,
                        };
                        idOrderItem = dal?.OrderItem.Add(myOrderItem) ?? throw new DO.DoesNotExistException("The OrderItem is not exiests");
                        Console.WriteLine($@"id number of the added orderItem {idOrderItem}");

                        break;
                    case 'b':
                        Console.WriteLine("please enter: Id of orderItem");
                        idOrderItem = int.Parse(Console.ReadLine() ?? "0");
                        myOrderItem = dal?.OrderItem.Get(idOrderItem) ?? throw new DO.DoesNotExistException("The OrderItem is not exiests");
                        Console.WriteLine(myOrderItem);
                        break;
                    case 'c':
                        foreach (OrderItem? item in dal?.OrderItem.GetAll()?? throw new DO.DoesNotExistException("The OrderItem is not exiests"))
                        {
                            Console.WriteLine(item);
                        };
                        break;
                    case 'd':
                        Console.WriteLine("please enter: Id");
                        idOrderItem = int.Parse(Console.ReadLine() ?? "0");
                        myOrderItem = dal?.OrderItem.Get(idOrderItem)?? throw new DO.DoesNotExistException("The OrderItem is not exiests");
                        Console.WriteLine(myOrderItem);

                        Console.WriteLine("Enter new values ​​to update the object");
                        Console.WriteLine("please enter: Id");
                        idOrderItem = int.Parse(Console.ReadLine() ?? "0");
                        Console.WriteLine("please enter: ProductId");
                        int productId1 = int.Parse(Console.ReadLine() ?? "0");
                        Console.WriteLine("please enter: OrderId");
                        int orderId1 = int.Parse(Console.ReadLine() ?? "0");
                        Console.WriteLine("please enter: Price");
                        double price1 = Double.Parse(Console.ReadLine() ?? "0");
                        Console.WriteLine("please enter: Amount");
                        int amount1 = int.Parse(Console.ReadLine() ?? "0");

                        myOrderItem = new OrderItem
                        {
                            Id = idOrderItem,
                            ProductId = productId1,
                            OrderId = orderId1,
                            Price = price1,
                            Amount = amount1,
                        };
                        dal.OrderItem.Update(myOrderItem);
                        break;
                    case 'e':
                        Console.WriteLine("please enter: Id of orderItem");
                        idOrderItem = int.Parse(Console.ReadLine() ?? "0");
                        dal?.OrderItem.Delete(idOrderItem);
                        break;
                    case 'f':
                        Console.WriteLine("please enter: Id of order");
                        idOrderItem1 = int.Parse(Console.ReadLine() ?? "0");
                        Console.WriteLine("please enter: Id of product");
                        idOrderItem2 = int.Parse(Console.ReadLine() ?? "0");
                        myOrderItem = dal?.OrderItem.GetF(orderItem => orderItem?.Value.ProductId == idOrderItem1 && orderItem.Value.ProductId == idOrderItem2) ?? throw new DO.DoesNotExistException("The OrderItem is not exiests");
                        Console.WriteLine(myOrderItem);
                        break;
                    case 'g':
                        Console.WriteLine("please enter: Id of order");
                        int id = int.Parse(Console.ReadLine() ?? "0");
                        foreach (OrderItem item in dal?.OrderItem.GetAll(orderItem => orderItem.Value.OrderId == id) ?? throw new DO.DoesNotExistException("The OrderItem is not exiests"))
                        {
                            Console.WriteLine(item);
                        };
                        break;
                    default:
                        ch = 'x';
                        break;

                }

            }
            catch (Exception str)
            {
                Console.WriteLine(str.Message);
            }
        }
    }


}


