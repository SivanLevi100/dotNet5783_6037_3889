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

public partial class MyCartWindow : Window
{
    private BlApi.IBl? bl = BlApi.Factory.Get();

    public static readonly DependencyProperty OrderItemsDependency = DependencyProperty.Register(nameof(OrdertItemsOfCart), typeof(ObservableCollection<BO.OrderItem?>), typeof(Window));
    public ObservableCollection<BO.OrderItem?> OrdertItemsOfCart
    {
        get => (ObservableCollection<BO.OrderItem?>)GetValue(OrderItemsDependency);
        private set => SetValue(OrderItemsDependency, value);
    }

   

    public MyCartWindow()
    {
        //  OrdertItemsOfCart = MyCart.OrdersItemsList.Cast<OrderItem>();  //הצגת הפריטים בסל 
        InitializeComponent();
    }

    private void OrderConfirmationButton_Click(object sender, RoutedEventArgs e)
    {
        new OrderConfirmationWindow().Show();
    }
}
