using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;


internal class OrderItem : IOrderItem
{
    const string o_orderItems = "orderItems"; //XML Serializer

    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? filter = null)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(o_orderItems)!;
        return filter == null ? listOrderItems.OrderBy(orderItem => ((DO.OrderItem)orderItem!).Id)
                              : listOrderItems.Where(filter).OrderBy(orderItem => ((DO.OrderItem)orderItem!).Id);
    }

    public DO.OrderItem? Get(int id) =>
    XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(o_orderItems).FirstOrDefault(orderItem => orderItem?.Id == id)
    ?? throw new NotFoundExceptions("The orderItem id is not exist in List");


    public int Add(DO.OrderItem orderItem)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(o_orderItems);

        if (listOrderItems.Exists(orderItem1 => ((DO.OrderItem)orderItem1!).Id == orderItem.Id))
            throw new DuplicateIdExceptions("no place in List to add");

        listOrderItems.Add(orderItem);

        XMLTools.SaveListToXMLSerializer(listOrderItems, o_orderItems);

        return orderItem.Id;
    }


    public void Delete(int id)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(o_orderItems);

        if (listOrderItems.RemoveAll(orderItem => orderItem?.Id == id) == 0)
            throw new NotFoundExceptions("The orderItem is not exist in the List");

        XMLTools.SaveListToXMLSerializer(listOrderItems, o_orderItems);
    }

    public void Update(DO.OrderItem orderItem)
    {
        Delete(orderItem.Id);
        Add(orderItem);
    }


    public DO.OrderItem GetF(Func<DO.OrderItem?, bool>? filter)///////////////////???
    {
        throw new NotFoundExceptions("The orderItem id is not exist in List");
    }





}
