using PL.Order;
using PL.Product;
using System;
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
        IEnumerable<int> orderForLists1 = new List<int>();
        orderForLists1 =  from item in bl?.Order.GetOrderList()
                          where item != null
                          select item.OrderId;
        NumberOfOrder.ItemsSource = orderForLists1;

    }

    //Pressing the enter button - opening a product list window
    private void ShowProductButton_Click(object sender, RoutedEventArgs e) => new ProductListWindow().Show();

    private void AdminButton_Click(object sender, RoutedEventArgs e)
    {
        OrderList.Visibility = Visibility.Visible;
        ProductList.Visibility = Visibility.Visible;
    }

    private void NewOrderButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void TrackButton_Click(object sender, RoutedEventArgs e)
    {
        //פותח חלון מעקב הזמנות
    }

    private void OrderList_Click(object sender, RoutedEventArgs e)
    {
        new OrderListWindow().Show();
    }

    private void ProductList_Click(object sender, RoutedEventArgs e)
    {
        new ProductListWindow().Show();
    }
}
