﻿
using DO;
using System;
using System.Collections;
using System.Security.Cryptography;

namespace Dal;

internal sealed class DataSource
{
    public static readonly Random randomaly = new();
    private static readonly DataSource _instance;
    public static DataSource Instance
    {
        get { return _instance; }
    }

    internal static  List<Product> productList = new();
    internal static List<Order>  orderList = new();
    internal static List<OrderItem> orderItemList = new();

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
        //internal static int ProductFreeIndex =0;    //Indexes of the first free element
        //internal static int OrderFreeIndex = 0;      //Indexes of the first free element
        //internal static int OrderItemFreeIndex = 0;  //Indexes of the first free element
        private static int orderLastId = 100000;     //Last ID number
        private static int orderItemLastId = 100000; //Last ID number

        public static int OrderLastId { get => orderLastId++; }
        public static int OrderItemLastId { get => orderItemLastId++; }

    }

    /// <summary>
    /// A private method that will add objects to the array of products
    /// </summary>
    private static void addProduct()
    {
        string[] names = { "SHARP refrigerator","FUJICOM freezer","Blomberg oven","kenwoon mixer", "Samsung TV 75","hp Computer Touch",
                "Electra Washing  machine","Bosch Dryer","TADIRAN - ALPHA PRO Air-Conditioner","TORNADO - Q30X WIFI Air-Conditioner" };
        Random rand = new Random();
        for (int i = 0; i < 10; i++)
        {
            productList.Add(new Product()
            {
                Id = rand.Next(100000, 1000000),
                Name = names[rand.Next(names.Length)],
                Price = rand.Next(300),
                Category = (Category)rand.Next(5),
                InStock = rand.Next(80),
            });
        }
    }
        

    /// <summary>
    /// A private method that will add objects to the orders array
    /// </summary>
    private static void addOrder()
    {
        string[] cities = { "Tel Aviv", "Jerusalem", "Haifa", "Ashdod", "Lod", "Beni Brak", "Ramat Gan", "Holon" };
        Random random = new Random();
        TimeSpan time;
        for (int i = 0; i < 20; i++)
        {

            orderList.Add(new Order()
            {
                Id = Config.OrderLastId,
                CustomerName= "Customer_" + (char)i,
                CustomerAdress= cities[random.Next(cities.Length)],
                CustomerEmail= (char)i * 3 + "@gmail.com",
                OrderDate= DateTime.Now.AddDays(random.Next(-1000, -1))
            }) ;

            //orderArray[i] = new Order();
            //orderArray[i].Id =Config.OrderLastId;//The running number increases when calling the function from the config class
            //orderArray[i].CustomerName = "Customer_" + (char)i;
            //orderArray[i].CustomerAdress = (char)(i + 3) +"in jerusalem" ;
            //orderArray[i].CustomerEmail = (char)i * 3 + "@gmail.com";
            //Config.OrderFreeIndex++;//Increase by 1 the next free place in the array
            //orderArray[i].OrderDate = DateTime.Now.AddDays(random.Next(-1000,-1));
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
    }

    /// <summary>
    /// A private method that will add objects to an array of order details
    /// </summary>
    private static void addOrderItem()
    {
        Random rand = new Random();
        for (int i = 0; i < 40; i++)
        {

            orderItemList.Add(new OrderItem()
            {
                Id= Config.OrderItemLastId,
                ProductId= rand.Next(),
                OrderId= rand.Next(),
                Amount= i * 3 + 1,
                Price= ///////////


            });
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
