using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using BlApi;
using DalApi;
//using DO;
using Microsoft.VisualBasic;

namespace BlImplementation;

internal class Cart : BlApi.ICart
{
    private DalApi.IDal? Dal = DalApi.Factory.Get();

    /// <summary>
    /// Method of adding a product to the cart
    /// </summary>
    /// <param name="cart1"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.IncorrectDataExceptions"></exception>
    /// <exception cref="BO.NotExiestsExceptions"></exception>
    public BO.Cart AddProduct(BO.Cart cart1, int id)
    {
        if (id <= 0)
            throw new BO.IncorrectDataExceptions("Product Id is not positive number");
        if(cart1 == null)
            cart1=new BO.Cart();
        if (cart1.OrdersItemsList == null)
            cart1.OrdersItemsList = new();

        DO.Product doProduct;
        try
        {
            doProduct = Dal?.Product.Get(id) ?? throw new BO.NotExiestsExceptions("The Product is not exiests");
        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("Product request failed", str);
        }
        BO.OrderItem? newOrderItem1= cart1.OrdersItemsList.FirstOrDefault(item=>item?.ProductId==id);
        if (newOrderItem1==null) //If a product does not exist in the shopping basket
        {
            if (doProduct.Id == id && doProduct.InStock > 0) //Check if the product exists and is in stock
            {
                BO.OrderItem newOrderItem = new BO.OrderItem
                {
                    Id = 0,
                    ProductId = doProduct.Id,
                    NameProduct = doProduct.Name,
                    Price = doProduct.Price,
                    AmountInOrder = 1,
                    TotalPriceOfItem = doProduct.Price
                };
                cart1.OrdersItemsList.Add(newOrderItem);
                cart1.TotalPrice = cart1.TotalPrice + newOrderItem.TotalPriceOfItem;
            }
            else
                throw new BO.NotExiestsExceptions("The product does not exist or is out of stock");
        }
        else  //If the product appears in the shopping cart
        {
            if(newOrderItem1.AmountInOrder<doProduct.InStock)
            {
                newOrderItem1.AmountInOrder ++;
                newOrderItem1.TotalPriceOfItem = doProduct.Price * newOrderItem1.AmountInOrder;
                cart1.TotalPrice = cart1.TotalPrice + doProduct.Price;

            }
        }
        return cart1;
    }


    /// <summary>
    /// A method that updates the amount of a product in the basket
    /// </summary>
    /// <param name="cart1"></param>
    /// <param name="id"></param>
    /// <param name="newAmount"></param>
    /// <returns></returns>
    /// <exception cref="BO.NotExiestsExceptions"></exception>
    /// <exception cref="BO.IncorrectDataExceptions"></exception>
    public BO.Cart UpdateAmountOfProduct(BO.Cart cart1, int id, int newAmount)
    {
        DO.Product doProduct;
        BO.OrderItem orderItem;
        try
        {
            doProduct = Dal?.Product.Get(id) ?? throw new BO.NotExiestsExceptions("The Product is not exiests");
        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("Product request failed", str);
        }
        //if(cart1?.OrdersItemsList?.Count() == 0|| cart1?.OrdersItemsList==null|| cart1.OrdersItemsList.Exists(item => item?.ProductId != id))
        //    throw new BO.NotExiestsExceptions("This item cannot be found because it is not in the basket");
        //else
        //    orderItem = cart1?.OrdersItemsList.Find(item => item?.ProductId == id)??throw new BO.NotExiestsExceptions("The product does not exist");
        orderItem = cart1?.OrdersItemsList?.Find(item => item?.ProductId == id)??throw new BO.NotExiestsExceptions("The product does not exist");

        if (newAmount + orderItem?.AmountInOrder == 0)//If the amount = 0
        {
            cart1.TotalPrice = cart1.TotalPrice - orderItem.TotalPriceOfItem; //cart price update
            cart1.OrdersItemsList.Remove(orderItem);
            return cart1;
        }
        if (newAmount > orderItem?.AmountInOrder)//If the amount increases add to the cart
        {
            if (doProduct.InStock > 0)//If there is a product in stock
            {
                orderItem.AmountInOrder = newAmount;
                orderItem.TotalPriceOfItem = doProduct.Price * orderItem.AmountInOrder;
                cart1.TotalPrice = cart1.TotalPrice + doProduct.Price;

            }
            else
                throw new BO.IncorrectDataExceptions("The product is not in stock");
        }
        if (newAmount < orderItem?.AmountInOrder) //If the amount decreases remove from the cart
        {
            orderItem.AmountInOrder = newAmount;
            orderItem.TotalPriceOfItem = doProduct.Price * newAmount;
            cart1.TotalPrice = cart1.TotalPrice - doProduct.Price;

        }

        return cart1;
    }


