using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using ProductManager.Services;
using ProductManager.DTOModels.Product;

namespace ProductManager.UI.ViewModels
{
    [QueryProperty(nameof(ProductId), "ProductId")]
    public class ProductDetailsViewModel : INotifyPropertyChanged
    {
        private readonly IProductService _productService;

        private Guid _productId;
        public Guid ProductId
        {
            get => _productId;
            set
            {
                _productId = value;
                // Завантажуємо повну інформацію про товар за отриманим ID
                ProductDetails = _productService.GetProduct(value);
            }
        }

        private ProductDetailsDTO _productDetails;
        public ProductDetailsDTO ProductDetails
        {
            get => _productDetails;
            set
            {
                _productDetails = value;
                OnPropertyChanged();
            }
        }

        public ProductDetailsViewModel(IProductService productService)
        {
            _productService = productService;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}