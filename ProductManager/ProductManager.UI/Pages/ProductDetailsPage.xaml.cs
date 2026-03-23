using Microsoft.Maui.Controls;
using ProductManager.UI.ViewModels;

namespace ProductManager.UI.Pages;

public partial class ProductDetailsPage : ContentPage
{
    public ProductDetailsPage(ProductDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}