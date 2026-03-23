using ProductManager.DBModels;
using System;
using System.Collections.Generic;

namespace ProductManager.DTOModels.Warehouse
{
    public class WarehouseDetailsDTO
    {
        public Guid Id { get; }
        public string Name { get; }
        public Location Location { get; }
        // Детальна сторінка складу також містить список товарів на ньому
        public IEnumerable<ProductManager.DTOModels.Product.ProductListDTO> Products { get; }

        public WarehouseDetailsDTO(Guid id, string name, Location location, IEnumerable<ProductManager.DTOModels.Product.ProductListDTO> products)
        {
            Id = id;
            Name = name;
            Location = location;
            Products = products;
        }
    }
}