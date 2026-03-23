using System;

namespace ProductManager.DBModels
{
    public class ProductDBModel
    {
        public Guid Id { get; }
        public Guid WarehouseId { get; set; } 
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductCategory Category { get; set; }
        public string Description { get; set; }

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