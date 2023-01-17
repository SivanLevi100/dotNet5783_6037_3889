﻿using BlApi;
using BO;
using PL.Cart;
using PL.Product;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
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

    //לרשימה של פריטים בהזמנה
    public static readonly DependencyProperty OrderItemListDependency = DependencyProperty.Register(nameof(OrderItemList), typeof(ObservableCollection<BO.OrderItem?>), typeof(Window));
    public ObservableCollection<BO.OrderItem?> OrderItemList
    {
        get => (ObservableCollection<BO.OrderItem?>)GetValue(OrderItemListDependency);
        private set => SetValue(OrderItemListDependency, value);
    }

    public OrderWindow(int id=0)
    {
        try
        {
            Order = id == 0 ? new() { Status=BO.OrderStatus.Unknown} : bl?.Order.GetOrderDetails(id);
            InitializeComponent();
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
        
        new OrderItemWindow(Order.Id).Show();
        Close();

        //Order = bl.Order.GetOrderDetails(Order.Id);


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
            //Close();
            //new OrderListWindow().Show();
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
            //Close();
            //new OrderListWindow().Show();
        }


    }

    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //DataGrid DataGrid = sender as DataGrid;
        //BO.OrderItem OrderItem = new BO.OrderItem();
        //OrderItem = DataGrid.SelectedItem as BO.OrderItem;

        //new OrderItemWindow(OrderItem.ProductId, Order.Id).Show();

        new OrderItemWindow(Order.Id).Show();
        Close();////////////
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        new OrderListWindow().Show();
        Close();
    }

    private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        ListView listView = sender as ListView;
        BO.OrderItem orderItem = new BO.OrderItem();
        orderItem = listView.SelectedItem as BO.OrderItem;
        new OrderItemWindow(orderItem.ProductId).Show();

        //Order = bl?.Order.GetOrderDetails(Order.Id);

        Close();
    }
}
