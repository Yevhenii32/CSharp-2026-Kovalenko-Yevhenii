using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using ProductManager.DTOModels.Product;
using ProductManager.DTOModels.Warehouse;
using ProductManager.Services;
using ProductManager.UI.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ProductManager.UI.ViewModels
{
    // Відображає інформацію про склад та керує списком товарів у ньому.
    public partial class WarehouseDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IWarehouseService _warehouseService;
        private readonly IProductService _productService;

        private Guid _warehouseId;

        // Детальна інформація про поточний склад.
        [ObservableProperty]
        private WarehouseDetailsDTO _currentWarehouse;

        // Колекція товарів, що належать до цього складу.
        [ObservableProperty]
        private ObservableCollection<ProductListDTO> _products;

        public WarehouseDetailsViewModel(IWarehouseService warehouseService, IProductService productService)
        {
            _warehouseService = warehouseService;
            _productService = productService;
        }

        // Приймає параметри навігації при переході на сторінку.
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("WarehouseId", out var idValue) && idValue is Guid id)
            {
                _warehouseId = id;
                _ = RefreshData();
            }
        }

        // Оновлення даних про склад та список його товарів з бази даних.
        [RelayCommand]
        internal async Task RefreshData()
        {
            IsBusy = true;
            try
            {
                CurrentWarehouse = await _warehouseService.GetWarehouseAsync(_warehouseId) ?? throw new Exception("Склад не знайдено.");
                Products = new ObservableCollection<ProductListDTO>(await _productService.GetProductsByWarehouseAsync(_warehouseId));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Помилка", $"Помилка завантаження деталей: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Виконує навігацію до сторінки деталей конкретного товару.
        [RelayCommand]
        private async Task GotoProductDetails(ProductListDTO product)
        {
            if (product == null) return;
            IsBusy = true;
            try
            {
                await Shell.Current.GoToAsync($"{nameof(ProductDetailsPage)}", new Dictionary<string, object> { { "ProductId", product.Id } });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Помилка", $"Помилка навігації: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Додає новий товар до поточного складу.
        [RelayCommand]
        private async Task AddProduct()
        {
            try
            {
                string name = await Application.Current.MainPage.DisplayPromptAsync("Новий товар", "Назва товару:");
                if (!string.IsNullOrWhiteSpace(name))
                {
                    await _productService.CreateProductAsync(_warehouseId, name, 1, 100m, ProductManager.DBModels.ProductCategory.Electronics, "Без опису");
                    await RefreshData();
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Помилка", $"Помилка створення: {ex.Message}", "OK");
            }
        }

        // Видаляє товар зі складу після підтвердження користувача.
        [RelayCommand]
        private async Task DeleteProduct(ProductListDTO product)
        {
            if (product == null) return;
            try
            {
                if (await Shell.Current.DisplayAlert("Підтвердження", $"Видалити {product.Name}?", "Так", "Ні"))
                {
                    await _productService.DeleteProductAsync(product.Id);
                    Products.Remove(product);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Помилка", $"Помилка видалення: {ex.Message}", "OK");
            }
        }
    }
}