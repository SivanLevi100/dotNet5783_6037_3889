using BO;
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
    private BlApi.IBl? bl = BlApi.Factory.Get();
    BackgroundWorker worker;
    bool isWork = false;
    DateTime nowTime = DateTime.Now;//save current time 
    bool inAddingProcess = false;

    public static readonly DependencyProperty OrderListDependency = DependencyProperty.Register(nameof(OrdertList), typeof(ObservableCollection<OrderForList?>), typeof(Window));
    public ObservableCollection<OrderForList?> OrdertList
    {
        get => (ObservableCollection<OrderForList?>)GetValue(OrderListDependency);
        private set => SetValue(OrderListDependency, value);
    }



    public SimulatorWindow()
    {
        InitializeComponent();

        worker = new() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };//create new worker
        worker.DoWork += Worker_DoWork;
        worker.ProgressChanged += Worker_ProgressChanged;
        worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

        worker.RunWorkerAsync();
        //stopWatch.Rstart();

    }
    private void Worker_DoWork(object sender, DoWorkEventArgs e) 
    {

    }

    private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        nowTime.AddHours(6);//add 6 hours to time
    }
    private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {

        //stopWatch.Stop();

    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        e.Cancel = true;
    }

    private void StopSimulatorButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    //protected override void OnClosing(CancelEventArgs e)
    //{
    //    e.Cancel=cancel
            
    //}
    private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        ListView listview = sender as ListView;
        BO.OrderForList order1 = new BO.OrderForList();
        order1 = listview.SelectedItem as BO.OrderForList;
        new OrderWindow(order1.OrderId).Show();
    }

    //////
}
