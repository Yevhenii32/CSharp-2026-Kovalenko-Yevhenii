using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManager.DTOModels.Product;
using ProductManager.DTOModels.Warehouse;
using ProductManager.Repository;
using ProductManager.DBModels;

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

        public async IAsyncEnumerable<WarehouseListDTO> GetAllWarehousesAsync()
        {
            await foreach (var warehouse in _warehouseRepository.GetAllWarehousesAsync())
            {
                yield return new WarehouseListDTO(warehouse.Id, warehouse.Name, warehouse.Location);
            }
        }

        public async Task<WarehouseDetailsDTO> GetWarehouseAsync(Guid warehouseId)
        {
            var warehouse = await _warehouseRepository.GetWarehouseAsync(warehouseId);
            if (warehouse == null) return null;

            var productDtos = new List<ProductListDTO>();
            var products = await _productRepository.GetProductsByWarehouseAsync(warehouseId);
            foreach (var product in products)
            {
                productDtos.Add(new ProductListDTO(product.Id, product.Name, product.Price, product.Category));
            }

            return new WarehouseDetailsDTO(warehouse.Id, warehouse.Name, warehouse.Location, productDtos);
        }

        public async Task CreateWarehouseAsync(string name, Location location)
        {
            var newWarehouse = new WarehouseDBModel(name, location);
            await _warehouseRepository.AddWarehouseAsync(newWarehouse);
        }

        public async Task UpdateWarehouseAsync(Guid id, string name, Location location)
        {
            var warehouse = await _warehouseRepository.GetWarehouseAsync(id);
            if (warehouse != null)
            {
                warehouse.Name = name;
                warehouse.Location = location;
                await _warehouseRepository.UpdateWarehouseAsync(warehouse);
            }
        }

        public async Task DeleteWarehouseAsync(Guid warehouseId)
        {
            await _warehouseRepository.DeleteWarehouseAsync(warehouseId);
        }
    }
}