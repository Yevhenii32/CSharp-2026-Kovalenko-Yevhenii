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
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.UI.ViewModels
{
    // Відображає інформацію про склад та керує списком товарів у ньому.
    public partial class WarehouseDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IWarehouseService _warehouseService;
        private readonly IProductService _productService;
        private Guid _warehouseId;

        // Зберігаємо всі товари для фільтрації
        private List<ProductListDTO> _allProducts = new();

        [ObservableProperty]
        private WarehouseDetailsDTO _currentWarehouse;

        [ObservableProperty]
        private ObservableCollection<ProductListDTO> _products = new();

        [ObservableProperty]
        private string _searchText = string.Empty;

        [ObservableProperty]
        private string _selectedSortOption = "За назвою";

        // Список доступних опцій сортування.
        public List<string> SortOptions { get; } = new() { "За назвою", "Спочатку дешевші", "Спочатку дорожчі" };

        // Автоматичне оновлення фільтрації при зміні параметрів пошуку чи сортування.
        partial void OnSearchTextChanged(string value) => ApplyFilters();
        partial void OnSelectedSortOptionChanged(string value) => ApplyFilters();

        public WarehouseDetailsViewModel(IWarehouseService warehouseService, IProductService productService)
        {
            _warehouseService = warehouseService;
            _productService = productService;
        }

        // Приймає параметри навігації при переході на сторінку.
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("WarehouseId", out var idValue) && idValue is Guid id) _warehouseId = id;
        }

        // Асинхронно оновлює дані про склад та список його товарів з бази даних.
        [RelayCommand]
        internal async Task RefreshData()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                CurrentWarehouse = await _warehouseService.GetWarehouseAsync(_warehouseId) ?? throw new Exception("Склад не знайдено.");
                _allProducts.Clear();
                var productsList = await _productService.GetProductsByWarehouseAsync(_warehouseId);
                foreach (var p in productsList) _allProducts.Add(p);
                ApplyFilters();
            }
            catch (Exception ex) { await Shell.Current.DisplayAlert("Помилка", ex.Message, "OK"); }
            finally { IsBusy = false; }
        }

        // Застосовує логіку фільтрації та сортування до локального списку товарів.
        private void ApplyFilters()
        {
            var filtered = _allProducts.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(SearchText))
                filtered = filtered.Where(p => p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            filtered = SelectedSortOption switch
            {
                "Спочатку дешевші" => filtered.OrderBy(p => p.Price),
                "Спочатку дорожчі" => filtered.OrderByDescending(p => p.Price),
                _ => filtered.OrderBy(p => p.Name)
            };

            Products.Clear();
            foreach (var p in filtered) Products.Add(p);
        }

        // Перехід на сторінку детальної інформації конкретного товару.
        [RelayCommand]
        private async Task GotoProductDetails(ProductListDTO product)
        {
            if (product == null) return;
            IsBusy = true;
            try
            {
                await Shell.Current.GoToAsync($"{nameof(ProductDetailsPage)}", new Dictionary<string, object> { { "ProductId", product.Id } });
            }
            catch (Exception ex) { await Shell.Current.DisplayAlert("Помилка", ex.Message, "OK"); }
            finally { IsBusy = false; }
        }

        // Створює новий товар з валідацією вводу через діалогові вікна.
        [RelayCommand]
        private async Task AddProduct()
        {
            try
            {
                string name = await Application.Current.MainPage.DisplayPromptAsync("Новий товар", "Введіть назву (обов'язково):");
                if (name == null) return;
                if (string.IsNullOrWhiteSpace(name))
                {
                    await Shell.Current.DisplayAlert("Помилка", "Назва товару є обов'язковою!", "OK");
                    return;
                }

                string priceStr = await Application.Current.MainPage.DisplayPromptAsync("Ціна", "Введіть ціну (або залиште порожнім для 0):", keyboard: Keyboard.Numeric);
                if (priceStr == null) return;

                decimal price = 0;
                if (!string.IsNullOrWhiteSpace(priceStr))
                {
                    if (!decimal.TryParse(priceStr, out price) || price < 0)
                    {
                        await Shell.Current.DisplayAlert("Помилка", "Ціна має бути додатнім числом!", "OK");
                        return;
                    }
                }

                string qtyStr = await Application.Current.MainPage.DisplayPromptAsync("Кількість", "Введіть кількість (або залиште порожнім для 0):", keyboard: Keyboard.Numeric);
                if (qtyStr == null) return;

                int quantity = 0;
                if (!string.IsNullOrWhiteSpace(qtyStr))
                {
                    if (!int.TryParse(qtyStr, out quantity) || quantity < 0)
                    {
                        await Shell.Current.DisplayAlert("Помилка", "Кількість має бути додатнім цілим числом!", "OK");
                        return;
                    }
                }

                string description = await Application.Current.MainPage.DisplayPromptAsync("Опис", "Введіть опис (необов'язково):");
                if (description == null) return;
                if (string.IsNullOrWhiteSpace(description)) description = "Опис відсутній";

                string categoryStr = await Application.Current.MainPage.DisplayActionSheet("Оберіть категорію", "Скасувати", null, "Електроніка", "Меблі", "Продукти");
                if (categoryStr == "Скасувати" || categoryStr == null) return;

                var category = categoryStr switch
                {
                    "Меблі" => ProductManager.DBModels.ProductCategory.Furniture,
                    "Продукти" => ProductManager.DBModels.ProductCategory.Groceries,
                    _ => ProductManager.DBModels.ProductCategory.Electronics
                };

                await _productService.CreateProductAsync(_warehouseId, name, quantity, price, category, description);
                await RefreshData();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Помилка", $"Помилка при створенні: {ex.Message}", "OK");
            }
        }

        // Редагування існуючого товару.
        [RelayCommand]
        private async Task EditProduct(ProductListDTO product)
        {
            if (product == null) return;
            try
            {
                var details = await _productService.GetProductAsync(product.Id);
                if (details == null) return;

                string newName = await Application.Current.MainPage.DisplayPromptAsync("Редагування", "Назва товару:", initialValue: details.Name);
                if (string.IsNullOrWhiteSpace(newName)) return;

                string priceStr = await Application.Current.MainPage.DisplayPromptAsync("Ціна", "Ціна:", initialValue: details.Price.ToString(), keyboard: Keyboard.Numeric);
                if (!decimal.TryParse(priceStr, out decimal newPrice) || newPrice < 0) return;

                string qtyStr = await Application.Current.MainPage.DisplayPromptAsync("Кількість", "Кількість:", initialValue: details.Quantity.ToString(), keyboard: Keyboard.Numeric);
                if (!int.TryParse(qtyStr, out int newQty) || newQty < 0) return;

                await _productService.UpdateProductAsync(details.Id, newName, newQty, newPrice, details.Category, details.Description);
                await RefreshData();
            }
            catch (Exception ex) { await Shell.Current.DisplayAlert("Помилка", ex.Message, "OK"); }
        }

        // Видаляє товар після підтвердження користувачем.
        [RelayCommand]
        private async Task DeleteProduct(ProductListDTO product)
        {
            if (product == null) return;
            try
            {
                if (await Shell.Current.DisplayAlert("Підтвердження", $"Видалити {product.Name}?", "Так", "Ні"))
                {
                    await _productService.DeleteProductAsync(product.Id);
                    await RefreshData();
                }
            }
            catch (Exception ex) { await Shell.Current.DisplayAlert("Помилка", ex.Message, "OK"); }
        }
    }
}