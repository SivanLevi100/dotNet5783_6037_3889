using BlApi;
using BO;
using PL.Order;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Cart;

/// <summary>
/// The back window containing all the functions for the cart window
/// </summary>
public partial class MyCartWindow : Window
{
    private BlApi.IBl? bl = BlApi.Factory.Get();

    public static readonly DependencyProperty OrderItemsDependency = DependencyProperty.Register(nameof(OrdertItemsOfCart), typeof(ObservableCollection<BO.OrderItem?>), typeof(Window));
    public ObservableCollection<BO.OrderItem?> OrdertItemsOfCart
    {
        get => (ObservableCollection<BO.OrderItem?>)GetValue(OrderItemsDependency);
        private set => SetValue(OrderItemsDependency, value);
    }

    public static readonly DependencyProperty TotalPriceDependency = DependencyProperty.Register(nameof(TotalPrice), typeof(double), typeof(Window));
    public double TotalPrice { get => (double)GetValue(TotalPriceDependency); private set => SetValue(TotalPriceDependency, value); }


    public MyCartWindow()
    {
        var temp = CatalogProductsWindow.myCart.OrdersItemsList;
        OrdertItemsOfCart = temp == null ? new() : new(temp);

        InitializeComponent();
        TotalPrice = CatalogProductsWindow.myCart.TotalPrice;
    }

    /// <summary>
    /// Function for the "order confirmation" button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OrderConfirmationButton_Click(object sender, RoutedEventArgs e)
    {
        if(CatalogProductsWindow.myCart.OrdersItemsList?.FirstOrDefault(x=>x?.TotalPriceOfItem==0)!=null)
        {
            //Opens the order confirmation window and closes the cart window
            new OrderConfirmationWindow().Show();
            Close();
        }
        else
            MessageBox.Show("The Cart is empty");


    }

    /// <summary>
    /// Function for the "delete the basket" button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DeleteTheBasketButton_Click(object sender, RoutedEventArgs e)
    {
        //Resetting the general price and deleting the products in the back window
        CatalogProductsWindow.myCart.OrdersItemsList = null;
        CatalogProductsWindow.myCart.TotalPrice = 0;

        //Updating the values ​​in the cart window
        var temp = CatalogProductsWindow.myCart.OrdersItemsList;
        OrdertItemsOfCart = temp == null ? new() : new(temp);
        TotalPrice = CatalogProductsWindow.myCart.TotalPrice;
    }

    /// <summary>
    /// Function for the "back to home window" button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void HomeButton_Click(object sender, RoutedEventArgs e)
    {
        //Opening the home window and closing the current window
        new MainWindow().Show();
        Close();
    }

    /// <summary>
    /// Function for the "add" button in the cart window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            //Add quantity to an existing product in the basket window
            BO.OrderItem orderItem= (BO.OrderItem)((sender as Button)!.DataContext!);
            CatalogProductsWindow.myCart = bl.Cart.AddProduct(CatalogProductsWindow.myCart, orderItem.ProductId);
            var temp = CatalogProductsWindow.myCart.OrdersItemsList;
            OrdertItemsOfCart = temp == null ? new() : new(temp);

            TotalPrice = CatalogProductsWindow.myCart.TotalPrice;


           // MessageBox.Show("The Product added to cart");
        }
        catch (BO.NotExiestsExceptions ex)
        {
            MessageBox.Show(ex.Message, "The product is out of stock");
        }
    }

    /// <summary>
    /// Function for the "remove" button in the cart window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RemoveButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            //reduction in quantity to an existing product in the basket window
            BO.OrderItem orderItem = (BO.OrderItem)((sender as Button)!.DataContext!);
            CatalogProductsWindow.myCart = bl.Cart.UpdateAmountOfProduct(CatalogProductsWindow.myCart, orderItem.ProductId, orderItem.AmountInOrder - 1);
            var temp = CatalogProductsWindow.myCart.OrdersItemsList;
            OrdertItemsOfCart = temp == null ? new() : new(temp);
            
            TotalPrice = CatalogProductsWindow.myCart.TotalPrice;

            //MessageBox.Show("The Item removed from cart");
        }
        catch (BO.NotExiestsExceptions ex)
        {
            MessageBox.Show(ex.Message, "Failed to remove a product because it is not in the cart");
        }
    }

    /// <summary>
    /// Function for the "remove product" button in the cart window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RemoveProductButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            //Removing a product from the cart
            BO.OrderItem orderItem = (BO.OrderItem)((sender as Button)!.DataContext!);
            CatalogProductsWindow.myCart = bl.Cart.UpdateAmountOfProduct(CatalogProductsWindow.myCart, orderItem.ProductId, (orderItem.AmountInOrder)*(-1));
           // MessageBox.Show("The Product removed from cart");
        }
        catch (BO.NotExiestsExceptions ex)
        {
            MessageBox.Show(ex.Message, "Failed to remove a product because it is not in the cart");
        }
        var temp = CatalogProductsWindow.myCart.OrdersItemsList;
        OrdertItemsOfCart = temp == null ? new() : new(temp);
        TotalPrice = CatalogProductsWindow.myCart.TotalPrice;

    }

    /// <summary>
    /// Function for the "back to previous window" button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        //Opening the catalog window and closing the current window
        new CatalogProductsWindow().Show();
        Close();
    }
}
