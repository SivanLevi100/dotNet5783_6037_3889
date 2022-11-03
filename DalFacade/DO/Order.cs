

using System.Diagnostics;
using System.Xml.Linq;

namespace DO;

public struct Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    public DateTime OrderDate { get; set; } //תאריך יצירת הזמנה
    public DateTime ShipDate { get; set; } //תאריך משלוח
    public DateTime DeliveryDate { get; set; } //תאריך מסירה


}


