using ProductManager.DBModels;
using System;

namespace ProductManager.DTOModels.Product
{
    public class ProductDetailsDTO
    {
        public Guid Id { get; }
        public Guid WarehouseId { get; }
        public string Name { get; }
        public int Quantity { get; }
        public decimal Price { get; }
        public ProductCategory Category { get; }
        public string Description { get; }

        public ProductDetailsDTO(Guid id, Guid warehouseId, string name, int quantity, decimal price, ProductCategory category, string description)
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