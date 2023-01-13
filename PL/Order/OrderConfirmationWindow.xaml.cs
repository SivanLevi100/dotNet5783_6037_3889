using BO;
using PL.Cart;
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

/// <summary>
/// Interaction logic for OrderConfirmationWindow.xaml
/// </summary>
public partial class OrderConfirmationWindow : Window
{
    private BlApi.IBl? bl = BlApi.Factory.Get();

    public static readonly DependencyProperty Cart1Dependency = DependencyProperty.Register(nameof(MyCart1), typeof(BO.Cart), typeof(Window));
    public BO.Cart MyCart1 { get => (BO.Cart)GetValue(Cart1Dependency); private set => SetValue(Cart1Dependency, value); }


    public static readonly DependencyProperty CustomerNameDependency = DependencyProperty.Register(nameof(CustomerName), typeof(string), typeof(Window));
    public string CustomerName { get => (string)GetValue(CustomerNameDependency); private set => SetValue(CustomerNameDependency, value); }

    public static readonly DependencyProperty CustomerEmailDependency = DependencyProperty.Register(nameof(CustomerEmail), typeof(string), typeof(Window));
    public string CustomerEmail { get => (string)GetValue(CustomerEmailDependency); private set => SetValue(CustomerEmailDependency, value); }
    
    public static readonly DependencyProperty CustomerAdressDependency = DependencyProperty.Register(nameof(CustomerAdress), typeof(string), typeof(Window));
    public string CustomerAdress { get => (string)GetValue(CustomerAdressDependency); private set => SetValue(CustomerAdressDependency, value); }



    public OrderConfirmationWindow()
    {
        InitializeComponent();
    }

    private void OrderConfirmationButton_Click(object sender, RoutedEventArgs e)
    {

        try
        {
            CatalogProductsWindow.myCart.CustomerName = CustomerName;
            CatalogProductsWindow.myCart.CustomerEmail = CustomerEmail;
            CatalogProductsWindow.myCart.CustomerAdress = CustomerAdress;

            bl?.Cart.Confirm(CatalogProductsWindow.myCart);
            CatalogProductsWindow.myCart = null;
            CatalogProductsWindow.myCart=new() { OrdersItemsList = new List<BO.OrderItem?>() };
            //Close();
        }
        catch (BO.IncorrectDataExceptions str) 
        {
            MessageBox.Show(str.Message, "Buyer's name or address are blank or Email address in invalid format", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            return;
        }
        catch (BO.NotExiestsExceptions str)
        {
            MessageBox.Show(str.Message, "The shopping cart is empty", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            return;
        }
        MessageBox.Show("The order was successfully placed");

    }
}
