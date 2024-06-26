﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
using System.Runtime.CompilerServices;

public struct ImportentNumbers
{
    public double numberSaved { get; set; }
    public string typeOfnumber { get; set; }
}


//implement ILecturer with XML Serializer
internal class Order : IOrder
{
    const string o_orders = "orders"; //XML Serializer
    string configPath = "config";

    /// <summary>
    /// Request/read method of the list of all objects of an order
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? filter = null)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(o_orders)!;
        return filter == null ? listOrders.OrderBy(order => ((DO.Order)order!).Id)
                              : listOrders.Where(filter).OrderBy(order => ((DO.Order)order!).Id);
    }

    /// <summary>
    /// A request/call method of a single object that receives an order ID number and returns the appropriate order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundExceptions"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Order? Get(int id) =>
        XMLTools.LoadListFromXMLSerializer<DO.Order>(o_orders).FirstOrDefault(order => order?.Id == id)
        ?? throw new NotFoundExceptions("The order id is not exist in List");



    /// <summary>
    ///  An add object method that accepts an order object and returns the ID number of the added order
    /// </summary>
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    /// <exception cref="DuplicateIdExceptions"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
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

        XMLTools.SaveListToXMLSerializer1(runningList, configPath);
        XMLTools.SaveListToXMLSerializer(listOrders, o_orders);

        return order.Id;

    }

    /// <summary>
    /// A method to delete an order object that receives an order ID number
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="NotFoundExceptions"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(o_orders);

        if (listOrders.RemoveAll(order => order?.Id == id) == 0)
            throw new NotFoundExceptions("The order is not exist in the List");

        XMLTools.SaveListToXMLSerializer(listOrders, o_orders);
    }

    /// <summary>
    /// An object update method that will receive a new order
    /// </summary>
    /// <param name="order"></param>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.Order order)
    {
        int id=order.Id;
        Delete(order.Id);
        order.Id = id;
        var orderList = XMLTools.LoadListFromXMLSerializer<DO.Order>(o_orders);
        if (orderList.Exists(item => item.Value.Id == order.Id))
            throw new Exception("ID already exist");
        orderList.Add(order);
        XMLTools.SaveListToXMLSerializer(orderList, o_orders);

    }


    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Order? GetF(Func<DO.Order?, bool>? filter)
    {
        var ordersList = XMLTools.LoadListFromXMLSerializer<DO.Order>(o_orders)!;
        if (ordersList.FirstOrDefault(filter) == null)
        {
            throw new Exception("The order id is not exist in List");
        }
        return ordersList.FirstOrDefault(filter);

    }


}
