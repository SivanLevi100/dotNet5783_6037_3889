
using DO;
using System.Collections;
using System.Security.Cryptography;

namespace Dal;

internal static class DataSource
{
    public static readonly Random randomaly = new();//*//
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
        private static int orderLastId = 0; //מספר מזהה אחרון
        private static int orderItemLastId = 0; //מספר מזהה אחרון

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
            //x = rand.Next(100000,1000000);
            //for(int j=0; j < Config.ProductFreeIndex; j++)
            //{
            //    if (x == DataSource.productArray[j].Id)
            //    {
            //        i--;
            //        break;
                    
            //    }

            //}
        }

        x = rand.Next(100000, 1000000);
        productArray[0].Id = x;
        productArray[0].Category = "מקררים ומקפיאים";
        productArray[0].Name = " SHARP מקרר ";
        productArray[0].Price =9000 ;
        productArray[0].InStock = 5;

        productArray[1].Id = x+1;
        productArray[1].Category = "מקררים ומקפיאים";
        productArray[1].Name = " FUJICOM מקפיא";
        productArray[1].Price = 1000;
        productArray[1].InStock = 7;

        productArray[2].Id = x+2;
        productArray[2].Category = "מוצרי חשמל למטבח";
        productArray[2].Name = "Blomberg תנור משולב ";
        productArray[2].Price = 2500;
        productArray[2].InStock = 0;

        productArray[3].Id = x+3;
        productArray[3].Category = "מוצרי חשמל למטבח";
        productArray[3].Name = " kenwoon מיקסר ";
        productArray[3].Price = 2000;
        productArray[3].InStock = 2;

        productArray[4].Id = x+4;
        productArray[4].Category = "טלוויזיות ומחשבים";
        productArray[4].Name = " Samsung טלוויזיה בגודל 75 אינץ ";
        productArray[4].Price = 3800;
        productArray[4].InStock = 8;

        productArray[5].Id = x+5;
        productArray[5].Category = "טלוויזיות ומחשבים";
        productArray[5].Name = " hp מחשב נייד מסך מגע  ";
        productArray[5].Price = 3200;
        productArray[5].InStock = 0;

        productArray[6].Id = x+6;
        productArray[6].Category = "מכונות כביסה וייבוש";
        productArray[6].Name = "Electra מכונת כביסה";
        productArray[6].Price =1200 ;
        productArray[6].InStock = 4;

        productArray[7].Id = x + 7;
        productArray[7].Category = "מכונות כביסה וייבוש";
        productArray[7].Name = "Bosch מייבש כביסה";
        productArray[7].Price = 2000;
        productArray[7].InStock = 7;

        productArray[8].Id = x + 8;
        productArray[8].Category = "מזגנים";
        productArray[8].Name = "TADIRAN - ALPHA PRO מזגן ";
        productArray[8].Price = 4100;
        productArray[8].InStock = 3;

        productArray[9].Id = x + 9;
        productArray[9].Category = "מזגנים";
        productArray[9].Name = "TORNADO - Q30X WIFI מזגן ";
        productArray[9].Price = 3900;
        productArray[9].InStock = 10;


    }

    /// <summary>
    /// A private method that will add objects to the orders array
    /// </summary>
    private static void addOrder()
    {
        Random random = new Random();
        for (int i = 0; i < 20; i++)
        {
            orderArray[i] = new Order();
            orderArray[i].Id =Config.OrderLastId;//המספר הרץ עולה בזימון הפונקציה ממחלקת config
            orderArray[i].OrderDate = DateTime.MinValue;  //DateTime.Now;
            orderArray[i].ShipDate = DateTime.MinValue;  //DateTime.Now;
            orderArray[i].DeliveryDate = DateTime.MinValue; //DateTime.Now;
            Config.OrderFreeIndex++;// מעלים ב1 את המקום הפנוי הבא המערך
        }
        //orderArray[0].CustomerName="Sivan "
        //orderArray[0].CustomerAdress=""
        //orderArray[0].CustomerEmail=""

        //orderArray[1].CustomerName = ""
        //orderArray[1].CustomerAdress = ""
        //orderArray[1].CustomerEmail = ""

            //עד 19 כי צריך 20 הזמנות


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
