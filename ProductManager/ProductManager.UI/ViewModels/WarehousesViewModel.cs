using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using ProductManager.Services;
using ProductManager.DTOModels.Warehouse;
using ProductManager.UI.Pages;
using System.Collections.Generic;

namespace ProductManager.UI.ViewModels
{
    public class WarehousesViewModel : INotifyPropertyChanged
    {
        private readonly IWarehouseService _warehouseService;

        // Колекція складів, яка автоматично оновлює список на екрані
        public ObservableCollection<WarehouseListDTO> Warehouses { get; } = new();

        private WarehouseListDTO _selectedWarehouse;
        public WarehouseListDTO SelectedWarehouse
        {
            get => _selectedWarehouse;
            set
            {
                _selectedWarehouse = value;
                OnPropertyChanged();

                // Якщо користувач вибрав склад — переходимо на сторінку деталей
                if (value != null)
                {
                    NavigateToDetails(value);
                    SelectedWarehouse = null;
                }
            }
        }

        public WarehousesViewModel(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
            LoadWarehouses();
        }

        // Метод для початкового завантаження даних із сервісу
        private void LoadWarehouses()
        {
            Warehouses.Clear();
            foreach (var w in _warehouseService.GetAllWarehouses())
            {
                Warehouses.Add(w);
            }
        }

        // Логіка навігації до деталей складу
        private async void NavigateToDetails(WarehouseListDTO warehouse)
        {
            var parameters = new Dictionary<string, object> { { "WarehouseId", warehouse.Id } };
            await Shell.Current.GoToAsync(nameof(WarehouseDetailsPage), parameters);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}