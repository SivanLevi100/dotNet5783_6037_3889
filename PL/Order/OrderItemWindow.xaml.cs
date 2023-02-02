using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace PL.Order;

/// <summary>
/// Interaction logic for OrderItemWindow.xaml
/// The back window containing all the functions for the Order Item window
/// </summary>
public partial class OrderItemWindow : Window
{
    private BlApi.IBl? bl = BlApi.Factory.Get();

    public static readonly DependencyProperty Order1Dependency = DependencyProperty.Register(nameof(Order1), typeof(BO.Order), typeof(Window));
    public BO.Order? Order1 { get => (BO.Order)GetValue(Order1Dependency); private set => SetValue(Order1Dependency, value); }


    public static readonly DependencyProperty idProductDependency = DependencyProperty.Register(nameof(idProduct), typeof(int), typeof(Window));
    public int idProduct { get => (int)GetValue(idProductDependency); private set => SetValue(idProductDependency, value); }

    public static readonly DependencyProperty AmountDependency = DependencyProperty.Register(nameof(Amount), typeof(int), typeof(Window));
    public int Amount { get => (int)GetValue(AmountDependency); private set => SetValue(AmountDependency, value); }

   // public int numbersProduct { get; set; } = 




    public OrderItemWindow()
    {
        InitializeComponent();
    }
    public OrderItemWindow(int idOrder)
    {
        Order1 = bl?.Order.GetOrderDetails(idOrder);//The order to which we will add a product
        InitializeComponent();
    }

    /// <summary>
    /// Function for the "add item to order" button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddItemForOrderButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            //Adding the product to the order and updating the order details
            Order1 = bl?.Order.AddItemForOrder(Order1, idProduct, Amount);
            Order1 = bl?.Order.GetOrderDetails(Order1.Id);
            MessageBox.Show("The Product added");

        }
        catch (BO.NotExiestsExceptions str)
        {
            MessageBox.Show(str.Message, "The Product is not exiests", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        //Closing the current window and opening the window of the order in question
        Close();
        new OrderWindow(Order1.Id).Show();
       
    }
}
