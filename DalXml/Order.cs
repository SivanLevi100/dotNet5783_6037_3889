using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;

public struct ImportentNumbers
{
    public double numberSaved { get; set; }
    public string typeOfnumber { get; set; }
}


//implement ILecturer with XML Serializer
internal class Order : IOrder
{
    const string o_orders = "orders"; //XML Serializer
    private readonly string configPath = "Config";

    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? filter = null)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(o_orders)!;
        return filter == null ? listOrders.OrderBy(order => ((DO.Order)order!).Id)
                              : listOrders.Where(filter).OrderBy(order => ((DO.Order)order!).Id);
    }

    public DO.Order? Get(int id) =>
        XMLTools.LoadListFromXMLSerializer<DO.Order>(o_orders).FirstOrDefault(order => order?.Id == id)
        ?? throw new NotFoundExceptions("The order id is not exist in List");


    public int Add(DO.Order order)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(o_orders);

        if (listOrders.Exists(order1 => ((DO.Order)order1!).Id == order.Id))
            throw new DuplicateIdExceptions("no place in List to add");

        List<ImportentNumbers> runningList = XMLTools.LoadListFromXMLSerializer1<ImportentNumbers>(configPath);

        ImportentNumbers runningNum = (from number in runningList
                                       where (number.typeOfnumber == "Number Runing Order")
                                       select number).FirstOrDefault();

        runningList.Remove(runningNum);
        runningNum.numberSaved++;
        order.Id = (int)runningNum.numberSaved;
        runningList.Add(runningNum);


        listOrders.Add(order);

        XMLTools.SaveListToXMLSerializer(runningList, configPath);
        XMLTools.SaveListToXMLSerializer(listOrders, o_orders);

        return order.Id;

    }


    //

    public void Delete(int id)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(o_orders);

        if (listOrders.RemoveAll(order => order?.Id == id) == 0)
            throw new NotFoundExceptions("The order is not exist in the List");

        XMLTools.SaveListToXMLSerializer(listOrders, o_orders);
    }

    public void Update(DO.Order order) 
    {
        Delete(order.Id);
        Add(order);
    }


    public DO.Order GetF(Func<DO.Order?, bool>? filter)///////////////////???
    {
        throw new NotFoundExceptions("The order id is not exist in List");
    }



}
