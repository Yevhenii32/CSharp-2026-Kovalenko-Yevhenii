using System;
using ProductManager.Models;

namespace ProductManager.ViewModels
{
    public class ProductViewModel
    {
        private readonly Product _product;

        // Конструктор приймає оригінальну модель товару
        public ProductViewModel(Product product)
        {
            _product = product;
        }

        // Прокидаємо властивості з оригінальної моделі
        public Guid Id => _product.Id;
        public Guid WarehouseId => _product.WarehouseId;
        public string Name => _product.Name;
        public int Quantity => _product.Quantity;
        public decimal Price => _product.Price;
        public ProductCategory Category => _product.Category;
        public string Description => _product.Description;

        // Обчислюване поле: загальна вартість товару (ціна * кількість)
        public decimal TotalValue => _product.Price * _product.Quantity;
    }
}