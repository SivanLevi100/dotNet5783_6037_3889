using BlApi;
using BlImplementation;
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
/// Interaction logic for ProductListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    private IBl bl = new Bl();
    public ProductListWindow()
    {
        InitializeComponent();
        ProductListview.ItemsSource = bl.Product.GetProductList();
        CatgegorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }

    private void CatgegorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
       var listProducts = (BO.Category?)ProductListview.SelectedItem == BO.Category.Unavailable ? bl.Product.GetProductList()
            : bl.Product.GetProductList().Where(product => product?.Category == (BO.Category?)ProductListview.SelectedItem);
        ProductListview.ItemsSource= listProducts;
    }
}
