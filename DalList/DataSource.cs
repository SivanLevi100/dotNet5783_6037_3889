﻿
//using DalApi;
//using DO;
//using System;
//using System.Collections;
//using System.Reflection;
//using System.Security.Cryptography;

using DO;
namespace Dal;

internal sealed class DataSource
{
    internal static DataSource s_instance { get; }

    private static readonly Random random = new Random(); 
    static DataSource() => s_instance = new DataSource();
    private DataSource() => s_Initialize();


    internal List<DO.Product> ProductList { get; } = new List<DO.Product>();
    internal List<DO.Order> OrderList { get; } = new List<DO.Order>();
    internal List<DO.OrderItem> OrderItemList { get; } = new List<DO.OrderItem>();



    //public static readonly Random random = new();
    //private static readonly DataSource _instance;
    //public static DataSource Instance
    //{
    //    get { return _instance; }
    //}

    //internal static List<Product> productList = new();
    //internal static List<Order> orderList = new();
    //internal static List<OrderItem> orderItemList = new();
    //static DataSource()
    //{
    //    _instance = new DataSource();
    //}
    //private DataSource()
    //{
    //    s_Initialize();
    //}

    /// <summary>
    /// The s_Initialize method will schedule the method of adding objects to the entity arrays
    /// </summary>
    private void s_Initialize()
    {
        addProducts();
        addOrders();
        addOrderItems();
    }

    /// <summary>
    /// Internal class config
    /// </summary>
    internal static class Config
    {
        private static int orderLastId = 100000;     //Last ID number
        private static int orderItemLastId = 100000; //Last ID number

        public static int OrderLastId { get => orderLastId++; }
        public static int OrderItemLastId { get => orderItemLastId++; }

    }

    /// <summary>
    /// A private method that will add objects to the array of products
    /// </summary>
    private void addProducts()
    {
        string[] names = { "SHARP refrigerator","FUJICOM freezer","Blomberg oven","kenwoon mixer", "Samsung TV 75","hp Computer Touch",
                "Electra Washing  machine","Bosch Dryer","TADIRAN - ALPHA PRO Air-Conditioner","TORNADO - Q30X WIFI Air-Conditioner" };
        for (int i = 0; i < 10; i++)
        {
            ProductList.Add(new DO.Product()
            {
                Id = random.Next(100000, 1000000),
                Name = names[random.Next(names.Length)],
                Price = random.Next(300),
                Category = (Category)random.Next(5),
                InStock = random.Next(80),
            });
        }
        //Console.WriteLine(ProductList.Count());

        //foreach (var d in ProductList)
        //{
        //    Console.WriteLine(d);
        //}

    }


    /// <summary>
    /// A private method that will add objects to the orders array
    /// </summary>
    private void addOrders()
    {
        string[] cities = { "Tel Aviv", "Jerusalem", "Haifa", "Ashdod", "Lod", "Beni Brak", "Ramat Gan", "Holon" };
        TimeSpan time;
        DateTime ShipDate11= DateTime.Now.AddDays(random.Next(-1000, -1));
        DateTime DeliveryDate11= DateTime.Now.AddDays(random.Next(-1000, -1));
        for (int i = 0; i < 20; i++)
        {
            if (i<=15)
            {
                do
                {
                    OrderList.Add(new Order()
                    {
                        Id = Config.OrderLastId,
                        CustomerName = "Customer_" + (char)i,
                        CustomerAdress = cities[random.Next(cities.Length)],
                        CustomerEmail = (char)i * 3 + "@gmail.com",
                        OrderDate = DateTime.Now.AddDays(random.Next(-1000, -1)),
                        ShipDate = ShipDate11

                    });
                    time = OrderList[i].ShipDate - OrderList[i].OrderDate;
                }
                while (time.TotalDays < 0);
            }
            else
            {
                OrderList.Add(new Order()
                {
                    Id = Config.OrderLastId,
                    CustomerName = "Customer_" + (char)i,
                    CustomerAdress = cities[random.Next(cities.Length)],
                    CustomerEmail = (char)i * 3 + "@gmail.com",
                    OrderDate = DateTime.Now.AddDays(random.Next(-1000, -1)),
                    ShipDate = DateTime.MinValue

                });
            }
            if (i <= 10)
            {
                do
                {
                    OrderList.Add(new Order()
                    {
                        Id = Config.OrderLastId,
                        CustomerName = "Customer_" + (char)i,
                        CustomerAdress = cities[random.Next(cities.Length)],
                        CustomerEmail = (char)i * 3 + "@gmail.com",
                        OrderDate = DateTime.Now.AddDays(random.Next(-1000, -1)),
                        DeliveryDate = DeliveryDate11

                    });
                    time = OrderList[i].DeliveryDate - OrderList[i].ShipDate;

                }
                while (time.TotalDays < 0);
            }
            else
            {
                OrderList.Add(new Order()
                {
                    Id = Config.OrderLastId,
                    CustomerName = "Customer_" + (char)i,
                    CustomerAdress = cities[random.Next(cities.Length)],
                    CustomerEmail = (char)i * 3 + "@gmail.com",
                    OrderDate = DateTime.Now.AddDays(random.Next(-1000, -1)),
                    DeliveryDate = DateTime.MinValue

                });
            }
        }

        //for (int i = 0; i < 20; i++)
        //{

        //    orderList.Add(new Order()
        //    {
        //        Id = Config.OrderLastId,
        //        CustomerName = "Customer_" + (char)i,
        //        CustomerAdress = cities[random.Next(cities.Length)],
        //        CustomerEmail = (char)i * 3 + "@gmail.com",
        //        OrderDate = DateTime.Now.AddDays(random.Next(-1000, -1))
        //    });

        //    if (i <= 15)
        //    {
        //        do
        //        {
        //            orderList[i].ShipDate = DateTime.Now.AddDays(random.Next(-1000, -1));
        //            time = orderList[i].ShipDate - orderList[i].OrderDate;
        //        }
        //        while (time.TotalDays < 0);
        //    }
        //    else
        //    {
        //        orderList[i].ShipDate = DateTime.MinValue;
        //    }
        //    if (i <= 10)
        //    {
        //        do
        //        {
        //            orderList[i].DeliveryDate = DateTime.Now.AddDays(random.Next(-1000, -1));
        //            time = orderList[i].DeliveryDate - orderList[i].ShipDate;

        //        }
        //        while (time.TotalDays < 0);
        //    }
        //    else
        //        orderList[i].DeliveryDate = DateTime.MinValue;
        //}

    }

    /// <summary>
    /// A private method that will add objects to an array of order details
    /// </summary>
    private void addOrderItems()
    {
        for (int i = 0; i < 40; i++)
        {
            OrderItem orderItem = new OrderItem();
            orderItem.Id = Config.OrderItemLastId;
            orderItem. ProductId = random.Next();
            orderItem.OrderId = random.Next();
            orderItem.Amount = random.Next(1, 5);
            foreach(Product product in ProductList)
            {
                if(product.Id == orderItem.ProductId)
                {
                    orderItem.Price = product.Price * orderItem.Amount;
                    break;
                }
            }
            OrderItemList.Add(orderItem);
        }
    }



}
