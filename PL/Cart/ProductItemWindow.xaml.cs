using BlApi;
using BO;
using PL.Product;
using System;
using System.Collections.Generic;
using System.Data.Common;
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

/// <summary>
/// The back window containing all the functions for the product details window
/// </summary>
public partial class ProductItemWindow : Window
{
    private BlApi.IBl? bl = BlApi.Factory.Get();

    public static readonly DependencyProperty ProductDependency = DependencyProperty.Register(nameof(ProductItem), typeof(BO.ProductItem), typeof(Window));
    public BO.ProductItem? ProductItem { get => (BO.ProductItem)GetValue(ProductDependency); private set => SetValue(ProductDependency, value); }



    public ProductItemWindow(int id = 0)
    {
        try
        {
            ProductItem = id == 0 ? new() : bl?.Product.GetProductDetailsBuyer(id,CatalogProductsWindow.myCart);
            InitializeComponent();

        }
        catch (BO.IncorrectDataExceptions ex)
        {
            MessageBox.Show(ex.Message, "Failure getting entity", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            Close();
            new CatalogProductsWindow().Show();

        }
    }

    /// <summary>
    /// function for the "add" button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddItemButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            //Adding the product to the basket
            CatalogProductsWindow.myCart = bl.Cart.AddProduct(CatalogProductsWindow.myCart, ProductItem.IdProduct);
            ProductItem = bl?.Product.GetProductDetailsBuyer(ProductItem.IdProduct, CatalogProductsWindow.myCart);
            MessageBox.Show("The Product added to cart");
        }
        catch (BO.NotExiestsExceptions ex) 
        {
            MessageBox.Show(ex.Message, "The product is out of stock");
           

        }
        //Closing the current window and opening the "Catalog" window
        Close();
        new CatalogProductsWindow().Show();
    }

    /// <summary>
    /// function for the "remove item" button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RemoveItemButton_Click(object sender, RoutedEventArgs e)
    {
        //Removing the product from the basket
        if (MessageBox.Show("Are you sure you want to remove the item from the cart?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
            try
            {
                //Updating the product quantity in the basket
                CatalogProductsWindow.myCart = bl.Cart.UpdateAmountOfProduct(CatalogProductsWindow.myCart, ProductItem.IdProduct, ProductItem.AmountInCart - 1);
                ProductItem = bl?.Product.GetProductDetailsBuyer(ProductItem.IdProduct, CatalogProductsWindow.myCart);
                MessageBox.Show("The Product removed from cart");

            }
            catch (BO.NotExiestsExceptions ex)
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
