using BlApi;
using BlImplementation;
using BO;
using Dal;
using DalApi;

namespace BlTest;

class Program  /*internal*/
{
    static BlApi.IBl? bl = BlApi.Factory.Get();
    static void Main(string[] args)
    {
        int choose;
        Console.WriteLine(" 0 - Exit \n 1 - Product testing \n 2 - Order testing \n 3 - Cart testing \n ");
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
                    SubMenuCart();
                    break;
                default:
                    break;
            }
            Console.WriteLine(" 0 - Exit \n 1 - Product testing \n 2 - Order testing \n 3 - Cart testing \n ");
            Console.WriteLine(@"Enter your choice");
            int.TryParse(Console.ReadLine(), out choose);

        }
    }

    ////////////////
    public static void SubMenuProduct()
    {
        int id;
        BO.Product myProduct;
        BO.ProductItem myProductItem;
        BO.Cart myCart = new BO.Cart()
        {
            TotalPrice = 0,
            OrdersItemsList = null,
            CustomerName = "David",
            CustomerAdress = "Jerusalem",
            CustomerEmail = "david@gmail.com"
        };
        string menu = "\ta - The option to view the list of products\n";
        menu += "\tb - Object display option by ID (for manager screen)\n";
        menu += "\tc - Object display option by ID (for buyer's catalog screen)\n";
        menu += "\td - Option to add an object to a product list\n";
        menu += "\te - Option to update object data\n ";
        menu += "\tf - Option to delete an object from a product list";

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
                        foreach (BO.ProductForList item in bl.Product.GetProductList())
                        {
                            Console.WriteLine(item);
                        };
                        break;
                    case 'b':
                        Console.WriteLine("please enter: Id of product");
                        id = Int32.Parse(Console.ReadLine() ?? "0");
                        myProduct = bl.Product.GetProductDetailsManager(id);
                        Console.WriteLine(myProduct);
                        break;
                    case 'c':
                        Console.WriteLine("please enter: Id of product");
                        id = Int32.Parse(Console.ReadLine() ?? "0");
                        myProductItem = bl.Product.GetProductDetailsBuyer(id, myCart);
                        Console.WriteLine(myProductItem);
                        break;
                    case 'd':
                        Console.WriteLine("please enter: id");
                        id = int.Parse(Console.ReadLine() ?? "0");
                        Console.WriteLine("please enter: name");
                        string? name = Console.ReadLine();
                        Console.WriteLine("please enter: category");
                        BO.Category category = (BO.Category)Enum.Parse(typeof(BO.Category), Console.ReadLine() ?? "Unavailable");
                        Console.WriteLine("please enter: price");
                        double price = Double.Parse(Console.ReadLine() ?? "0");
                        Console.WriteLine("please enter: inStock");
                        int instock = Int32.Parse(Console.ReadLine() ?? "0");

                        myProduct = new BO.Product
                        {
                            Id = id,
                            Price = price,
                            Name = name ?? "Unknown",
                            Category = category,
                            InStock = instock
                        };
                        bl?.Product.Add(myProduct);
                        Console.WriteLine($@"id number of the added product {id}");
                        break;
                    case 'e':
                        Console.WriteLine("please enter: id");
                        id = int.Parse(Console.ReadLine() ?? "0");
                        myProduct = bl.Product.GetProductDetailsManager(id);
                        Console.WriteLine(myProduct);

                        Console.WriteLine("Enter new values ​​to update the object");
                        Console.WriteLine("please enter: id");
                        id = int.Parse(Console.ReadLine() ?? "0");
                        Console.WriteLine("please enter: name");
                        string? name1 = Console.ReadLine();
                        Console.WriteLine("please enter: category");
                        BO.Category category1 = (BO.Category)Enum.Parse(typeof(BO.Category), Console.ReadLine() ?? "Unavailable");
                        Console.WriteLine("please enter: price");
                        double price1 = Double.Parse(Console.ReadLine() ?? "0");
                        Console.WriteLine("please enter: inStock");
                        int instock1 = Int32.Parse(Console.ReadLine() ?? "0");

                        myProduct = new BO.Product
                        {
                            Id = id,
                            Price = price1,
                            Name = name1 ?? "Unknown",
                            Category = category1,
                            InStock = instock1
                        };
                        bl?.Product.Update(myProduct);
                        break;
                    case 'f':
                        Console.WriteLine("please enter: Id of product");
                        Int32.TryParse(Console.ReadLine(), out id);
                        bl?.Product.Delete(id);
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


    public static void SubMenuOrder()
    {
        int id;
        BO.Order myOrder;
        BO.OrderTracking myOrderTracking;
        string menu = "\ta - The option to view the list of orders\n";
        menu += "\tb - Object display option by ID\n";
        menu += "\tc - Option to update delivery object data\n";
        menu += "\td - Option to update shipping object data\n";
        menu += "\te - Option to Tracking object data";

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
                        foreach (BO.OrderForList item in bl.Order.GetOrderList())
                        {
                            Console.WriteLine(item);
                        };
                        break;
                    case 'b':
                        Console.WriteLine("please enter: Id of order");
                        id = Int32.Parse(Console.ReadLine() ?? "0");
                        myOrder = bl.Order.GetOrderDetails(id);
                        Console.WriteLine(myOrder);
                        break;
                    case 'c':
                        Console.WriteLine("please enter: id");
                        id = int.Parse(Console.ReadLine() ?? "0");
                        myOrder = bl.Order.UpdateDelivery(id);
                        Console.WriteLine(myOrder);
                        break;
                    case 'd':
                        Console.WriteLine("please enter: id");
                        id = int.Parse(Console.ReadLine() ?? "0");
                        myOrder = bl.Order.UpdateShipping(id);
                        Console.WriteLine(myOrder);
                        break;
                    case 'e':
                        Console.WriteLine("please enter: id");
                        id = int.Parse(Console.ReadLine() ?? "0");
                        myOrderTracking = bl.Order.Tracking(id);
                        Console.WriteLine(myOrderTracking);
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

    public static void SubMenuCart()
    {
        BO.Cart myCart = new BO.Cart()
        {
            CustomerName = "David",
            CustomerAdress = "Jerusalem",
            CustomerEmail = "david@gmail.com"
        };
        int id;
        int amount;
        string menu = "\ta - Option to add an object to the cart\n";
        menu += "\tb - Option to update amount Of Product\n";
        menu += "\tc - Option to confirm the order";

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
                        Console.WriteLine("please enter: id");
                        id = int.Parse(Console.ReadLine() ?? "0");
                        myCart = bl.Cart.AddProduct(myCart, id);
                        Console.WriteLine(myCart);
                        Console.WriteLine("The product has been added to the shopping cart\n");
                        break;
                    case 'b':
                        Console.WriteLine("please enter: id");
                        id = int.Parse(Console.ReadLine() ?? "0");
                        Console.WriteLine("please enter: new amount");
                        amount = int.Parse(Console.ReadLine() ?? "0");
                        myCart = bl.Cart.UpdateAmountOfProduct(myCart, id, amount);
                        Console.WriteLine(myCart);//מדפיס סל מעודכן
                        Console.WriteLine("The product amount has been updated in the shopping cart\n");
                        break;
                    case 'c':
                        bl?.Cart.Confirm(myCart);
                        Console.WriteLine("The cart is confirmed\n");//מדפיס סל מעודכן
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