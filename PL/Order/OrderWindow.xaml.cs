using BO;
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

    public OrderWindow(int id)
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
            new OrderListWindow().ShowDialog();
            //new OrderListWindow().Show();
        }

        //InitializeComponent();

        //try
        //{
        //    UpdateShiping.Visibility = Visibility.Hidden;
        //    UpdateDelivery.Visibility = Visibility.Hidden;
        //    BO.Order order = bl?.Order.GetOrderDetails(id);
        //    txtId.Text = order.Id.ToString();
        //    txtCustomerName.Text = order.CustomerName;
        //    txtCustomerEmail.Text = order.CustomerEmail;
        //    txtCustomerAdress.Text = order.CustomerAdress;
        //    txtOrderDate.Text = order.OrderDate.ToString();
        //    txtShipDate.Text = order.ShipDate.ToString();
        //    txtDeliveryDate.Text = order.DeliveryDate.ToString();
        //    txtStatus.Text = order.Status.ToString();
        //    txtTotalPrice.Text = order.TotalPrice.ToString();
        //    OrdersItemListView.ItemsSource = bl.Order.GetOrderList(/*id*/);//צריך להוסיף בשכבה הלוגית בפונקציה של רשימת הזמנות ביטוי עם פילטר 
        //    txtId.IsEnabled = false;
        //    txtCustomerName.IsEnabled = false;
        //    txtCustomerEmail.IsEnabled = false;
        //    txtCustomerAdress.IsEnabled = false;
        //    txtOrderDate.IsEnabled = false;
        //    txtShipDate.IsEnabled = false;
        //    txtDeliveryDate.IsEnabled = false;
        //    txtStatus.IsEnabled = false;
        //    txtTotalPrice.IsEnabled = false;

        //}
        //catch (IncorrectDataExceptions str)
        //{
        //    MessageBox.Show(str.Message, "Failure getting entity", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //    Close();
        //}


    }

    private void UpdateDateOrderButton_Click(object sender, RoutedEventArgs e)
    {
        txtShipDate.IsEnabled = true;
        txtDeliveryDate.IsEnabled = true;
        UpdateDelivery.Visibility = Visibility.Visible;
        UpdateShiping.Visibility= Visibility.Visible;
 
    }

    //private void UpdateDelivery(object sender, RoutedEventArgs e)
    //{
    //    bl.Order.UpdateDelivery(int.Parse(txtId.Text));
    //    MessageBox.Show("update delivery date");
    //    Close();
    //}

    private void UpdateShipingButton_Click(object sender, RoutedEventArgs e)
    {
        bl.Order.UpdateShipping(5);/////////////////////**************************////////////
    }

    private void UpdateDelivery_Click(object sender, RoutedEventArgs e)
    {
        bl.Order.UpdateDelivery(5);////////////////**********************/////////////////////

    }
}
