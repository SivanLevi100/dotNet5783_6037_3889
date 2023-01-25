using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using BlApi;
using BO;
//using BO;
using DalApi;
using DO;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    private DalApi.IDal? Dal = DalApi.Factory.Get();

    /// <summary>
    /// get method of order list
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.NotExiestsExceptions"></exception>
    public IEnumerable<BO.OrderForList> GetOrderList()
    {

        try
        {
            //Going through all the items in the order list in DAL
            //and returning an order according to a supplier ID
            return from item in Dal?.Order.GetAll()
                   where item != null
                   let order = ((DO.Order)item)!
                   let listOrderItems = Dal!.OrderItem.GetAll(o => ((DO.OrderItem)o!).OrderId == order.Id).Cast<DO.OrderItem>()
                   select new BO.OrderForList
                   {
                       OrderId = order.Id,
                       CustomerName = order.CustomerName,
                       AmountItems = (from orderitem in listOrderItems select orderitem).Sum(or => or.Amount),
                       TotalPrice = listOrderItems.Sum(orderItem => orderItem.Amount * orderItem.Price),
                       Status = statusFromDate(order)
                   };

        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("Order request failed", str);
        }

    }

    /// <summary>
    /// A method that receives an order ID number and returns the order's details
    /// </summary>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    /// <exception cref="BO.NotExiestsExceptions"></exception>
    public BO.Order GetOrderDetails(int idOrder)
    {
        try
        {
            if (idOrder > 0)
            {
                DO.Order orderDO = Dal?.Order.Get(idOrder) ?? throw new BO.NotExiestsExceptions("The Order is not exiests");
                BO.OrderStatus status1 = statusFromDate(orderDO);
                double sumOfPrices = 0;
                //Creating an instance of order items based on the order ID received
                IEnumerable<DO.OrderItem?>? orderItemListDo = Dal.OrderItem.GetAll(orderitem => ((DO.OrderItem)orderitem!).OrderId == idOrder);
                //Going over the list of order items and calculating the price of all items
                foreach (DO.OrderItem orderItem in orderItemListDo)
                {
                    sumOfPrices += orderItem.Price * orderItem.Amount;
                }
                //Create an instance of an order with the new values
                BO.Order order = new BO.Order
                {
                    Id = orderDO.Id,
                    CustomerName = orderDO.CustomerName,
                    CustomerAdress = orderDO.CustomerAdress,
                    CustomerEmail = orderDO.CustomerEmail,
                    OrderDate = orderDO.OrderDate,
                    ShipDate = orderDO.ShipDate,
                    DeliveryDate = orderDO.DeliveryDate,
                    Status = status1,
                    TotalPrice = sumOfPrices,
                    OrdersItemsList = getBOlistOfOrderItem(orderDO.Id)

                };
                return order;
            }
            else
                throw new BO.NotExiestsExceptions("Order request failed/**");
        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("Order request failed/*****", str);
        }
        catch (BO.NotExiestsExceptions str)
        {
            throw new BO.NotExiestsExceptions("Order request failed/*/*/*", str);
        }

    }
    /// <summary>
    /// A method that updates the order delivery date
    /// </summary>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    /// <exception cref="BO.IncorrectDataExceptions"></exception>
    /// <exception cref="BO.NotExiestsExceptions"></exception>
    public BO.Order UpdateDelivery(int idOrder)
    {
        DO.Order orderDO;
        if (idOrder <= 0) throw new BO.IncorrectDataExceptions("Order id is incorrect");
        try
        {
            orderDO = Dal?.Order.Get(idOrder) ?? throw new BO.NotExiestsExceptions("The Order is not exiests");
            //if the order was not delivered
            if (orderDO.OrderDate != null && orderDO.DeliveryDate == null && orderDO.ShipDate != null) 
            {
                orderDO.DeliveryDate = DateTime.Now;
                Dal.Order.Update(orderDO); //Update in the data layer
                //Create an instance of an order with the new values
                BO.Order orderBO = new BO.Order
                {
                    Id = orderDO.Id,
                    CustomerName = orderDO.CustomerName,
                    CustomerAdress = orderDO.CustomerAdress,
                    CustomerEmail = orderDO.CustomerEmail,
                    OrderDate = orderDO.OrderDate,
                    ShipDate = orderDO.ShipDate,
                    DeliveryDate = DateTime.Now,
                    Status = BO.OrderStatus.delivered,
                    OrdersItemsList = getBOlistOfOrderItem(idOrder),
                    TotalPrice = Dal.OrderItem.GetAll(orderItem => ((DO.OrderItem)orderItem!).OrderId == idOrder)
                    .Sum(orderItem => ((DO.OrderItem)orderItem!).Price * ((DO.OrderItem)orderItem!).Amount)
                };
                return orderBO;
            }
            else
                throw new BO.NotExiestsExceptions("The order has not been sent so it is not possible to update order delivery\n");
        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("Order request failed", str);
        }
    }

    /// <summary>
    /// A method that updates the order shipping date
    /// </summary>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    /// <exception cref="BO.IncorrectDataExceptions"></exception>
    /// <exception cref="BO.NotExiestsExceptions"></exception>
    public BO.Order UpdateShipping(int idOrder)//Order shipping update
    {
        DO.Order orderDO;
        if (idOrder <= 0)
            throw new BO.IncorrectDataExceptions("Order id is incorrect");
        try
        {
            orderDO = Dal?.Order.Get(idOrder) ?? throw new BO.NotExiestsExceptions("The Order is not exiests");
            //if the order was not sent
            if (orderDO.OrderDate != null && orderDO.ShipDate == null && orderDO.DeliveryDate == null)//The order was not sent
            {
                orderDO.ShipDate = DateTime.Now;
                Dal?.Order.Update(orderDO); //Update in the data layer
                //Create an instance of an order with the new values
                BO.Order orderBO = new BO.Order
                {
                    Id = orderDO.Id,
                    CustomerName = orderDO.CustomerName,
                    CustomerAdress = orderDO.CustomerAdress,
                    CustomerEmail = orderDO.CustomerEmail,
                    OrderDate = orderDO.OrderDate,
                    ShipDate = DateTime.Now,
                    DeliveryDate = orderDO.DeliveryDate,
                    Status = BO.OrderStatus.shipped,
                    OrdersItemsList = getBOlistOfOrderItem(idOrder),
                    TotalPrice = Dal?.OrderItem.GetAll(orderItem => ((DO.OrderItem)orderItem!).OrderId == idOrder)
                    .Sum(orderItem => ((DO.OrderItem)orderItem!).Price * ((DO.OrderItem)orderItem!).Amount)
                    ?? throw new BO.NotExiestsExceptions("The Order is not exiests")
                };

                return orderBO;
            }
            else
                throw new BO.NotExiestsExceptions("The order has been delivered so it is not possible to update shipping\n");
        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("Order request failed", str);
        }
       
    }

    /// <summary>
    /// A method that returns the status of the order and the dates of the steps performed so far
    /// </summary>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    /// <exception cref="BO.IncorrectDataExceptions"></exception>
    /// <exception cref="BO.NotExiestsExceptions"></exception>
    public BO.OrderTracking Tracking(int idOrder) //Order Tracking
    {
        if (idOrder <= 0)
            throw new BO.IncorrectDataExceptions("Order id is incorrect");
        if(idOrder<100000)
            throw new BO.IncorrectDataExceptions("Order id is too short");
        DO.Order order = new DO.Order();
        try
        {
            order = Dal?.Order.Get(idOrder) ?? throw new BO.NotExiestsExceptions("The Order is not exiests");
        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("Order request failed", str);
        }
        List<Tuple<DateTime?, string>> track = new List<Tuple<DateTime?, string>>();
        if (order.DeliveryDate != null)//The order has been delivered
        {
            track.Add(Tuple.Create(order.DeliveryDate, "The order has been delivered"));
        }
        if (order.ShipDate != null)//The order has been sent
        {
            track.Add(Tuple.Create(order.ShipDate, "The order has been sent"));
        }
        if (order.OrderDate != null)//The order has been created
        {
            track.Add(Tuple.Create(order.OrderDate, "The order has been created"));
        }

        return new BO.OrderTracking
        {
            OrderId = idOrder,
            Status = statusFromDate(order),
            Tracking = track,
        };
    }

    /// <summary>
    /// A function that adds a product to an already existing order 
    /// Amount = How much do you want to add or remove
    /// </summary>
    /// <param name="order"></param>
    /// <param name="idProduct"></param>
    /// <param name="Amount"></param>
    /// <returns></returns>
    /// <exception cref="BO.NotExiestsExceptions"></exception>

    public BO.Order AddItemForOrder(BO.Order order, int idProduct, int Amount)
    {
        //It is not possible to add products to a shipment that has left or arrived at the customer
        if (order.ShipDate != null)
            throw new BO.NotExiestsExceptions("Products cannot be added to orders that have been sent or arrived at the customer");

        //It is not possible to add products to a shipment that has left or arrived at the customer
        if (order.ShipDate!=null && order.DeliveryDate!=null)
            throw new BO.NotExiestsExceptions("Products cannot be added to orders that have been sent or arrived at the customer");

        DO.Product doProduct;
        DO.Order doOrder;
        try
        {
            doProduct = Dal?.Product.Get(idProduct) ?? throw new BO.NotExiestsExceptions("The Product is not exiests");
            //doOrder = Dal?.Order.Get(order.Id)?? throw new BO.NotExiestsExceptions("The Order is not exiests");
        }
        catch (DO.NotFoundExceptions str)
        {
            throw new BO.NotExiestsExceptions("The Product is not exiests in the list of product", str);
        }
        //There is not enough stock for this product
        if (doProduct.InStock < Amount)
            throw new BO.NotExiestsExceptions("The Product is not exiexts in inStock");

        BO.OrderItem orderItemBo;
        int numberOrderItem;
        orderItemBo = order.OrdersItemsList?.FirstOrDefault(orderItem => orderItem?.ProductId == idProduct)!;//Checking whether the product is available in the order
        if (orderItemBo == null) // If the product does not exist in the order
        {
            DO.OrderItem doOrderItem = new DO.OrderItem
            {
                Id = 0,
                OrderId = order.Id,
                ProductId = idProduct,
                Price = doProduct.Price,
                Amount = Amount
            };
            try
            {
                numberOrderItem = Dal.OrderItem.Add(doOrderItem);
            }
            catch (DO.DuplicateIdExceptions str)
            {
                throw new BO.NotExiestsExceptions("Failed to add orderItem to data tier", str);
            }
            BO.OrderItem BOnewOrderItem = new BO.OrderItem
            {
                Id = numberOrderItem,
                ProductId = idProduct,
                NameProduct =doProduct.Name,
                Price = doProduct.Price,
                AmountInOrder =Amount,
                TotalPriceOfItem=Amount* doProduct.Price
            };
            order.OrdersItemsList?.Add(BOnewOrderItem);
            order.TotalPrice += doProduct.Price * Amount;
            doProduct.InStock = doProduct.InStock - Amount;
            Dal.Product.Update(doProduct);
           
        }
        else //Order item is on order and want to add more
        {
            DO.OrderItem item = Dal?.OrderItem.Get(orderItemBo.Id)?? throw new BO.NotExiestsExceptions("OrderIte is null");
            item.Amount += Amount;

            orderItemBo.AmountInOrder += Amount;
            doProduct.InStock = Amount > 0 ? doProduct.InStock - Amount : doProduct.InStock + (-1 * Amount);
            order.TotalPrice += orderItemBo.Price * Amount;
            Dal.Product.Update(doProduct);

            Dal.OrderItem.Update(item);

        }
        return order;

    }

    /// <summary>
    /// The method of selecting the next order to handle
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.NotExiestsExceptions"></exception>
    public int? GetNextOrder()
    {
        DO.Order ordrDo=new DO.Order();
        IEnumerable<DO.Order?> orderList = Dal.Order.GetAll(item => item.Value.DeliveryDate == null);
        if(orderList!=null)//There are orders that have not been delivered
        {
            DO.Order? orderDateMin = orderList.MinBy(item => ((DO.Order)item!).OrderDate);
            DO.Order? shipDateMin = orderList.MinBy(item => ((DO.Order)item!).ShipDate);
            if(orderDateMin != null) 
            {
                if (orderDateMin.Value.ShipDate < shipDateMin.Value.ShipDate)
                    return orderDateMin.Value.Id;
                else
                    return shipDateMin.Value.Id;
            }
            else 
            {
                if (orderDateMin.Value.OrderDate< shipDateMin.Value.ShipDate)
                    return orderDateMin.Value.Id;
                else
                    return shipDateMin.Value.Id;
            }

        }
        else
            throw new BO.NotExiestsExceptions("List of Order list is empty");

    }



    //Helper function to return a list of order details
    private List<BO.OrderItem?>? getBOlistOfOrderItem(int id)
    {

        List<BO.OrderItem?>? listBo = new();

        foreach (DO.OrderItem doOrderItem in Dal?.OrderItem.GetAll(orderItem => ((DO.OrderItem)orderItem!).OrderId == id) ?? throw new BO.NotExiestsExceptions("The Order is not exiests"))
        {
            listBo.Add(new BO.OrderItem
            {
                Id = doOrderItem.Id,
               NameProduct = Dal?.Product?.Get(doOrderItem.ProductId).Value.Name,/////////////
                ProductId = doOrderItem.ProductId,
                Price = doOrderItem.Price,
                AmountInOrder = doOrderItem.Amount,
                TotalPriceOfItem = doOrderItem.Price * doOrderItem.Amount,

            });
        }
        return listBo;
    }

    /// <summary>
    /// Helper function that returns the status of the order
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    private BO.OrderStatus statusFromDate(DO.Order order)
    {

        if (order.DeliveryDate != null) //The order has been delivered
            return BO.OrderStatus.delivered;

        if (order.ShipDate != null) //The order has been sent
            return BO.OrderStatus.shipped;

        if (order.OrderDate != null) //The order has been created
            return BO.OrderStatus.Confirmed;

        return BO.OrderStatus.Unknown;
    }


}
