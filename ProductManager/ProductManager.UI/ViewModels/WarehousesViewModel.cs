using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
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
    // Забезпечує перегляд списку складів, пошук, сортування та повний цикл CRUD.
    public partial class WarehousesViewModel : BaseViewModel
    {
        private readonly IWarehouseService _warehouseService;
        private List<WarehouseListDTO> _allWarehouses = new();

        // Колекція складів, що відображаються на екрані.
        [ObservableProperty]
        private ObservableCollection<WarehouseListDTO> _warehouses = new();

        // Поточний вибраний склад.
        [ObservableProperty]
        private WarehouseListDTO _currentWarehouse;
        // Текст для пошуку складу за назвою.
        [ObservableProperty]
        private string _searchText = string.Empty;
         
        // Обрана опція сортування.
        [ObservableProperty]
        private string _selectedSortOption = "За назвою (А-Я)";

        // Доступні варіанти сортування для користувача.
        public List<string> SortOptions { get; } = new() { "За назвою (А-Я)", "За назвою (Я-А)", "За містом" };

        partial void OnSearchTextChanged(string value) => ApplyFilters();
        partial void OnSelectedSortOptionChanged(string value) => ApplyFilters();

        public WarehousesViewModel(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        // Асинхронно завантажує список усіх складів із сервісу.
        [RelayCommand]
        internal async Task RefreshData()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                _allWarehouses.Clear();
                await foreach (var warehouse in _warehouseService.GetAllWarehousesAsync())
                {
                    _allWarehouses.Add(warehouse);
                }
                ApplyFilters();
            }
            catch (Exception ex) { await Shell.Current.DisplayAlert("Помилка", ex.Message, "OK"); }
            finally { IsBusy = false; }
        }

        // Застосовує логіку пошуку та сортування до завантаженого списку складів.
        private void ApplyFilters()
        {
            var filtered = _allWarehouses.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(SearchText))
                filtered = filtered.Where(w => w.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            filtered = SelectedSortOption switch
            {
                "За назвою (Я-А)" => filtered.OrderByDescending(w => w.Name),
                "За містом" => filtered.OrderBy(w => w.Location.ToString()),
                _ => filtered.OrderBy(w => w.Name)
            };

            Warehouses.Clear();
            foreach (var w in filtered) Warehouses.Add(w);
        }

        // Здійснює навігацію до сторінки деталей обраного складу.
        [RelayCommand]
        private async Task GotoWarehouseDetails(WarehouseListDTO warehouse)
        {
            if (warehouse == null) return;
            IsBusy = true;
            try
            {
                await Shell.Current.GoToAsync($"{nameof(WarehouseDetailsPage)}", new Dictionary<string, object> { { "WarehouseId", warehouse.Id } });
                CurrentWarehouse = null;
            }
            catch (Exception ex) { await Shell.Current.DisplayAlert("Помилка", ex.Message, "OK"); }
            finally { IsBusy = false; }
        }

        // Створює новий склад через діалогові вікна з валідацією.
        [RelayCommand]
        private async Task AddWarehouse()
        {
            try
            {
                string name = await Application.Current.MainPage.DisplayPromptAsync("Новий склад", "Введіть назву складу (обов'язково):");
                if (name == null) return;
                if (string.IsNullOrWhiteSpace(name))
                {
                    await Shell.Current.DisplayAlert("Помилка", "Назва складу не може бути порожньою!", "OK");
                    return;
                }

                string locationStr = await Application.Current.MainPage.DisplayActionSheet("Оберіть місто", "Скасувати", null, "Київ", "Львів", "Харків");
                if (locationStr == "Скасувати" || locationStr == null) return;

                var location = locationStr switch
                {
                    "Львів" => ProductManager.DBModels.Location.Lviv,
                    "Харків" => ProductManager.DBModels.Location.Kharkiv,
                    _ => ProductManager.DBModels.Location.Kyiv
                };

                await _warehouseService.CreateWarehouseAsync(name, location);
                await RefreshData();
            }
            catch (Exception ex) { await Shell.Current.DisplayAlert("Помилка", ex.Message, "OK"); }
        }

        // Редагує існуючий склад (назву та локацію).
        [RelayCommand]
        private async Task EditWarehouse(WarehouseListDTO warehouse)
        {
            if (warehouse == null) return;
            try
            {
                string newName = await Application.Current.MainPage.DisplayPromptAsync("Редагування", "Змініть назву:", initialValue: warehouse.Name);
                if (string.IsNullOrWhiteSpace(newName)) return;

                string locationStr = await Application.Current.MainPage.DisplayActionSheet("Оберіть місто", "Скасувати", null, "Київ", "Львів", "Харків");
                if (locationStr == "Скасувати" || locationStr == null) return;

                var newLocation = locationStr switch
                {
                    "Львів" => ProductManager.DBModels.Location.Lviv,
                    "Харків" => ProductManager.DBModels.Location.Kharkiv,
                    _ => ProductManager.DBModels.Location.Kyiv
                };

                await _warehouseService.UpdateWarehouseAsync(warehouse.Id, newName, newLocation);
                await RefreshData();
            }
            catch (Exception ex) { await Shell.Current.DisplayAlert("Помилка", ex.Message, "OK"); }
        }

        // Видаляє склад та всі пов'язані з ним товари.
        [RelayCommand]
        private async Task DeleteWarehouse(WarehouseListDTO warehouse)
        {
            if (warehouse == null) return;
            try
            {
                if (await Shell.Current.DisplayAlert("Підтвердження", $"Видалити {warehouse.Name} та всі товари?", "Так", "Ні"))
                {
                    await _warehouseService.DeleteWarehouseAsync(warehouse.Id);
                    await RefreshData();
                }
            }
            catch (Exception ex) { await Shell.Current.DisplayAlert("Помилка", ex.Message, "OK"); }
        }
    }
}