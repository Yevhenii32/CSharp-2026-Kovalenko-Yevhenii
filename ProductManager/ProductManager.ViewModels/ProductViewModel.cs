using System;
using ProductManager.Models;

namespace ProductManager.ViewModels
{
    public class ProductViewModel
    {
        // Прокидаємо властивості
        public Guid Id { get; private set; }
        public Guid WarehouseId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductCategory Category { get; set; }
        public string Description {  get; set; }

        // Обчислюване поле: загальна вартість товару (ціна * кількість)
        public decimal TotalValue => Price * Quantity;

        // Конструктор для створення нового товару
        public ProductViewModel()
        {
            Id = Guid.NewGuid();
        }
        // Конструктор приймає оригінальну модель товару
        public ProductViewModel(Product product)
        {
            Id = product.Id;
            WarehouseId = product.WarehouseId;
            Name = product.Name;
            Quantity = product.Quantity;
            Price = product.Price;
            Category = product.Category;
            Description = product.Description;
        }
    }
}