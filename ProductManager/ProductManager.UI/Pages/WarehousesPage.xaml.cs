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
    private async void WarehousesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Перевіряємо, чи дійсно вибрано склад
        if (e.CurrentSelection.FirstOrDefault() is WarehouseViewModel selectedWarehouse)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "SelectedWarehouse", selectedWarehouse }
            };

            // Переходимо на сторінку деталей. Використовуємо відносний шлях.
            await Shell.Current.GoToAsync($"{nameof(WarehouseDetailsPage)}", navigationParameter);

            // Знімаємо виділення з картки
            WarehousesCollection.SelectedItem = null;
        }
    }
}
