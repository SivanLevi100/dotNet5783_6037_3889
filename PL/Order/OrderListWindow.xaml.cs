﻿using BO;
using PL.Product;
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

namespace PL.Order;

/// <summary>
/// Interaction logic for OrderListWindow.xaml
/// </summary>
public partial class OrderListWindow : Window
{
    private BlApi.IBl? bl = BlApi.Factory.Get();

    public static readonly DependencyProperty OrderListDependency = DependencyProperty.Register(nameof(OrdertList), typeof(ObservableCollection<OrderForList?>), typeof(Window));
    public ObservableCollection<OrderForList?> OrdertList
    {
        get => (ObservableCollection<OrderForList?>)GetValue(OrderListDependency);
        private set => SetValue(OrderListDependency, value);
    }
    public BO.OrderStatus OrderStatus { get; set; } = BO.OrderStatus.Unknown;
    public Array AllStatus { get; set; } = Enum.GetValues(typeof(BO.OrderStatus));


    public OrderListWindow()
    {
        InitializeComponent();
        var temp = bl?.Order.GetOrderList();
        OrdertList = temp == null ? new() : new(temp);

    }

    private void OrdertListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        ListView listview = sender as ListView;
        BO.OrderForList order1 = new BO.OrderForList();
        order1 = listview.SelectedItem as BO.OrderForList;
        new OrderWindow(order1.OrderId).Show();
        Close();

        //ListBox listBox = sender as ListBox;
        //BO.OrderForList order = new BO.OrderForList();
        //order = listBox.SelectedItem as BO.OrderForList;
        //new OrderWindow(order.OrderId).Show();
        //Close();
    }

    private void OrderDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //DataGrid listBox = sender as DataGrid;
        //BO.OrderForList order = new BO.OrderForList();
        //order = DataGrid as BO.OrderForList;
        //new OrderWindow(order.OrderId).Show();
        //Close();
    }

    //סינון לפי סטטוס הזמנה ComboBox
    private void orderStatusSelected_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var temp = OrderStatus == BO.OrderStatus.Unknown ? bl?.Order.GetOrderList() 
            : bl?.Order.GetOrderList().Where(item => item.Status == OrderStatus);
        OrdertList = temp == null ? new() : new(temp);

    }
}
