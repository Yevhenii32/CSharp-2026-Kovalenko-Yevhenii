using System;
using System.Collections.Generic;
using ProductManager.DTOModels.Product;
using ProductManager.DTOModels.Warehouse;
using ProductManager.Repository;

namespace ProductManager.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IProductRepository _productRepository;

        public WarehouseService(IWarehouseRepository warehouseRepository, IProductRepository productRepository)
        {
            _warehouseRepository = warehouseRepository;
            _productRepository = productRepository;
        }

        // Формуємо список складів для головної сторінки
        public IEnumerable<WarehouseListDTO> GetAllWarehouses()
        {
            foreach (var warehouse in _warehouseRepository.GetAllWarehouses())
            {
                yield return new WarehouseListDTO(warehouse.Id, warehouse.Name, warehouse.Location);
            }
        }
        // Формуємо детальні дані про склад
        public WarehouseDetailsDTO GetWarehouse(Guid warehouseId)
        {
            var warehouse = _warehouseRepository.GetWarehouse(warehouseId);
            if (warehouse == null) return null;

            // Збираємо список товарів для цього складу
            var productDtos = new List<ProductListDTO>();
            foreach (var product in _productRepository.GetProductsByWarehouse(warehouseId))
            {
                productDtos.Add(new ProductListDTO(product.Id, product.Name, product.Price, product.Category));
            }

            return new WarehouseDetailsDTO(warehouse.Id, warehouse.Name, warehouse.Location, productDtos);
        }
    }
}