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
using System.Windows.Shapes;

namespace PL.Order;

/// <summary>
/// Interaction logic for OrderListWindow.xaml
/// </summary>
public partial class OrderListWindow : Window
{
    private BlApi.IBl? bl = BlApi.Factory.Get();

    public OrderListWindow()
    {
        InitializeComponent();
        OrdertListview.ItemsSource = bl?.Order.GetOrderList();
    }

    private void OrdertListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        ListBox listBox = sender as ListBox;
        BO.OrderForList order = new BO.OrderForList();
        order = listBox.SelectedItem as BO.OrderForList;
        new OrderWindow(order.OrderId).Show();
        Close();

    }
}
