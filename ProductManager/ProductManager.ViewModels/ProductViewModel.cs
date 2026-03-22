using System;
using ProductManager.Models;

namespace ProductManager.ViewModels
{
    public class ProductViewModel
    {
        // Прокидаємо властивості
        public Guid Id { get; set; }
        public Guid WarehouseId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductCategory Category { get; set; }
        public string Description {  get; set; }

        // Переклад категорії для інтерфейсу 
        public string CategoryName => Category switch
        {
            ProductCategory.Electronics => "Електроніка",
            ProductCategory.Clothing => "Одяг",
            ProductCategory.Groceries => "Продукти",
            ProductCategory.Furniture => "Меблі",
            ProductCategory.Tools => "Інструменти",
            _ => "Інше"
        };

        // Обчислюване поле: загальна вартість товару (ціна * кількість)
        public decimal TotalValue => Price * Quantity;

        // Конструктор для створення нового товару
        public ProductViewModel()
        {
          
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