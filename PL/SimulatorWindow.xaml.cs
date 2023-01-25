using BO;
using PL.Cart;
using PL.Order;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace PL;

/// <summary>
/// Interaction logic for SimulatorWindow.xaml
/// </summary>
public partial class SimulatorWindow : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();
    ObservableCollection<OrderForList?> ordersForList = new();
    IEnumerable<BO.Order> orders;
    BackgroundWorker worker;
    bool isWork = false;
    DateTime nowTime = DateTime.Now;
    bool inAddingProcess = false;
    public SimulatorWindow()
    {
        InitializeComponent();
        orders=bl!.Order.GetOrderList().Select(x => bl.Order.GetOrderDetails((int)x?.OrderId!));
        try
        {
           ordersForList = Castings.convertIenumerableToObservable( bl.Order.GetOrderList());
        }
        catch (BO.NotExiestsExceptions ex)
        {
            MessageBox.Show(ex.Message);
        }
        DataContext = ordersForList;

        worker = new() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
        worker.DoWork += Worker_DoWork;
        worker.ProgressChanged += Worker_ProgressChanged;
        worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

    }
    private void Worker_DoWork(object sender, DoWorkEventArgs e)
    {
        var Worker = sender as BackgroundWorker;
        OrderForList? myOFL = new();
        foreach (BO.Order? Item in orders)
        {
            orders = bl!.Order.GetOrderList().Select(x => bl.Order.GetOrderDetails((int)x?.OrderId!)).OrderBy(x => x.OrderDate);
            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                break;
            }
            switch (Item.Status)
            {
                case BO.OrderStatus.Confirmed:
                    if (Item.OrderDate?.AddDays(15) >= nowTime)
                    {
                        bl.Order.UpdateShipping(Item.Id);
                        System.Threading.Thread.Sleep(400);
                    }
                    break;

                case BO.OrderStatus.shipped:
                    if (Item.ShipDate?.AddDays(12) >= nowTime)
                    {
                        bl.Order.UpdateDelivery(Item.Id);
                        System.Threading.Thread.Sleep(400);
                    }
                    break;
            }

            System.Threading.Thread.Sleep(2000);
        }

    }
    private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        nowTime.AddHours(3);
    }
    private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        StartTracking.IsEnabled = true;
        StopTracking.IsEnabled = true;
        if (!inAddingProcess)
            MessageBox.Show("Simulator stopped");
    }
    private void StartTracking_Click(object sender, RoutedEventArgs e)
    {
        if (isWork == false)
        {
            isWork = true;
            StartTracking.IsEnabled = false;
            StopTracking.IsEnabled = true;
            worker.RunWorkerAsync("Test");
        }
    }
    private void StopTracking_Click(object sender, RoutedEventArgs e)
    {
        if (isWork == true)
        {
            isWork = false;
            StartTracking.IsEnabled = true;
            worker.CancelAsync();
        }
    }


    private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (listViewOrders.SelectedItem is BO.OrderForList orderForList)
        {
            new OrderWindow(orderForList.OrderId).ShowDialog();
            ordersForList = Castings.convertIenumerableToObservable(bl.Order.GetOrderList());
            DataContext = ordersForList;
        }

        //ListView listview = sender as ListView;
        //BO.OrderForList order1 = new BO.OrderForList();
        //order1 = listview.SelectedItem as BO.OrderForList;
        //new OrderWindow(order1.OrderId).Show();
    }


    private void cart_Click(object sender, RoutedEventArgs e)
    {
        new CatalogProductsWindow().ShowDialog();
        ordersForList = Castings.convertIenumerableToObservable(bl.Order.GetOrderList());
        DataContext = ordersForList;
    }

  
}








public class Castings
{
    public static ObservableCollection<BO.OrderForList?> convertIenumerableToObservable(IEnumerable<BO.OrderForList?> list)
    {
        ObservableCollection<BO.OrderForList?> lists = new ObservableCollection<BO.OrderForList?>();
        foreach (BO.OrderForList? item in list)
        {
            lists.Add(item);
        }
        return lists;
    }

    public static ObservableCollection<BO.ProductForList?> convertIenumerableToObservable(IEnumerable<BO.ProductForList?> list)
    {
        ObservableCollection<BO.ProductForList?> lists = new ObservableCollection<BO.ProductForList?>();
        foreach (BO.ProductForList? item in list)
        {
            lists.Add(item);
        }
        return lists;
    }
    public static ObservableCollection<BO.OrderItem> convertListToObservable(List<BO.OrderItem> list, double discont = 1)
    {
        ObservableCollection<BO.OrderItem> lists = new ObservableCollection<BO.OrderItem>();
        foreach (BO.OrderItem item in list)
        {
            item.Price = discont == 1 ? item.Price : item.Price - item.Price * discont;
            item.TotalPriceOfItem = item.Price * item.AmountInOrder;
            lists.Add(item);
        }
        return lists;
    }
}









//private BlApi.IBl? bl = BlApi.Factory.Get();
//BackgroundWorker worker;
//bool isWork = false;
//DateTime nowTime = DateTime.Now;//save current time 
//bool inAddingProcess = false;

//public static readonly DependencyProperty OrderListDependency = DependencyProperty.Register(nameof(OrdertList), typeof(ObservableCollection<OrderForList?>), typeof(Window));
//public ObservableCollection<OrderForList?> OrdertList
//{
//    get => (ObservableCollection<OrderForList?>)GetValue(OrderListDependency);
//    private set => SetValue(OrderListDependency, value);
//}



//public SimulatorWindow()
//{
//    InitializeComponent();

//    worker = new() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };//create new worker
//    worker.DoWork += Worker_DoWork;
//    worker.ProgressChanged += Worker_ProgressChanged;
//    worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

//    worker.RunWorkerAsync();
//    //stopWatch.Rstart();

//}
//private void Worker_DoWork(object sender, DoWorkEventArgs e) 
//{

//}

//private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
//{
//    nowTime.AddHours(6);//add 6 hours to time
//}
//private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
//{

//    //stopWatch.Stop();

//}


//xaml -   Closing="Window_Closing"


//private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
//{
//    e.Cancel = true;
//}

//private void StopSimulatorButton_Click(object sender, RoutedEventArgs e)
//{
//    Close();
//}

////protected override void OnClosing(CancelEventArgs e)
////{
////    e.Cancel=cancel

////}
//private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
//{
//    ListView listview = sender as ListView;
//    BO.OrderForList order1 = new BO.OrderForList();
//    order1 = listview.SelectedItem as BO.OrderForList;
//    new OrderWindow(order1.OrderId).Show();
//}
//////

