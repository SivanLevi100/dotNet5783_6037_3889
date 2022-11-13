
using DO;
using System.Collections;
using System.Security.Cryptography;

namespace Dal;

internal static class DataSource
{
    public static readonly int randomaly = 0;//לחזור לזה
    //public static Random ran = new Random();
    internal static  Product[] productArray = new Product[50];
    internal static Order[]  orderArray = new Order[100];
    internal static OrderItem[] orderItemArray = new OrderItem[200];

    private static int index = 0;
    public static int Index { get => index++; }





    static DataSource()
    {
        s_Initialize();
    }
    private static void addProduct()
    {
        //int rand=
        for (int i = 0; i < 10; i++)
        {
            productArray[i] = new Product();

        }
        productArray[0].Category = "מקררים ומקפיאים";
        productArray[0].Name = " SHARP מקרר ";
        productArray[0].Price =9000 ;
        productArray[0].InStock = 5;


        productArray[1].Category = "מקררים ומקפיאים";
        productArray[1].Name = " FUJICOM מקפיא";
        productArray[1].Price = 1000;
        productArray[1].InStock = 7;


        productArray[2].Category = "מוצרי חשמל למטבח";
        productArray[2].Name = "Blomberg תנור משולב ";
        productArray[2].Price = 2500;
        productArray[2].InStock = 0;

        productArray[3].Category = "מוצרי חשמל למטבח";
        productArray[3].Name = " kenwoon מיקסר ";
        productArray[3].Price = 2000;
        productArray[3].InStock = 2;

        productArray[4].Category = "טלוויזיות ומחשבים";
        productArray[4].Name = " Samsung טלוויזיה בגודל 75 אינץ ";
        productArray[4].Price = 3800;
        productArray[4].InStock = 8;

        productArray[5].Category = "טלוויזיות ומחשבים";
        productArray[5].Name = " hp מחשב נייד מסך מגע  ";
        productArray[5].Price = 3200;
        productArray[5].InStock = 0;

        productArray[6].Category = "מכונות כביסה וייבוש";
        productArray[6].Name = "Electra מכונת כביסה";
        productArray[6].Price =1200 ;
        productArray[6].InStock = 4;

        productArray[7].Category = "מכונות כביסה וייבוש";
        productArray[7].Name = "Bosch מייבש כביסה";
        productArray[7].Price = 2000;
        productArray[7].InStock = 7;

        productArray[8].Category = "מזגנים";
        productArray[8].Name = "TADIRAN - ALPHA PRO מזגן ";
        productArray[8].Price = 4100;
        productArray[8].InStock = 3;

        productArray[9].Category = "מזגנים";
        productArray[9].Name = "TORNADO - Q30X WIFI מזגן ";
        productArray[9].Price = 3900;
        productArray[9].InStock = 10;


    }

    private static void addOrder()
    {

        for (int i = 0; i < 20; i++)
        {
            orderArray[i] = new Order();
            productArray[i].Id = DataSource.Index;
            orderArray[i].OrderDate = DateTime.Now; //DateTime.MinValue;
            orderArray[i].ShipDate = DateTime.Now; //DateTime.MinValue;
            orderArray[i].DeliveryDate = DateTime.Now;//DateTime.MinValue;
        }
        for (int i = 0; i < 20; i++)
        {
            //orderArray[i].OrderDate = new Random(DateTime.Now.Millisecond);
        }
    }
    private static void addOrderItem()
    {
        for (int i = 0; i < 40; i++)
        {
            orderItemArray[i] = new OrderItem();
        }
        //int count = 1;
        //for(int i = 0; i < 40; i++)
        //{
        //    orderItemArray[i].Amount = count;
        //    count++;
        //    if(count ==)
        //}
        for (int i = 0; i < 10; i++)
        {
            orderItemArray[i].Price = productArray[i].Price;

        }


    }

    private static void s_Initialize()
    {
        addProduct();
        addOrderItem();
        addOrder();
        //for(int i = 0; i < 50; i++)
    }

    internal static class Config 
    {
        internal static int ProductFreeIndex = 0; //אינדקסים) של האלמנט הפנוי הראשון
        internal static int OrderFreeIndex = 0;
        internal static int OrderItemFreeIndex = 0;
        private static int orderLastId = 0; //מספר מזהה אחרון
        private static int orderItemLastId = 0; //מספר מזהה אחרון

        // get
        public static int OrderLastId { get => orderLastId++; }
        public static int OrderItemLastId { get => orderItemLastId++; }





    }
}
