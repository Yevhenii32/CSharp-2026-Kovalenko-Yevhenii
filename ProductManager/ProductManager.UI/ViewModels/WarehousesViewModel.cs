using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using ProductManager.DTOModels.Warehouse;
using ProductManager.Services;
using ProductManager.UI.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ProductManager.UI.ViewModels
{
    // Модель представлення для головної сторінки зі списком усіх складів.
    public partial class WarehousesViewModel : BaseViewModel
    {
        private readonly IWarehouseService _warehouseService;
       
        // Колекція складів для відображення в інтерфейсі користувача.
        [ObservableProperty]
        private ObservableCollection<WarehouseListDTO> _warehouses = new();
        
        //Поточний вибраний склад зі списку.
        [ObservableProperty]
        private WarehouseListDTO _currentWarehouse;

        public WarehousesViewModel(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
            // Завантажуємо дані при створенні ViewModel
            _ = RefreshData();
        }
        // Асинхронне завантаження списку складів з бази даних.
        [RelayCommand]
        internal async Task RefreshData()
        {
            IsBusy = true;
            try
            {
                Warehouses.Clear();
                await foreach (var warehouse in _warehouseService.GetAllWarehousesAsync())
                {
                    Warehouses.Add(warehouse);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Помилка", $"Не вдалося завантажити склади: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        // Виконує перехід на сторінку деталей вибраного складу.
        [RelayCommand]
        private async Task GotoWarehouseDetails(WarehouseListDTO warehouse)
        {
            if (warehouse == null) return;
            IsBusy = true;
            try
            {
                await Shell.Current.GoToAsync($"{nameof(WarehouseDetailsPage)}", new Dictionary<string, object> { { "WarehouseId", warehouse.Id } });
                CurrentWarehouse = null; // Скидаємо виділення після переходу
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

        // Викликає діалогове вікно для створення нового складу та зберігає його в базу
        [RelayCommand]
        private async Task AddWarehouse()
        {
            try
            {
                string name = await Application.Current.MainPage.DisplayPromptAsync("Новий склад", "Введіть назву:");
                if (!string.IsNullOrWhiteSpace(name))
                {
                    await _warehouseService.CreateWarehouseAsync(name, ProductManager.DBModels.Location.Kyiv);
                    await RefreshData();
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Помилка", $"Помилка створення: {ex.Message}", "OK");
            }
        }

        // Видаляє вибраний склад та всі пов'язані з ним товари після підтвердження.
        [RelayCommand]
        private async Task DeleteWarehouse(WarehouseListDTO warehouse)
        {
            if (warehouse == null) return;
            try
            {
                if (await Shell.Current.DisplayAlert("Підтвердження", $"Видалити {warehouse.Name} та всі товари?", "Так", "Ні"))
                {
                    await _warehouseService.DeleteWarehouseAsync(warehouse.Id);
                    Warehouses.Remove(warehouse);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Помилка", $"Помилка видалення: {ex.Message}", "OK");
            }
        }
    }
}