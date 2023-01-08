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

    public static readonly DependencyProperty ProductItemDependency = DependencyProperty.Register(nameof(ProductItems), typeof(ObservableCollection<BO.ProductItem?>), typeof(Window));
    public ObservableCollection<BO.ProductItem?> ProductItems
    {
        get => (ObservableCollection<BO.ProductItem?>)GetValue(ProductItemDependency);
        private set => SetValue(ProductItemDependency, value);
    }
    public BO.Category Category1 { get; set; } = BO.Category.Unavailable;
    public Array Categories1 { get; set; } = Enum.GetValues(typeof(BO.Category));

    //public static readonly DependencyProperty CartDependency = DependencyProperty.Register(nameof(MyCart), typeof(BO.Cart), typeof(Window));
    //public BO.Cart MyCart { get => (BO.Cart)GetValue(CartDependency); private set => SetValue(CartDependency, value); }




    public CatalogProductsWindow()
    {
        InitializeComponent();
        var temp = bl?.Product?.GetProductItemList();
        ProductItems = temp == null ? new() : new(temp);

    }
    private void CatgegorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //var listProducts = (BO.Category?)CatgegorySelector.SelectedItem == BO.Category.Unavailable ? bl?.Product.GetProductItemList()
        //: bl?.Product.GetProductItemList().Where(product => product?.Category == (BO.Category?)CatgegorySelector.SelectedItem);
        //ProductItemListView.ItemsSource = listProducts;


        var temp = Category1 == BO.Category.Unavailable ?
        bl?.Product.GetProductItemList() : bl?.Product.GetProductItemList().Where(item => item.Category == Category1);
        ProductItems = temp == null ? new() : new(temp);


    }

    private void ProductItemListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        ListView listView= sender as ListView;
        BO.ProductItem productItem = new BO.ProductItem();
        productItem = listView.SelectedItem as BO.ProductItem;
        new ProductItemWindow(productItem.IdProduct).Show();
       // Close();
    }

    private void MyCartButton_Click(object sender, RoutedEventArgs e)
    {
        
        new MyCartWindow().Show();
    }
}
