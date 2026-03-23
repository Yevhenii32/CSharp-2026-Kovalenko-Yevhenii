using Microsoft.Maui.Controls;
using ProductManager.UI.ViewModels;

namespace ProductManager.UI.Pages;

public partial class WarehousesPage : ContentPage
{
    public WarehousesPage(WarehousesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}