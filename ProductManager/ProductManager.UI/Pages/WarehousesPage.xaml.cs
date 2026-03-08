using ProductManager.Services;
using ProductManager.ViewModels;
using System.Linq;

namespace ProductManager.UI.Pages;

public partial class WarehousesPage : ContentPage
{
    private readonly IStorageService _storageService;

    public WarehousesPage(IStorageService storageService)
    {
        InitializeComponent();
        _storageService = storageService;
    }
    // Оновлюємо дані щоразу, коли сторінка з'являється на екрані
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadWarehouses();
    }

    private void LoadWarehouses()
    {
        // Отримуємо сирі дані з сервісу
        var warehouses = _storageService.GetAllWarehouses();
        // Перетворюємо моделі даних у ViewModels для відображення в UI
        var viewModels = warehouses.Select(w =>
        {
            var products = _storageService.GetProductsByWarehouseId(w.Id);
            var productVMs = products.Select(p => new ProductViewModel(p)).ToList();
            return new WarehouseViewModel(w, productVMs);
        }).ToList();

        WarehousesCollection.ItemsSource = viewModels;
    }
}