
using Microsoft.Maui.Controls;
using ProductManager.UI.ViewModels;

namespace ProductManager.UI.Pages;

// Клас логіки для сторінки деталей складу.
public partial class WarehouseDetailsPage : ContentPage
{
    private readonly WarehouseDetailsViewModel _viewModel;

    // Ініціалізує сторінку та встановлює зв'язок з моделлю представлення.
    public WarehouseDetailsPage(WarehouseDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }
    // Використовується для автоматичного оновлення списку товарів.
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.RefreshDataCommand.ExecuteAsync(null);
    }
}