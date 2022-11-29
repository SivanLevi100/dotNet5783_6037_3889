using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlApi;
using BO;
using Dal;
using DalApi;
using DO;

namespace BlImplementation;

internal class Cart:ICart
{
    private IDal Dal = new DalList();

    public BO.Cart AddProduct(BO.Cart cart1, int id)
    {
        //try { } להוסיף
        DO.Product product = Dal.Product.Get(id);
        foreach (BO.OrderItem orderItem in cart1.OrdersItemsList)
        {
            if (orderItem.Id != product.Id) //אם מוצר לא קיים בסל קניות
            {
                if (product.Id == id && product.InStock >= 0) //תבדוק האם המוצר קיים ויש במלאי
                {
                    BO.OrderItem newOrderItem = new BO.OrderItem
                    {
                        ProductId = product.Id,
                        NameProduct = product.Name,
                        Price = product.Price,
                        AmountInOrder = 1,
                        TotalPriceOfItem = product.Price

                    };
                    cart1.OrdersItemsList.Append(newOrderItem); //מוסיף ערך לסוף הרצף
                    cart1.TotalPrice = cart1.TotalPrice + product.Price;
                }
                else
                    throw new NotExiestsExceptions("The product does not exist or is out of stock", str);
            }
            else               //אם המוצר כבר מופיע בסל קניות
            {
                if (product.InStock >= 0)
                {
                    orderItem.AmountInOrder = orderItem.AmountInOrder + 1;
                    orderItem.TotalPriceOfItem= product.Price;
                    cart1.TotalPrice = cart1.TotalPrice + product.Price;
                    ////
                }
            }

        }
        return cart1;
    }
    public BO.Cart UpdateAmountOfProduct(BO.Cart cart1, int id, int newAmount)
    {
        DO.Product product = Dal.Product.Get(id);
        foreach (BO.OrderItem orderItem in cart1.OrdersItemsList)
        {
            if (orderItem.ProductId == id && newAmount > orderItem.AmountInOrder)
            {
                if (orderItem.Id == id)
                {
                    if (product.InStock >= 0)
                    {
                        orderItem.AmountInOrder = newAmount/*orderItem.AmountInOrder + 1*/;
                        orderItem.TotalPriceOfItem = product.Price;
                        cart1.TotalPrice = cart1.TotalPrice + newAmount * product.Price;

                    }
                }
            }
            if (orderItem.ProductId == id && newAmount < orderItem.AmountInOrder)
            {
                orderItem.AmountInOrder = newAmount;
                orderItem.TotalPriceOfItem = product.Price;
                cart1.TotalPrice = cart1.TotalPrice + newAmount * product.Price;
            }
            if (orderItem.ProductId == id && newAmount + orderItem.AmountInOrder == 0)
            {
                // cart1.OrdersItemsList.Where(orderItem => orderItem.ProductId == id && newAmount + orderItem.AmountInOrder == 0);
                cart1.OrdersItemsList.ToList().Remove(orderItem);                  //  תִּמְחַק את הפריט
                cart1.TotalPrice = cart1.TotalPrice - product.Price;

            }

        }
        //cart1.OrdersItemsList.Where(orderItem1 => orderItem1.ProductId == id && newAmount + orderItem1.AmountInOrder == 0);
        //cart1.TotalPrice = cart1.TotalPrice - product.Price;

        return cart1;   
        //להוסיף חריגות
    }

    public void Confirm(BO.Cart cart1, string CustomerName, string CustomerEmail, string CustomerAdress)
    {

    }





}
