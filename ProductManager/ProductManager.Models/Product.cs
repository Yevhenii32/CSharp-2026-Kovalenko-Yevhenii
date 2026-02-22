using System;

namespace ProductManager.Models
{
    public class Product
    {
        public Guid Id { get; }
        public Guid WarehouseId { get; set; } 
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductCategory Category { get; set; }
        public string Description { get; set; }

        public Product(Guid id, Guid warehouseId, string name, int quantity, decimal price, ProductCategory category, string description)
        {
            Id = id;
            WarehouseId = warehouseId;
            Name = name;
            Quantity = quantity;
            Price = price;
            Category = category;
            Description = description;
        }
    }
}