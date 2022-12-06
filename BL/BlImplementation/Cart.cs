using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using BlApi;
using Dal;
using DalApi;
using Microsoft.VisualBasic;

namespace BlImplementation;

internal class Cart: BlApi.ICart
{
    private IDal Dal = new DalList();

    public BO.Cart AddProduct(BO.Cart cart1, int id)
    {
        if (id <= 0) 
            throw new BO.IncorrectDataExceptions("Product Id is not positive number");
        if (cart1 == null) 
            throw new BO.NotExiestsExceptions("Missing cart object");

        DO.Product doProduct;
        try    //נבדוק האם המוצר קיים
        {
            doProduct = Dal.Product.Get(id);
        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("Product request failed", str);

        }
        if (cart1.OrdersItemsList.Exists(orderItem => orderItem.ProductId != doProduct.Id)) //אם מוצר לא קיים בסל קניות
        {
            if (doProduct.Id == id && doProduct.InStock >= 0) //תבדוק האם המוצר קיים ויש במלאי
            {
                BO.OrderItem newOrderItem = new BO.OrderItem
                {
                    Id = 0, //////////////?
                    ProductId = doProduct.Id,
                    NameProduct = doProduct.Name,
                    Price = doProduct.Price,
                    AmountInOrder = 1,
                    TotalPriceOfItem = doProduct.Price

                };
                cart1.OrdersItemsList.Append(newOrderItem); //מוסיף ערך לסוף הרצף
                cart1.TotalPrice = cart1.TotalPrice + newOrderItem.TotalPriceOfItem;
            }
            else
                throw new BO.NotExiestsExceptions("The product does not exist or is out of stock");
        }
        else  //אם המוצר כבר מופיע בסל קניות
        {   
            foreach (BO.OrderItem orderItem in cart1.OrdersItemsList)
            {
                if(orderItem.ProductId == doProduct.Id && doProduct.InStock >= 0) //אם זה המוצר והכמות גדולה מ0 
                {
                    orderItem.AmountInOrder = orderItem.AmountInOrder + 1;
                    orderItem.TotalPriceOfItem = doProduct.Price * orderItem.AmountInOrder;
                    cart1.TotalPrice = cart1.TotalPrice + orderItem.TotalPriceOfItem;
                    break;
                }
            }
        }
        return cart1;
    }

    public BO.Cart UpdateAmountOfProduct(BO.Cart cart1, int id, int newAmount)
    {
        DO.Product doProduct;
        try    //נבדוק האם המוצר קיים
        {
            doProduct = Dal.Product.Get(id);
        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("Product request failed", str);

        }
        //DO.Product product = Dal.Product.Get(id);
        foreach (BO.OrderItem orderItem in cart1.OrdersItemsList)
        {
            if (orderItem.ProductId == id && newAmount > orderItem.AmountInOrder) //אם הכמות גדלה
            {
                if (doProduct.InStock >= 0)//אם יש מוצר במלאי
                {
                    orderItem.AmountInOrder = newAmount;
                    orderItem.TotalPriceOfItem = doProduct.Price * orderItem.AmountInOrder;
                    cart1.TotalPrice = cart1.TotalPrice + orderItem.TotalPriceOfItem;
                }
                else
                    throw new BO.NotExiestsExceptions("The product is not in stock");
            }
            if (orderItem.ProductId == id && newAmount < orderItem.AmountInOrder) //אם הכמות קטנה
            {
                orderItem.AmountInOrder = newAmount;
                orderItem.TotalPriceOfItem = doProduct.Price * newAmount;
                cart1.TotalPrice = cart1.TotalPrice + orderItem.TotalPriceOfItem;
            }
            if (orderItem.ProductId == id && newAmount + orderItem.AmountInOrder == 0) //אם הכמות נהייתה 0
            {
                cart1.TotalPrice = cart1.TotalPrice - orderItem.TotalPriceOfItem; //עדכון מחיר סל
                cart1.OrdersItemsList.Remove(orderItem);

                //cart1.OrdersItemsList/*.ToList()*/.Remove(orderItem);
            }
        }
        return cart1;   
    }

    public void Confirm(BO.Cart cart1)
    {
        if (string.IsNullOrWhiteSpace(cart1.CustomerName) && string.IsNullOrWhiteSpace(cart1.CustomerAdress)) //מחרוזת ריקה ולא חוקית
            throw new BO.IncorrectDataExceptions("Buyer's name and address are blank");

        if (!new EmailAddressAttribute().IsValid(cart1.CustomerName))//כתובת אימיל לא חוקית
            throw new BO.IncorrectDataExceptions("Email address in invalid format");

        //if (cart1.OrdersItemsList.Any(orderItem => orderItem.ProductId != Dal.Product.Get(orderItem.ProductId).Id))//מוצר לא קיים
        //    throw new BO.NotExiestsExceptions("The product does not exist");

        foreach (BO.OrderItem orderItem in cart1.OrdersItemsList) //כל המוצרים קיימים, כמויות חיוביות, יש מספיק במלאי
        {
            DO.Product productOfDo = Dal.Product.Get(orderItem.ProductId);
            if(Dal.Product.GetList().Contains(productOfDo) == false)//מוצר לא קיים
                throw new BO.NotExiestsExceptions("The product does not exist");
            if (orderItem.AmountInOrder <= 0) //כמות שלילית
                throw new BO.IncorrectDataExceptions("Invalid item quantity");
            if(orderItem.AmountInOrder > Dal.Product.Get(orderItem.ProductId).InStock) //אין מספיק במלאי
                throw new BO.IncorrectDataExceptions("This product is out of stock");
        }
        DO.Order doOrder = new DO.Order
        {
            CustomerName = cart1.CustomerName,
            CustomerAdress = cart1.CustomerAdress,
            CustomerEmail = cart1.CustomerEmail,
            DeliveryDate = DateTime.MinValue,
            OrderDate = DateTime.Now,
            ShipDate = DateTime.MinValue
        };
        int numberOrder;
        try
        {
            numberOrder = Dal.Order.Add(doOrder);  //הוספת הזמנה לשכבת הנתונים
        }
        catch(DO.DuplicateIdExceptions str)
        {
            throw new BO.NotExiestsExceptions("Failed to add order to data tier", str);
        }
        foreach(BO.OrderItem boOrderItem in cart1.OrdersItemsList)//בניית אוביקטים של פריט הזמנה והוספתם לשכבת הנתונים
        {
            DO.OrderItem doOrderItem = new DO.OrderItem
            {
                //Id = 0,
                OrderId = numberOrder,
                ProductId = boOrderItem.ProductId,
                Price = boOrderItem.Price,
                Amount = boOrderItem.AmountInOrder
            };
            try
            {
                Dal.OrderItem.Add(doOrderItem);
            }
            catch (DO.DuplicateIdExceptions str)
            {
                throw new BO.NotExiestsExceptions("Failed to add orderItem to data tier", str);
            }
        }
        foreach (BO.OrderItem boOrderItem in cart1.OrdersItemsList)//אישור ההזמנה
        {
            try
            {
                DO.Product product = Dal.Product.Get(boOrderItem.ProductId); //בקשת מוצר משכבת הנתונים
                product.InStock= product.InStock - boOrderItem.AmountInOrder; //עדכון המלאי
                Dal.Product.Update(product); //עדכון מוצר 
            }
            catch (DO.DuplicateIdExceptions str)
            {
                throw new BO.NotExiestsExceptions("Failed to request data layer products and updates", str);
            }
        }
        cart1.OrdersItemsList.Clear(); //ריקון הסל כלומר מחקית רשימת פריטי הזמנה מהסל
    }





}
