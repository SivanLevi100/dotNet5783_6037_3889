﻿
using DO;
using System.Collections;
using System.Security.Cryptography;

namespace Dal;

internal sealed class DataSource
{
    public static readonly Random randomaly = new();//*//
    private static readonly DataSource _instance;
    public static DataSource Instance
    {
        get { return _instance; }
    }

    internal static  Product[] productArray = new Product[50];
    internal static Order[]  orderArray = new Order[100];
    internal static OrderItem[] orderItemArray = new OrderItem[200];

    //private static int index = 0;
    //public static int Index { get => index++; }

    /// <summary>
    /// constructor
    /// </summary>
    static DataSource()
    {
      _instance = new DataSource();
    }
    private DataSource()
    {
        s_Initialize();
    }
    /// <summary>
    /// Internal class config
    /// </summary>
    internal static class Config
    {
        internal static int ProductFreeIndex = 0; //אינדקסים) של האלמנט הפנוי הראשון
        internal static int OrderFreeIndex = 0;
        internal static int OrderItemFreeIndex = 0;
        private static int orderLastId = 100000; //מספר מזהה אחרון
        private static int orderItemLastId = 100000; //מספר מזהה אחרון

        // get
        public static int OrderLastId { get => orderLastId++; }
        public static int OrderItemLastId { get => orderItemLastId++; }

    }

    /// <summary>
    /// A private method that will add objects to the array of products
    /// </summary>
    private static void addProduct()
    {
        int x;
        Random rand=new Random();
        for (int i = 0; i < 10; i++)
        {
            productArray[i] = new Product();
            //Config.ProductFreeIndex++;
        }

        x = rand.Next(100000, 1000000);
        productArray[0].Id = x;
        productArray[0].Category = Category.Refrigerator;
        productArray[0].Name = " SHARP refrigerator ";
        productArray[0].Price =9000 ;
        productArray[0].InStock = 5;

        productArray[1].Id = x+1;
        productArray[1].Category = Category.Refrigerator;
        productArray[1].Name = " FUJICOM freezer";
        productArray[1].Price = 1000;
        productArray[1].InStock = 7;

        productArray[2].Id = x+2;
        productArray[2].Category = Category.Kitchen;
        productArray[2].Name = "Blomberg oven ";
        productArray[2].Price = 2500;
        productArray[2].InStock = 0;

        productArray[3].Id = x+3;
        productArray[3].Category = Category.Kitchen;
        productArray[3].Name = " kenwoon mixer ";
        productArray[3].Price = 2000;
        productArray[3].InStock = 2;

        productArray[4].Id = x+4;
        productArray[4].Category = Category.Computer;
        productArray[4].Name = " Samsung TV 75 ";
        productArray[4].Price = 3800;
        productArray[4].InStock = 8;

        productArray[5].Id = x+5;
        productArray[5].Category = Category.Computer; 
        productArray[5].Name = " hp Computer Touch  ";
        productArray[5].Price = 3200;
        productArray[5].InStock = 0;

        productArray[6].Id = x+6;
        productArray[6].Category = Category.Cleaning;
        productArray[6].Name = "Electra Washing  machine";
        productArray[6].Price =1200 ;
        productArray[6].InStock = 4;

        productArray[7].Id = x + 7;
        productArray[7].Category = Category.Cleaning;
        productArray[7].Name = "Bosch Dryer";
        productArray[7].Price = 2000;
        productArray[7].InStock = 7;

        productArray[8].Id = x + 8;
        productArray[8].Category = Category.Cooling;
        productArray[8].Name = "TADIRAN - ALPHA PRO Air-Conditioner ";
        productArray[8].Price = 4100;
        productArray[8].InStock = 3;

        productArray[9].Id = x + 9;
        productArray[9].Category = Category.Cooling;
        productArray[9].Name = "TORNADO - Q30X WIFI Air-Conditioner ";
        productArray[9].Price = 3900;
        productArray[9].InStock = 10;


    }

    /// <summary>
    /// A private method that will add objects to the orders array
    /// </summary>
    private static void addOrder()
    {
        Random random = new Random();
        TimeSpan time;
        for (int i = 0; i < 20; i++)
        {
            orderArray[i] = new Order();
            orderArray[i].Id =Config.OrderLastId;//המספר הרץ עולה בזימון הפונקציה ממחלקת config
           // orderArray[i].OrderDate = DateTime.MinValue;
            //orderArray[i].ShipDate = DateTime.MinValue;  
            //orderArray[i].DeliveryDate = DateTime.MinValue;
            orderArray[i].CustomerName = "Customer_" + (char)i;
            orderArray[i].CustomerAdress = (char)(i + 3) +"in jerusalem" ;
            orderArray[i].CustomerEmail = (char)i * 3 + "@gmail.com";
            Config.OrderFreeIndex++;// מעלים ב1 את המקום הפנוי הבא המערך
            orderArray[i].OrderDate = DateTime.Now.AddDays(random.Next(-1000,-1));
            if(i<=15)
            {
                do
                {
                    orderArray[i].ShipDate = DateTime.Now.AddDays(random.Next(-1000, -1));
                    time = orderArray[i].ShipDate - orderArray[i].OrderDate;
                }
                while (time.TotalDays < 0);
            }
            else
            {
                orderArray[i].ShipDate = DateTime.MinValue;
            }
            if(i<=10)
            {
                do
                {
                    orderArray[i].DeliveryDate= DateTime.Now.AddDays(random.Next(-1000, -1));
                    time = orderArray[i].DeliveryDate - orderArray[i].ShipDate;

                }
                while(time.TotalDays < 0);
            }
            else
                orderArray[i].DeliveryDate= DateTime.MinValue;
        }
        ///80  60 אחוז
    }

    /// <summary>
    /// A private method that will add objects to an array of order details
    /// </summary>
    private static void addOrderItem()
    {
        Random rand = new Random();
        for (int i = 0; i < 40; i++)
        {
            orderItemArray[i] = new OrderItem();
            orderItemArray[i].Id = Config.OrderItemLastId;//המספר הרץ עולה בזימון הפונקציה ממחלקת config
            orderItemArray[i].ProductId = rand.Next(); //randomaly.Next();
            orderItemArray[i].OrderId = rand.Next(); //randomaly.Next();
            orderItemArray[i].Amount = i * 3 + 1;
            Config.OrderItemFreeIndex++;//מעלים ב1 את המקום הפנוי הבא המערך
        }

        for (int i = 0; i < /*40*/10; i++)
        {
            orderItemArray[i].Price = productArray[i].Price;
        }

    }

    /// <summary>
    /// The s_Initialize method will schedule the method of adding objects to the entity arrays
    /// </summary>
    private static void s_Initialize()
    {
        addProduct();
        addOrder();
        addOrderItem();
    }

   
}
