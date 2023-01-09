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

    //public static readonly DependencyProperty TotalPriceDependency = DependencyProperty.Register(nameof(TotalPrice), typeof(double), typeof(Window));
    //public double TotalPrice { get => (double)GetValue(TotalPriceDependency); private set => SetValue(TotalPriceDependency, value); }
    public double totalPrice;


    public MyCartWindow()
    {
        List<BO.OrderItem?> Collection = CatalogProductsWindow.myCart.OrdersItemsList;
        foreach(var item in Collection)
        {
            OrdertItemsOfCart.Add(item);
        }
        totalPrice= CatalogProductsWindow.myCart.TotalPrice;
        InitializeComponent();


        //TotalPrice = CatalogProductsWindow.myCart.TotalPrice;
        //OrdertItemsOfCart.Add();   //הצגת הפריטים בסל 
    }

    private void OrderConfirmationButton_Click(object sender, RoutedEventArgs e)
    {
        new OrderConfirmationWindow().Show();
    }

    private void DeleteTheBasketButton_Click(object sender, RoutedEventArgs e)
    {
        CatalogProductsWindow.myCart.OrdersItemsList = null;
        CatalogProductsWindow.myCart.TotalPrice = 0;
        //CatalogProductsWindow.myCart = null;
    }

    private void ExitButton_Click(object sender, RoutedEventArgs e)
    {
        new CatalogProductsWindow().Show();
        Close();
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void RemoveButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void RemoveProductButton_Click(object sender, RoutedEventArgs e)
    {

    }
}