    /// <summary>
    /// Order confirmation method
    /// </summary>
    /// <param name="cart1"></param>
    /// <exception cref="BO.IncorrectDataExceptions"></exception>
    /// <exception cref="BO.NotExiestsExceptions"></exception>
    public void Confirm(BO.Cart cart1)
    {
        if (string.IsNullOrWhiteSpace(cart1?.CustomerName) && string.IsNullOrWhiteSpace(cart1?.CustomerAdress)) //An empty and invalid string
            throw new BO.IncorrectDataExceptions("Buyer's name and address are blank");

        if (!new EmailAddressAttribute().IsValid(cart1?.CustomerEmail))//Invalid email address
            throw new BO.IncorrectDataExceptions("Email address in invalid format");

        //if (cart1.OrdersItemsList.Any(orderItem => orderItem.ProductId != Dal.Product.Get(orderItem.ProductId).Id))//מוצר לא קיים
        //    throw new BO.NotExiestsExceptions("The product does not exist");

        if (cart1?.OrdersItemsList == null)
            throw new BO.NotExiestsExceptions("The shopping cart is empty");

        foreach (BO.OrderItem? orderItem in cart1?.OrdersItemsList ?? throw new BO.NotExiestsExceptions("The Cart is not exiests")) //All products exist, positive quantities, enough in stock
        {
            DO.Product productOfDo = Dal?.Product.Get(orderItem?.ProductId ?? throw new BO.NotExiestsExceptions("The OrderItem is not exiests")) ?? throw new BO.NotExiestsExceptions("The Product is not exiests");
            if (Dal?.Product.GetAll().Contains(productOfDo) == false)//Product does not exist
                throw new BO.NotExiestsExceptions("The product does not exist");
            if (orderItem.AmountInOrder <= 0) //negative quantity
                throw new BO.IncorrectDataExceptions("Invalid item quantity");
            if (orderItem.AmountInOrder > Dal?.Product.Get(orderItem.ProductId).Value.InStock) //Not enough in stock
                throw new BO.IncorrectDataExceptions("This product is out of stock");
        }

        DO.Order doOrder = new DO.Order
        {
            Id = 0,
            CustomerName = cart1.CustomerName,
            CustomerAdress = cart1.CustomerAdress,
            CustomerEmail = cart1.CustomerEmail,
            DeliveryDate = null,
            OrderDate = DateTime.Now,
            ShipDate = null
        };
        int numberOrder;
        try
        {
            numberOrder = Dal?.Order.Add(doOrder) ?? throw new BO.NotExiestsExceptions("The Order is not exiests");  //Add an order to the data layer
        }
        catch (DO.DuplicateIdExceptions str)
        {
            throw new BO.NotExiestsExceptions("Failed to add order to data tier", str);
        }

        foreach (BO.OrderItem? boOrderItem in cart1.OrdersItemsList)//Building order item objects and adding them to the data layer
        {
            DO.OrderItem doOrderItem = new DO.OrderItem
            {
                Id = boOrderItem?.Id ?? throw new BO.NotExiestsExceptions("The OrderItem is not exiests"),
                OrderId = numberOrder,
                ProductId = boOrderItem?.ProductId ?? throw new BO.NotExiestsExceptions("Order item null"),
                Price = boOrderItem.Price,
                Amount = boOrderItem.AmountInOrder
            };
            try
            {
                Dal?.OrderItem.Add(doOrderItem);
            }
            catch (DO.DuplicateIdExceptions str)
            {
                throw new BO.NotExiestsExceptions("Failed to add orderItem to data tier", str);
            }
        }


        foreach (BO.OrderItem? boOrderItem in cart1.OrdersItemsList) //Order Confirmation
        {
            try
            {
                DO.Product product = Dal?.Product.Get(boOrderItem.ProductId) ?? throw new BO.NotExiestsExceptions("The Product is not exiests"); //Request a product from the data layer
                product.InStock = product.InStock - boOrderItem.AmountInOrder; //In Stock update
                Dal?.Product.Update(product); //Update Product
            }
            catch (DO.DuplicateIdExceptions str)
            {
                throw new BO.NotExiestsExceptions("Failed to request data layer products and updates", str);
            }
        }

        cart1.OrdersItemsList.Clear(); //Emptying the basket means deleting a list of order items from the basket
    }

}
