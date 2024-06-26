﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Dal;
using DalApi;
using DO;


internal class OrderItem : IOrderItem
{
    const string o_orderItems = "orderItems"; //XML Serializer

     string configPath = "config";


    /// <summary>
    ///  A request/read method of the list of all order item objects
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? filter = null)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(o_orderItems)!;
        return filter == null ? listOrderItems.OrderBy(orderItem => ((DO.OrderItem)orderItem!).Id)
                              : listOrderItems.Where(filter).OrderBy(orderItem => ((DO.OrderItem)orderItem!).Id);
    }

    /// <summary>
    /// A request/call method of a single object that receives an order item ID number and returns the appropriate order item
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundExceptions"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.OrderItem? Get(int id) =>
    XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(o_orderItems).FirstOrDefault(orderItem => orderItem?.Id == id)
    ?? throw new NotFoundExceptions("The orderItem id is not exist in List");


    /// <summary>
    /// An add object method that receives an object of an order item and returns the ID number of the added order item
    /// </summary>
    /// <param name="orderItem"></param>
    /// <returns></returns>
    /// <exception cref="DuplicateIdExceptions"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(DO.OrderItem orderItem)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(o_orderItems);

        if (listOrderItems.Exists(orderItem1 => ((DO.OrderItem)orderItem1!).Id == orderItem.Id))
            throw new DuplicateIdExceptions("no place in List to add");


        List<ImportentNumbers> runningList = XMLTools.LoadListFromXMLSerializer1<ImportentNumbers>(configPath);

        ImportentNumbers runningNum = (from number in runningList
                                       where (number.typeOfnumber == "Number Runing OrderItem")
                                       select number).FirstOrDefault();

        runningList.Remove(runningNum);
        runningNum.numberSaved++;
        orderItem.Id = (int)runningNum.numberSaved;
        runningList.Add(runningNum);


        listOrderItems.Add(orderItem);

        XMLTools.SaveListToXMLSerializer1(runningList, configPath);
        XMLTools.SaveListToXMLSerializer(listOrderItems, o_orderItems);

        return orderItem.Id;

    }


    /// <summary>
    ///  A method to delete an order items object that receives an order item ID number
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="NotFoundExceptions"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(o_orderItems);

        if (listOrderItems.RemoveAll(orderItem => orderItem?.Id == id) == 0)
            throw new NotFoundExceptions("The orderItem is not exist in the List");

        XMLTools.SaveListToXMLSerializer(listOrderItems, o_orderItems);
    }


    /// <summary>
    /// An object update method that will receive a new order item
    /// </summary>
    /// <param name="orderItem"></param>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.OrderItem orderItem)
    {
        int id = orderItem.Id;
        Delete(orderItem.Id);
        orderItem.Id = id;
        var orderItemList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(o_orderItems);
        if (orderItemList.Exists(item => item.Value.Id == orderItem.Id))
            throw new Exception("ID already exist");
        orderItemList.Add(orderItem);
        XMLTools.SaveListToXMLSerializer(orderItemList, o_orderItems);

    }


    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.OrderItem? GetF(Func<DO.OrderItem?, bool>? filter)
    {
        var orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(o_orderItems)!;
        if (orderItemsList.FirstOrDefault(filter) == null)
        {
            throw new Exception("The orderItem id is not exist in List");
        }
        return orderItemsList.FirstOrDefault(filter);
    }





}
