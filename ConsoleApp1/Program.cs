using DO;
using DalApi;
namespace ConsolApp1
{
    public class Program
    {
        //static /*private*/  IDal dal = new Dal.DalList();
        static IDal dal = new Dal.DalList();
        static void Main(string[] args)
        {
            foreach (Product item in dal.Product.GetList())
            {
                Console.WriteLine(item);
            };
        }
    }
}