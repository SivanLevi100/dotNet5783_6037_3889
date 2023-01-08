using BO;
using DO;
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
using System.Xml.Linq;

namespace PL.Order;


public partial class OrderWindow : Window
{
    private BlApi.IBl? bl = BlApi.Factory.Get();

    public static readonly DependencyProperty OrderDependency = DependencyProperty.Register(nameof(Order), typeof(BO.Order), typeof(Window));
    public BO.Order? Order { get => (BO.Order)GetValue(OrderDependency); private set => SetValue(OrderDependency, value); }

    public OrderWindow(int id=0)
    {
        try
        {
            Order = id == 0 ? new() { Status=BO.OrderStatus.Unknown} : bl?.Order.GetOrderDetails(id);
            InitializeComponent();
            //datagrid.ItemsSource = bl.Order.GetOrderDetails(id).OrdersItemsList;
            //OrdersItemListView.ItemsSource = bl?.Order.GetOrderDetails(id).OrdersItemsList; 
        }
        catch (BO.IncorrectDataExceptions ex)
        {
            MessageBox.Show(ex.Message, "Failure getting entity", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            Close();
            //new OrderListWindow().ShowDialog();
            new OrderListWindow().Show();
        }
        //catch (BO.NotExiestsExceptions ex)
        //{
        //    MessageBox.Show(ex.Message, "Failure getting entity", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //    Close();
        //    //new OrderListWindow().ShowDialog();
        //    new OrderListWindow().Show();
        //}



    }

    private void AddProductForOrderButton_Click(object sender, RoutedEventArgs e)
    {
 

    }


    private void UpdateShipingButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Order=bl?.Order.UpdateShipping(Order.Id);

        }
        catch (BO.NotExiestsExceptions ex)
        {
            MessageBox.Show(ex.Message, "Shipping date cannot be updated", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            Close();
            new OrderListWindow().Show();
        }


        
    }

    private void UpdateDelivery_Click(object sender, RoutedEventArgs e)
    {
        try
        {
           Order= bl?.Order.UpdateDelivery(Order.Id);

        }
        catch (BO.NotExiestsExceptions ex)
        {
            MessageBox.Show(ex.Message, "Delivery date cannot be updated", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            Close();
            new OrderListWindow().Show();
        }

    }
}
