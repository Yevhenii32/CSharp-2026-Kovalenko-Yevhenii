using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using ProductManager.DTOModels.Product;
using ProductManager.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManager.UI.ViewModels
{
    // Модель представлення для сторінки деталей товару.
    public partial class ProductDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IProductService _productService;
        private Guid _productId;

        // Детальна інформація про вибраний товар.
        [ObservableProperty]
        private ProductDetailsDTO _currentProduct;

        public ProductDetailsViewModel(IProductService productService)
        {
            _productService = productService;
        }

        // Приймає ідентифікатор товару при навігації на цю сторінку.
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("ProductId", out var idValue) && idValue is Guid id)
            {
                _productId = id;
                _ = RefreshData();
            }
        }

        // Завантажує повну інформацію про товар з бази даних.
        [RelayCommand]
        internal async Task RefreshData()
        {
            IsBusy = true;
            try
            {
                CurrentProduct = await _productService.GetProductAsync(_productId) ?? throw new Exception("Товар не знайдено.");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Помилка", $"Помилка завантаження товару: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}