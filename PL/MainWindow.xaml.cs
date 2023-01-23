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
/// </summary>
public partial class MainWindow : Window
{
    private BlApi.IBl? bl = BlApi.Factory.Get();


    //constructor
    public MainWindow()
    {
        InitializeComponent();
        //IEnumerable<int> orderForLists1 = new List<int>();
        //orderForLists1 = from item in bl?.Order.GetOrderList()
        //                 where item != null
        //                 select item.OrderId;
        //NumberOfOrder.ItemsSource = orderForLists1;

        //Track.IsEnabled = false;

    }

    //Pressing the enter button - opening a product list window
    private void ShowProductButton_Click(object sender, RoutedEventArgs e) => new ProductListWindow().Show();

    private void AdminButton_Click(object sender, RoutedEventArgs e)
    {
        OrderList.Visibility = Visibility.Visible;
        ProductList.Visibility = Visibility.Visible;
        Admin.IsEnabled = false;
    }

    private void NewOrderButton_Click(object sender, RoutedEventArgs e)
    {
        new CatalogProductsWindow().Show();
    }

    private void TrackButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtnumber.Text))
            MessageBox.Show("Enter order number for tracking");
        else
        {
            int idOrder = int.Parse(txtnumber.Text);
            BO.OrderForList order = bl.Order.GetOrderList().FirstOrDefault(o => o.OrderId == idOrder);
            if (order == null)
                MessageBox.Show("The number of order is not exiests");
            else
                new OrderTrackingWindow(idOrder).Show();
        }
        txtnumber.Text = null;
    }

    private void OrderList_Click(object sender, RoutedEventArgs e)
    {
        new OrderListWindow().Show();
        Admin.IsEnabled = true;
    }

    private void ProductList_Click(object sender, RoutedEventArgs e)
    {
        new ProductListWindow().Show();
        Admin.IsEnabled = true;
    }

    private void NumberOfOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //Track.Visibility = Visibility.Visible;
       // Track.IsEnabled = true;
    }

    private void CustomerButton_Click(object sender, RoutedEventArgs e)
    {
        NewOrder.Visibility = Visibility;
        Track.Visibility = Visibility;
        NumberOfOrder.Visibility = Visibility;
        txtnumber.Visibility = Visibility;

    }

    private void SimulatorButton_Click(object sender, RoutedEventArgs e)
    {
        new SimulatorWindow().Show();

    }
}

