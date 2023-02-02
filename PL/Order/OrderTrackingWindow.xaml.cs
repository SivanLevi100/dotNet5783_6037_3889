using BO;
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
/// The back window containing all the functions for the Order Tracking window
/// </summary>
public partial class OrderTrackingWindow : Window
{
    private BlApi.IBl? bl = BlApi.Factory.Get();

    public static readonly DependencyProperty OrderTrackingDependency = DependencyProperty.Register(nameof(OrderTracking), typeof(BO.OrderTracking), typeof(Window));
    public BO.OrderTracking? OrderTracking { get => (BO.OrderTracking)GetValue(OrderTrackingDependency); private set => SetValue(OrderTrackingDependency, value); }


    public OrderTrackingWindow(int id)
    {
        try
        {
            OrderTracking = bl.Order.Tracking(id);
        }
        catch (BO.NotExiestsExceptions str)
        {
            MessageBox.Show(str.Message, "This order does not exist", MessageBoxButton.OK, MessageBoxImage.Exclamation);

        }
        InitializeComponent();
    }

    /// <summary>
    /// function for the "exit" button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Exit the page");
        Close();
    }
}
