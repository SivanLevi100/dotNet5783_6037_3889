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

namespace PL.Order;

public partial class NewOrderWindow : Window
{
    private BlApi.IBl? bl = BlApi.Factory.Get();

    public NewOrderWindow()
    {
        InitializeComponent();
    }

    private void CatgegorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var listProducts = (BO.Category?)CatgegorySelector.SelectedItem == BO.Category.Unavailable ? bl?.Product.GetProductItemList()
        : bl?.Product.GetProductItemList().Where(product => product?.Category == (BO.Category?)CatgegorySelector.SelectedItem);
        ProductItemListView.ItemsSource = listProducts;

    }
}
