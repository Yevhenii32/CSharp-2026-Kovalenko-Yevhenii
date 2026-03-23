using ProductManager.DBModels;
using System;

namespace ProductManager.DTOModels.Product
{
    public class ProductListDTO
    {
        public Guid Id { get; }
        public string Name { get; }
        public decimal Price { get; }
        public ProductCategory Category { get; }

        public ProductListDTO(Guid id, string name, decimal price, ProductCategory category)
        {
            Id = id;
            Name = name;
            Price = price;
            Category = category;
        }
    }
}