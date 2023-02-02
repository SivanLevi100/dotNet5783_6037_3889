using PL.Cart;
using PL.Order;
using PL.Product;
using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// The back window containing all the functions for the Main window
/// </summary>
public partial class MainWindow : Window
{
    private BlApi.IBl? bl = BlApi.Factory.Get();


    //constructor
    public MainWindow()
    {
        InitializeComponent();

    }

    //Pressing the enter button - opening a product list window
    private void ShowProductButton_Click(object sender, RoutedEventArgs e) => new ProductListWindow().Show();

    /// <summary>
    /// Function for the "Admin" button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AdminButton_Click(object sender, RoutedEventArgs e)
    {
        //Showing the "OrderList" and "ProductList" buttons
        OrderList.Visibility = Visibility.Visible;
        ProductList.Visibility = Visibility.Visible;
        Admin.IsEnabled = false;
    }

    /// <summary>
    /// Function for the "New Order" button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NewOrderButton_Click(object sender, RoutedEventArgs e)
    {
        new CatalogProductsWindow().Show();
    }

    /// <summary>
    /// Function for the "Track" button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TrackButton_Click(object sender, RoutedEventArgs e)
    {
        //Checking if an order number has been entered
        if (string.IsNullOrWhiteSpace(txtnumber.Text))
            MessageBox.Show("Enter order number for tracking");
        else
        {
            int idOrder = int.Parse(txtnumber.Text);
            //Displaying the order status
            BO.OrderForList order = bl?.Order?.GetOrderList().FirstOrDefault(o => o.OrderId == idOrder);
            if (idOrder < 100000)
                MessageBox.Show("The number of order is too short");
            else if (order == null)
                MessageBox.Show("The number of order is not exiests");
            
            else
                new OrderTrackingWindow(idOrder).Show();
        }
        txtnumber.Text = null;
    }

    /// <summary>
    /// Function for the "OrderList" button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OrderList_Click(object sender, RoutedEventArgs e)
    {
        //Opening the order list window
        new OrderListWindow().Show();
        Admin.IsEnabled = true;
    }

    /// <summary>
    /// Function for the "ProductList" button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProductList_Click(object sender, RoutedEventArgs e)
    {
        //Opening the product list window
        new ProductListWindow().Show();
        Admin.IsEnabled = true;
    }

    /// <summary>
    ///  A function for the combo box with all number of order
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NumberOfOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //Track.Visibility = Visibility.Visible;
       // Track.IsEnabled = true;
    }

    /// <summary>
    /// Function for the "Customer" button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CustomerButton_Click(object sender, RoutedEventArgs e)
    {
        //Showing the "NewOrder", "NumberOfOrder" and "Track" buttons
        NewOrder.Visibility = Visibility;
        Track.Visibility = Visibility;
        NumberOfOrder.Visibility = Visibility;
        txtnumber.Visibility = Visibility;

    }

    /// <summary>
    /// Function for the "Simulator" button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SimulatorButton_Click(object sender, RoutedEventArgs e)
    {
        //Simulator
        new SimulatorWindow().Show();

    }
}

