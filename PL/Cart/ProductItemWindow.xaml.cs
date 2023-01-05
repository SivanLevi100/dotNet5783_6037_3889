using PL.Product;
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

namespace PL.Cart;
public partial class ProductItemWindow : Window
{
    private BlApi.IBl? bl = BlApi.Factory.Get();

    public static readonly DependencyProperty ProductDependency = DependencyProperty.Register(nameof(ProductItem), typeof(BO.ProductItem), typeof(Window));
    public BO.ProductItem? ProductItem { get => (BO.ProductItem)GetValue(ProductDependency); private set => SetValue(ProductDependency, value); }

    public static readonly DependencyProperty CartDependency = DependencyProperty.Register(nameof(MyCart), typeof(BO.Cart), typeof(Window));
    public BO.Cart MyCart { get => (BO.Cart)GetValue(CartDependency); private set => SetValue(CartDependency, value); }

    public ProductItemWindow(int id=0)
    {

        //InitializeComponent();
        try
        {
            ProductItem = id == 0 ? new() : bl?.Product.GetProductDetailsBuyer(id,MyCart);
            InitializeComponent();

        }
        catch (BO.IncorrectDataExceptions ex)
        {
            MessageBox.Show(ex.Message, "Failure getting entity", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            Close();
            new CatalogProductsWindow().Show();

        }
    }

    private void AddItemButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            MyCart = bl?.Cart.AddProduct(MyCart, ProductItem.IdProduct);
            MessageBox.Show("The Product added to cart");

        }
        catch (BO.NotExiestsExceptions ex) 
        {
            MessageBox.Show(ex.Message, "The product is out of stock");
            Close();

        }
    }

    private void RemoveItemButton_Click(object sender, RoutedEventArgs e)
    {
        if (MessageBox.Show("Are you sure you want to remove the item from the cart?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
            try
            {
                if(ProductItem?.AmountInCart==0)
                {
                    MessageBox.Show("The item cannot be removed");
                }
                else 
                {
                    MyCart = bl.Cart.UpdateAmountOfProduct(MyCart, ProductItem.IdProduct, ProductItem.AmountInCart - 1);
                    MessageBox.Show("The Product removed from cart");
                }
            }
            catch (BO.IncorrectDataExceptions ex)
            {
                MessageBox.Show(ex.Message, "Failed to remove a product because it is not in the cart");
            }
        }
        else
        {
            MessageBox.Show("The item has not been removed from the cart", "Item not removed");
        }
        Close();

    }
}
