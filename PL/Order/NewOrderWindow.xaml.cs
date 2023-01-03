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

namespace PL.Order;

public partial class NewOrderWindow : Window
{
    private BlApi.IBl? bl = BlApi.Factory.Get();
    //public BO.ProductItem ProductItem;

    //public static readonly DependencyProperty ProductsDependency = DependencyProperty.Register(nameof(ProductItems), typeof(ObservableCollection<ProductForList?>), typeof(Window));

    //public ObservableCollection<BO.ProductItem?> ProductItems
    //{
    //    get => (ObservableCollection<BO.ProductItem?>)GetValue(ProductsDependency);
    //    private set => SetValue(ProductsDependency, value);
    //}
    public NewOrderWindow()
    {
        InitializeComponent();
        ProductItemListView.ItemsSource = bl?.Product.GetProductItemList();
        CatgegorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));

        //var temp = bl?.Product?.GetProductItemList();
        //ProductItems = temp == null ? new() : new(temp);

        //ProductItem = new ProductItem() ;
        //DataContext = ProductItem;
    }

    private void CatgegorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var listProducts = (BO.Category?)CatgegorySelector.SelectedItem == BO.Category.Unavailable ? bl?.Product.GetProductItemList()
        : bl?.Product.GetProductItemList().Where(product => product?.Category == (BO.Category?)CatgegorySelector.SelectedItem);
        ProductItemListView.ItemsSource = listProducts;

    }
}
