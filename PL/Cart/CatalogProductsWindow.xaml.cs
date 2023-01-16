using BO;
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

namespace PL.Cart;

public partial class CatalogProductsWindow : Window
{
    private BlApi.IBl? bl = BlApi.Factory.Get();

    public static BO.Cart myCart = new() { OrdersItemsList=new List<BO.OrderItem?>()};

    public static readonly DependencyProperty ProductItemDependency = DependencyProperty.Register(nameof(ProductItems), typeof(ObservableCollection<BO.ProductItem?>), typeof(Window));
    public ObservableCollection<BO.ProductItem?> ProductItems
    {
        get => (ObservableCollection<BO.ProductItem?>)GetValue(ProductItemDependency);
        private set => SetValue(ProductItemDependency, value);
    }
    public BO.Category Category1 { get; set; } = BO.Category.Unavailable;
    public Array Categories1 { get; set; } = Enum.GetValues(typeof(BO.Category));

    //public static readonly DependencyProperty ProductItem1Dependency = DependencyProperty.Register(nameof(ProductItem), typeof(BO.ProductItem), typeof(Window));
    //public BO.ProductItem ProductItem { get => (BO.ProductItem)GetValue(ProductItem1Dependency); private set => SetValue(ProductItem1Dependency, value); }




    public CatalogProductsWindow()
    {
        InitializeComponent();
        var temp = bl?.Product.GetProductItemList(myCart);
        ProductItems = temp == null ? new() : new(temp);

        
    }
    private void CatgegorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var temp = Category1 == BO.Category.Unavailable ?
        bl?.Product.GetProductItemList(myCart) : bl?.Product.GetProductItemList(myCart).Where(item => item.Category == Category1);
        ProductItems = temp == null ? new() : new(temp);

    }

    private void ProductItemListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        ListView listView= sender as ListView;
        BO.ProductItem productItem = new BO.ProductItem();
        productItem = listView.SelectedItem as BO.ProductItem;
        new ProductItemWindow(productItem.IdProduct).Show();

        var temp = bl?.Product?.GetProductItemList(myCart);
        ProductItems = temp == null ? new() : new(temp);

        Close();
    }

    private void MyCartButton_Click(object sender, RoutedEventArgs e)
    {
        
        new MyCartWindow().Show();
        Close();
    }

    private void AddItemButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            ProductItem productItem = (ProductItem)((sender as Button)!.DataContext!);
            myCart = bl.Cart.AddProduct(myCart, productItem.IdProduct);
            var temp = bl.Product.GetProductItemList(myCart);
            ProductItems = temp == null ? new() : new(temp);
            MessageBox.Show("The Product added to cart");
        }
        catch (BO.NotExiestsExceptions ex)
        {
            MessageBox.Show(ex.Message, "The product is out of stock");
        }
    }

    private void GroupingButton_Click(object sender, RoutedEventArgs e)
    {

    }
}
