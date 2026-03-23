using System;

namespace ProductManager.DBModels
{
    public class ProductDBModel
    {
        public Guid Id { get; set; }
        public Guid WarehouseId { get; set; } 
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductCategory Category { get; set; }
        public string Description { get; set; }

        // Генеруємо новий ID за замовчуванням для нових об'єктів.
        // При завантаженні з StorageContext цей ID буде перезаписаний існуючим значенням.
        public ProductDBModel(Guid warehouseId, string name, int quantity, decimal price, ProductCategory category, string description)
        {
            Id = Guid.NewGuid();
            WarehouseId = warehouseId;
            Name = name;
            Quantity = quantity;
            Price = price;
            Category = category;
            Description = description;
        }
    }
}