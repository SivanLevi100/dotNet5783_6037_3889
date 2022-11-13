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
        int choose = 0;
        Console.WriteLine(@"0 - Exit
         1 -בדיקת מוצר   
         2 - בדיקת הזמנה
         3 -בדיקת פריט בהזמנה ");
        Console.WriteLine(@"Enter your choice");
        choose = Console.Read();
        switch(choose)
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

        }
    }

    public static void SubMenuProduct()
    {
        Product myProduct = new Product();
        int myIdOfProduct = 0;
        Product[] productsArray = new Product[50];
        char ch;
        Console.WriteLine(@"a - אפשרות הוספת אובייקט לרשימה של ישות
            b - אפשרות תצוגת אובייקט על פי מזהה 
            c - אפשרות תצוגת הרשימה של ישות
            d - אפשרות עדכון נתוני אובייקט
            e - אפשרות מחיקת אובייקט מרשימת של ישות");
        ch = (char)Console.Read();
        switch (ch)
        {
            case 'a':
                try
                {
                    myProduct.Id = Console.Read();
                    myProduct.Name = Console.ReadLine();
                    myProduct.Category = Console.ReadLine();
                    myProduct.Price = Console.Read();
                    myProduct.InStock = Console.Read();
                    DalProduct.addProducts(myProduct);
                }
                catch (Exception str)
                {
                    Console.WriteLine(str);
                }
                break;
            case 'b':
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
                productsArray = DalProduct.getArrayOfProduct();
                foreach ( Product product1 in productsArray)
                    { Console.WriteLine(productsArray);};
                break;
            case 'd':
                myProduct.Id = Console.Read();
                myProduct.Name = Console.ReadLine();
                myProduct.Category = Console.ReadLine();
                myProduct.Price = Console.Read();
                myProduct.InStock = Console.Read();
                try
                {
                    DalProduct.updateProduct(myProduct);
                    Console.WriteLine(myProduct);
                    Product newProduct =new Product();                     //קולט ערכים חדשים
                    Console.WriteLine("Enter new Product");
                    newProduct.Id=Console.Read();
                    newProduct.Name= Console.ReadLine();
                    newProduct.Category= Console.ReadLine();
                    newProduct.Price= Console.Read();
                    newProduct.InStock= Console.Read();
                }
                catch (Exception str)
                {
                    Console.WriteLine(str);
                }
                break;
            case 'e':
                myIdOfProduct= Console.Read();
                try
                {
                    DalProduct.deleteProduct(myIdOfProduct);
                }
                catch(Exception str)
                {
                    Console.WriteLine(str);
                }
                break;
        }
    }

    public static void SubMenuOrder()
    {
        Order myOrder=new Order();
        Order[] orderArray = new Order[100];
        int idOrder=0;
        char ch;
        Console.WriteLine(@"a - אפשרות הוספת אובייקט לרשימה של ישות
            b - אפשרות תצוגת אובייקט על פי מזהה 
            c - אפשרות תצוגת הרשימה של ישות
            d - אפשרות עדכון נתוני אובייקט
            e - אפשרות מחיקת אובייקט מרשימת של ישות");
        ch = (char)Console.Read();
        switch (ch)
        {
            case 'a':
                try
                {
                    myOrder.Id = Console.Read();
                    myOrder.CustomerName = Console.ReadLine();
                    myOrder.CustomerAdress = Console.ReadLine();
                    myOrder.CustomerEmail = Console.ReadLine();
                    myOrder.OrderDate = (DateTime)Console.Read();
                    myOrder.ShipDate = (DateTime)Console.Read();
                    myOrder.DeliveryDate = (DateTime)Console.Read();
                    DalOrder.addOrders(myOrder);
                }
                catch (Exception str)
                {
                    Console.WriteLine(str);
                }
                break;
            case 'b':
                idOrder= Console.Read();
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
                myOrder.Id = Console.Read();
                myOrder.CustomerName = Console.ReadLine();
                myOrder.CustomerAdress = Console.ReadLine();
                myOrder.CustomerEmail = Console.ReadLine();
                myOrder.OrderDate = (DateTime)Console.Read();
                myOrder.ShipDate = (DateTime)Console.Read();
                myOrder.DeliveryDate = (DateTime)Console.Read();
                try
                {
                    DalOrder.updateOrder(myOrder);
                    Console.WriteLine(myOrder);
                    Order newOrder = new Order();                     //קולט ערכים חדשים
                    Console.WriteLine("Enter new Order");
                    newOrder.Id = Console.Read();
                    newOrder.CustomerName = Console.ReadLine();
                    newOrder.CustomerAdress = Console.ReadLine();
                    newOrder.CustomerEmail = Console.ReadLine();
                    newOrder.OrderDate = (DateTime)Console.Read();
                    newOrder.ShipDate= (DateTime)Console.Read();
                    newOrder.DeliveryDate= (DateTime)Console.Read();
                }
                catch (Exception str)
                {
                    Console.WriteLine(str);
                }
                break;
            case 'e':
                idOrder= Console.Read();
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


    public static void SubMenuOrderItem()
    {
        Order myOrder=new Order();  
        OrderItem myOrderItem = new OrderItem();
        OrderItem[] orderItemArray = new OrderItem[200];
        int idOrderItem = 0;
        int idOrderItem1=0, idOrderItem2=0;
        char ch;
        Console.WriteLine(@"a - אפשרות הוספת אובייקט לרשימה של ישות
            b - אפשרות תצוגת אובייקט על פי מזהה 
            c - אפשרות תצוגת הרשימה של ישות
            d - אפשרות עדכון נתוני אובייקט
            e - אפשרות מחיקת אובייקט מרשימת של ישות
            f - אפשרות תצוגת אובייקט על פי מזהה מוצר ומזהה הזמנה
            g - אפשרות תצוגת הרשימה של ישות על פי מספר מזהה של ההזמנה");
        ch = (char)Console.Read();
        switch (ch)
        {
            case 'a':
                try
                {
                    myOrderItem.Id = Console.Read();
                    myOrderItem.ProductId = Console.Read();
                    myOrderItem.OrderId = Console.Read();
                    myOrderItem.Price = Console.Read();
                    myOrderItem.Amount = Console.Read();
                    DalOrderItem.addOrderItems(myOrderItem);
                }
                catch (Exception str)
                {
                    Console.WriteLine(str);
                }
                break;
            case 'b':
                idOrderItem= Console.Read();
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
                    newOrderItem.Price= Console.Read();
                    newOrderItem.Amount = Console.Read();
                }
                catch (Exception str)
                {
                    Console.WriteLine(str);
                }
                break;
            case 'e':
                idOrderItem= Console.Read();
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
                idOrderItem= Console.Read();
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
                foreach(Console.WriteLine(orderItemArray));///////////////////
                break;
        }
    }


}




