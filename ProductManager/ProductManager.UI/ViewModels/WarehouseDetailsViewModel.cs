using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using ProductManager.Services;
using ProductManager.DTOModels.Warehouse;
using ProductManager.DTOModels.Product;
using ProductManager.UI.Pages;

namespace ProductManager.UI.ViewModels
{
    [QueryProperty(nameof(WarehouseId), "WarehouseId")]
    public class WarehouseDetailsViewModel : INotifyPropertyChanged
    {
        private readonly IWarehouseService _warehouseService;

        private Guid _warehouseId;
        public Guid WarehouseId
        {
            get => _warehouseId;
            set
            {
                _warehouseId = value;
                // Як тільки отримали ID — завантажуємо дані через сервіс
                WarehouseDetails = _warehouseService.GetWarehouse(value);
            }
        }

        private WarehouseDetailsDTO _warehouseDetails;
        public WarehouseDetailsDTO WarehouseDetails
        {
            get => _warehouseDetails;
            set
            {
                _warehouseDetails = value;
                OnPropertyChanged();
            }
        }

        private ProductListDTO _selectedProduct;
        public ProductListDTO SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
                // Якщо вибрано товар то переходимо до його детального опису
                if (value != null)
                {
                    NavigateToProduct(value);
                    SelectedProduct = null;
                }
            }
        }

        public WarehouseDetailsViewModel(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        private async void NavigateToProduct(ProductListDTO product)
        {
            var parameters = new Dictionary<string, object> { { "ProductId", product.Id } };
            await Shell.Current.GoToAsync(nameof(ProductDetailsPage), parameters);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}