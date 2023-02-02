using BO;
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

namespace PL.Product;

/// <summary>
/// Interaction logic for ProductListWindow.xaml
/// The back window containing all the functions for the Product List window
/// </summary>
public partial class ProductListWindow : Window
{
    private  BlApi.IBl? bl = BlApi.Factory.Get();

    public static readonly DependencyProperty ProductListDependency = DependencyProperty.Register(nameof(ProductList), typeof(ObservableCollection<ProductForList?>), typeof(Window));
    public ObservableCollection<ProductForList?> ProductList
    {
        get => (ObservableCollection<ProductForList?>)GetValue(ProductListDependency);
        private set => SetValue(ProductListDependency, value);
    }
    public BO.Category Category { get; set; } = Category.Unavailable;
    public Array Categories { get; set; } = Enum.GetValues(typeof(BO.Category));


    //constructor
    public ProductListWindow()
    {
        InitializeComponent();
        var temp = bl?.Product?.GetProductList();
        ProductList = temp == null ? new() : new(temp);

        //InitializeComponent();
        //ProductListview.ItemsSource = bl?.Product.GetProductList();
        //CatgegorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }

    //A function that implements filtering a list of products by category
    private void CatgegorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var temp = Category == BO.Category.Unavailable ?
            bl?.Product.GetProductList() : bl?.Product.GetProductList().Where(item => item.Category == Category);
        ProductList = temp == null ? new() : new(temp);

        //var listProducts = (BO.Category?)CatgegorySelector.SelectedItem == BO.Category.Unavailable ? bl?.Product.GetProductList()
        // : bl?.Product.GetProductList().Where(product => product?.Category == (BO.Category?)CatgegorySelector.SelectedItem);
        //ProductListview.ItemsSource = listProducts;
    }

    //A function that implements a click on the "Add New Product" button
    private void ButtonAddNewProduct_Click(object sender, RoutedEventArgs e)
    {
        new ProductWindow().Show();
        //var temp = bl?.Product?.GetProductList(); //////////////
        //ProductList = temp == null ? new() : new(temp);

        Close();
    }

    //A function that implements a double click on an item in a product list
    private void ListView_DoubleClick(object sender, MouseButtonEventArgs e)
    {
        ListBox listBox= sender as ListBox;
        BO.ProductForList product = new BO.ProductForList();
        product = listBox.SelectedItem as BO.ProductForList;
        new ProductWindow(product.IdProduct).Show();
        Close();
    }

}
