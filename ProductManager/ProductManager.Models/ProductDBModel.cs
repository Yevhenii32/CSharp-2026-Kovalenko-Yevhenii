using SQLite; 
using System;

namespace ProductManager.DBModels
{
    public class ProductDBModel
    {
        [PrimaryKey] 
        public Guid Id { get; set; }

        public Guid WarehouseId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductCategory Category { get; set; }
        public string Description { get; set; }

        // Порожній конструктор
        public ProductDBModel() { }

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