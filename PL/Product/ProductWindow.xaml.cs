using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.Product;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    private BlApi.IBl? bl = BlApi.Factory.Get();

    public static readonly DependencyProperty ProductDependency = DependencyProperty.Register(nameof(Product), typeof(BO.Product), typeof(Window));
    public BO.Product? Product { get => (BO.Product)GetValue(ProductDependency); private set => SetValue(ProductDependency, value); }

    //public BO.Category Category { get; set; } = BO.Category.Unavailable;

    //public Array Categories1 { get { return Enum.GetValues(typeof(BO.Category)); } }
    // public Array Categories { get; set; } = Enum.GetValues(typeof(BO.Category));
   public Array Categories2 { get; set; } = Enum.GetValues(typeof(BO.Category));



    //Builder for the add product window
    public ProductWindow()
    {
        InitializeComponent();
        //ComboBoxCategory.ItemsSource = Enum.GetValues(typeof(BO.Category)); /////////////
        AddButton.Visibility = Visibility.Visible; ////////////////
        UpdateButton.Visibility = Visibility.Hidden; //////////////
        DeleteProduct.Visibility = Visibility.Hidden;
    }

    //Constructor for the product update window
    public ProductWindow(int id = 0)
    {

        try
        {
            Product = id == 0 ? new() { Category = BO.Category.Unavailable} : bl.Product.GetProductDetailsManager(id);
            InitializeComponent();
            //ComboBoxCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
            AddButton.Visibility = Visibility.Hidden;
            UpdateButton.Visibility = Visibility.Visible;
            DeleteProduct.Visibility = Visibility.Visible;
            txtId.IsEnabled = false;//This field cannot be changed

        }
        catch (BO.IncorrectDataExceptions ex)
        {
            MessageBox.Show(ex.Message, "Failure getting entity", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            Close();
            new ProductListWindow().Show();

        }

    }

    //A function that implements an Add button click event
    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        //try
        //{
        //    bl?.Product.Add(Product);
        //    MessageBox.Show("The Product added");
        //    Close();
        //    new ProductListWindow().Show();
        //}
        //catch (BO.IncorrectDataExceptions str)
        //{
        //    MessageBox.Show(str.Message, "Failure getting entity", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //    Close();
        //}

        while (string.IsNullOrEmpty(txtId.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(txtInStock.Text) || ComboBoxCategory.SelectedItem == null || txtPrice.Text == "0" || txtInStock.Text == "0" || txtId.Text == "000000000" || txtName.Text == "Name")
        {
            MessageBox.Show("Not all fields are filled");
            return;
        }
        BO.Product? product = new BO.Product();
        product.Id = int.Parse(txtId.Text);
        product.Name = txtName.Text;
        product.Price = double.Parse(txtPrice.Text);
        product.InStock = int.Parse(txtInStock.Text);
        product.Category = (BO.Category)ComboBoxCategory.SelectedItem;
        try
        {
            bl?.Product.Add(product);
        }
        catch (BO.IncorrectDataExceptions str)
        {
            MessageBox.Show(str.Message, "Failure getting entity", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            Close();
            //new ProductListWindow().Show();
            //return;
        }
        MessageBox.Show("The Product added");
        Close();
        new ProductListWindow().Show();

    }

    //A function that implements an Update button click event
    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {

        if((BO.Category?)ComboBoxCategory.SelectedItem == BO.Category.Unavailable)
        {
            MessageBox.Show("Not all fields are filled");
        }
        BO.Product? product = new BO.Product();
        product.Id = int.Parse(txtId.Text);
        product.Name = txtName.Text;
        product.Price = double.Parse(txtPrice.Text);
        product.InStock = int.Parse(txtInStock.Text);
        product.Category = (BO.Category)ComboBoxCategory.SelectedItem;

        try
        {
            bl?.Product.Update(product);
        }
        catch(BO.IncorrectDataExceptions str) 
        {
            MessageBox.Show(str.Message, "Failure getting entity", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            Close();
            return;
        }
        MessageBox.Show("The Product updated","updated", MessageBoxButton.OK);
        Close();
        new ProductListWindow().Show();

    }

    private void DeleteProduct_Click(object sender, RoutedEventArgs e)
    {
        BO.Product? product = new BO.Product();
        product.Id = int.Parse(txtId.Text);
        //product.Name = txtName.Text;
        //product.Price = double.Parse(txtPrice.Text);
        //product.InStock = int.Parse(txtInStock.Text);
        //product.Category = (BO.Category)ComboBoxCategory.SelectedItem;
        if (MessageBox.Show("Are you sure you want to remove the product from the order?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
            try
            {
                bl.Product.Delete(product.Id);
            }
            catch (BO.NotExiestsExceptions str)
            {
                MessageBox.Show(str.Message, "Failure getting entity", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Close();
                return;
            }

        }
        else
        {
            MessageBox.Show("The item has not been removed from the cart", "Item not removed");

        }
        Close();
        MessageBox.Show("The product has been deleted");
        new ProductListWindow().Show();


    }



    //Function to check input only numbers
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
