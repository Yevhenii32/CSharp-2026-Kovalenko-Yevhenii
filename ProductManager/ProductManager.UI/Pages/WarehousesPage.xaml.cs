using Microsoft.Maui.Controls;
using ProductManager.UI.ViewModels;

namespace ProductManager.UI.Pages;

public partial class WarehousesPage : ContentPage
{
    private readonly WarehousesViewModel _viewModel;

    public WarehousesPage(WarehousesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    // Запускаємо завантаження щоразу, коли сторінка з'являється на екрані
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.RefreshDataCommand.ExecuteAsync(null);
    }
}