
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
    static readonly Random random = new Random();
    internal List<DO.Product> ProductList { get; } = new();
    internal List<DO.Order> OrderList { get; } = new();
    internal List<DO.OrderItem> OrderItemList { get; } = new();

    internal static DataSource S_instance { get; }

    private DataSource() => s_Initialize();

    static DataSource() => S_instance = new DataSource();




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
        ProductList.Add(new Product() { Id = random.Next(100000, 1000000), Name = "SHARP refrigerator", Price = random.Next(300), Category = Category.Refrigerator, InStock = random.Next(80) });
        ProductList.Add(new Product() { Id = random.Next(100000, 1000000), Name = "FUJICOM freezer", Price = random.Next(300), Category = Category.Refrigerator, InStock = random.Next(80) });
        ProductList.Add(new Product() { Id = random.Next(100000, 1000000), Name = "Blomberg oven", Price = random.Next(300), Category = Category.Kitchen, InStock = random.Next(80) });
        ProductList.Add(new Product() { Id = random.Next(100000, 1000000), Name = "kenwood mixer", Price = random.Next(300), Category = Category.Kitchen, InStock = random.Next(80) });
        ProductList.Add(new Product() { Id = random.Next(100000, 1000000), Name = "Samsung TV 75", Price = random.Next(300), Category = Category.Computer, InStock = random.Next(80) });
        ProductList.Add(new Product() { Id = random.Next(100000, 1000000), Name = "hp Computer Touch", Price = random.Next(300), Category = Category.Computer, InStock = random.Next(80) });
        ProductList.Add(new Product() { Id = random.Next(100000, 1000000), Name = "Electra Washing  machine", Price = random.Next(300), Category = Category.Cooling, InStock = random.Next(80) });
        ProductList.Add(new Product() { Id = random.Next(100000, 1000000), Name = "Bosch Dryer", Price = random.Next(300), Category = Category.Cooling, InStock = random.Next(80) });
        ProductList.Add(new Product() { Id = random.Next(100000, 1000000), Name = "TADIRAN - ALPHA PRO Air-Conditioner", Price = random.Next(300), Category = Category.Cooling, InStock = random.Next(80) });
        ProductList.Add(new Product() { Id = random.Next(100000, 1000000), Name = "TADIRAN - ALPHA PRO Air-Conditioner", Price = random.Next(300), Category = Category.Cooling, InStock = random.Next(80) });

    }


    /// <summary>
    /// A private method that will add objects to the orders array
    /// </summary>
    private void addOrders()
    {
        string[] names = { "Sivan","Yael","David","Shani", "Shira","Itai",
                "Tehila","Ido","Shalom","Miri","Avi","Moshe","Shimon"};
        string[] cities = { "Tel Aviv", "Jerusalem", "Haifa", "Ashdod", "Lod", "Beni Brak", "Ramat Gan", "Holon","Ashkelon","Netivot","Hertzelia","Naharia" };

        for (int i = 0; i < 12; i++)//80% ShipDatev+ 60% DeliveryDate = 12
        {
            var order = new Order()
            {
                Id= Config.OrderLastId,
                CustomerName = "Customer_" + names[i],
                CustomerEmail = names[i] + "@gmail.com",
                CustomerAdress = "address_" + cities[i],
                OrderDate = DateTime.Now - new TimeSpan(random.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L)),
                ShipDate = DateTime.Now + new TimeSpan(random.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L))
            };
            order.DeliveryDate = order.ShipDate + new TimeSpan(random.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));
            OrderList.Add(order);
        }
        for (int i = 13; i < 17; i++)// only ShipDate = 4
        {
            var order = new Order()
            {
                Id = Config.OrderLastId,
                CustomerName = " Coustumer_" + names[random.Next(names.Length)],
                CustomerEmail = names[random.Next(names.Length)] + "@gmail.com",
                CustomerAdress = "address_" + cities[random.Next(cities.Length)],
                OrderDate = DateTime.Now - new TimeSpan(random.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L)),
                ShipDate = DateTime.Now + new TimeSpan(random.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L)) ,
                DeliveryDate = DateTime.MinValue,
            };

            OrderList.Add(order);
        }

        for (int i = 17; i < 21; i++)// without DeliveryDate and ShipDate = 4
        {
            var order = new Order()
            {
                Id = Config.OrderLastId,
                CustomerName = " Coustumer_" + names[random.Next(names.Length)],
                CustomerEmail = names[random.Next(names.Length)] + "@gmail.com",
                CustomerAdress = "address_" + cities[random.Next(cities.Length)],
                OrderDate = DateTime.Now - new TimeSpan(random.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L)),
                ShipDate = DateTime.MinValue,
                DeliveryDate = DateTime.MinValue
            };
            OrderList.Add(order);
        }

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
            orderItem.ProductId = random.Next();
            orderItem.OrderId = random.Next();
            orderItem.Amount = random.Next(1, 5);
            foreach (Product product in ProductList)
            {
                if (product.Id == orderItem.ProductId)
                {
                    orderItem.Price = product.Price * orderItem.Amount;
                    break;
                }
            }
            OrderItemList.Add(orderItem);
        }
    }



}
