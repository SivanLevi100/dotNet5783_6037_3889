using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
/// <summary>
/// class of Cart
/// </summary>
public class Cart
{
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAdress { get; set; }
    public List<OrderItem?>? OrdersItemsList { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString()
    {
        string str = "";
        str += $"Customer Name: {CustomerName} \n";
        str += $"Customer Email: {CustomerEmail} \n";
        str += $"Customer Adress: {CustomerAdress} \n";
        str += $"Total Price Of Item: {TotalPrice} \n";
        str += $"List of OrdersItems: \n";
        foreach (var item in OrdersItemsList ?? throw new BO.NotExiestsExceptions("The list of orderItem is not exiest"))
        {
            str += $" orderitem: {item}\n";
        }
        return str;


    }


}
