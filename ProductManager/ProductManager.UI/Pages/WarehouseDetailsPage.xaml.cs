using Microsoft.Maui.Controls;
using ProductManager.UI.ViewModels;

namespace ProductManager.UI.Pages;

public partial class WarehouseDetailsPage : ContentPage
{
    public WarehouseDetailsPage(WarehouseDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}