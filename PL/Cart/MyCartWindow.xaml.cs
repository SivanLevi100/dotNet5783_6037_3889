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

    public static readonly DependencyProperty TotalPriceDependency = DependencyProperty.Register(nameof(TotalPrice), typeof(double), typeof(Window));
    public double TotalPrice { get => (double)GetValue(TotalPriceDependency); private set => SetValue(TotalPriceDependency, value); }


    public MyCartWindow()
    {
        var temp = CatalogProductsWindow.myCart.OrdersItemsList;
        OrdertItemsOfCart = temp == null ? new() : new(temp);

        InitializeComponent();
        TotalPrice = CatalogProductsWindow.myCart.TotalPrice;
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
        try
        {
            BO.OrderItem orderItem= (BO.OrderItem)((sender as Button)!.DataContext!);
            //ProductItem productItem = (ProductItem)((sender as Button)!.DataContext!);
            CatalogProductsWindow.myCart = bl.Cart.AddProduct(CatalogProductsWindow.myCart, orderItem.ProductId);
            MessageBox.Show("The Product added to cart");

        }
        catch (BO.NotExiestsExceptions ex)
        {
            MessageBox.Show(ex.Message, "The product is out of stock");
        }
    }

    private void RemoveButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            BO.OrderItem orderItem = (BO.OrderItem)((sender as Button)!.DataContext!);
            CatalogProductsWindow.myCart = bl.Cart.UpdateAmountOfProduct(CatalogProductsWindow.myCart, orderItem.ProductId, orderItem.AmountInOrder - 1);

            // ProductItem = bl?.Product.GetProductDetailsBuyer(ProductItem.IdProduct, CatalogProductsWindow.myCart);
            MessageBox.Show("The Item removed from cart");
        }
        catch (BO.NotExiestsExceptions ex)
        {
            MessageBox.Show(ex.Message, "Failed to remove a product because it is not in the cart");
        }
    }

    private void RemoveProductButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            BO.OrderItem orderItem = (BO.OrderItem)((sender as Button)!.DataContext!);
            CatalogProductsWindow.myCart = bl.Cart.UpdateAmountOfProduct(CatalogProductsWindow.myCart, orderItem.ProductId, (orderItem.AmountInOrder)*(-1));
            MessageBox.Show("The Product removed from cart");
        }
        catch (BO.NotExiestsExceptions ex)
        {
            MessageBox.Show(ex.Message, "Failed to remove a product because it is not in the cart");
        }
    }
}
