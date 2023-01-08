﻿using BO;
using DO;
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
/// Interaction logic for OrderItemWindow.xaml
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



    public OrderItemWindow()
    {
        InitializeComponent();
    }
    public OrderItemWindow(int idOrder)
    {
        Order1 = bl.Order.GetOrderDetails(idOrder);//ההזממה שאליה נוסיף מוצר
        InitializeComponent();
    }

    private void AddItemForOrderButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Order1 = bl.Order.AddItemForOrder(Order1, idProduct, Amount);

        }
        catch (BO.NotExiestsExceptions str)
        {
            MessageBox.Show(str.Message, "The Product is not exiests", MessageBoxButton.OK, MessageBoxImage.Exclamation);

        }
    }
}
