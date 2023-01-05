using PL.Order;
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

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for MyCartWindow.xaml
    /// </summary>
    public partial class MyCartWindow : Window
    {
        public MyCartWindow()
        {
            InitializeComponent();
        }

        private void OrderConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            new OrderConfirmationWindow().Show();
        }
    }
}
