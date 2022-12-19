using BlApi;
using BlImplementation;
using BO;
using Microsoft.VisualBasic;
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

namespace PL.Product;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    private IBl bl = new Bl();
    public ProductWindow(int id = 0)
    {

        try
        {
            BO.Product product = id == 0 ? new() { Category = BO.Category.Unavailable } : bl.Product.GetProductDetailsManager(id);
            InitializeComponent();
        }
        catch (IncorrectDataExceptions str)
        {
            Close();
            MessageBox.Show(str.Message, "Failure getting entity", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        ComboBoxCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));

        //InitializeComponent();

    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        //ComboBoxCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));

        BO.Product product = new();
        product.Id = int.Parse(txtId.Text);
        product.Name = txtName.Text;
        product.Price = double.Parse(txtPrice.Text);
        product.InStock = int.Parse(txtInStock.Text);
        product.Category = (BO.Category)ComboBoxCategory.SelectedItem;
        

        try
        {
            bl.Product.Add((BO.Product)sender);

        }
        catch(BO.IncorrectDataExceptions str)
        {
            //Close();
            //MessageBox.Show(str.Message, "Failure getting entity", MessageBoxButton.OK, MessageBoxImage.Exclamation);

        }
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        bl.Product.Update((BO.Product)sender);
    }


    private void TextBox_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)//הכנסת רק מספרים לתיבת הטקסט
    {
        TextBox text = sender as TextBox;
        if (text == null) return;
        if (e == null) return;
        //allow get out of the text box
        if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
            return;
        //allow list of system keys (add other key here if you want to allow)
        if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
        e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
        || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
            return;
        char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
        //allow control system keys
        if (Char.IsControl(c)) return;
        //allow digits (without Shift or Alt)
        if (Char.IsDigit(c))
            if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                return; //let this key be written inside the textbox
                        //forbid letters and signs (#,$, %, ...)
        e.Handled = true; //ignore this key. mark event as handled, will not be routed to other
        return;
    }

}
