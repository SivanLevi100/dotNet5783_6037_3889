using BlApi;
using BlImplementation;
using Dal;
using DalApi;

namespace BlTest;

class Program  /*internal*/
{
    static IBl bl = new Bl();
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


    public static void SubMenuProduct()
    {

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

                        break;
                    case 'b':

                        break;
                    case 'c':

                        break;
                    case 'd':

                        break;
                    case 'e':

                        break;
                    case 'f':

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

    }
    public static void SubMenuCart()
    {

    }


}