using System;
using System.Collections.Generic;
using ProductManager.DTOModels.Product;
using ProductManager.Repository;

namespace ProductManager.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        // Перетворюємо список моделей бази даних у список DTO
        public IEnumerable<ProductListDTO> GetProductsByWarehouse(Guid warehouseId)
        {
            foreach (var product in _productRepository.GetProductsByWarehouse(warehouseId))
            {
                yield return new ProductListDTO(product.Id, product.Name, product.Price, product.Category);
            }
        }
        // Отримуємо детальні дані про товар
        public ProductDetailsDTO GetProduct(Guid productId)
        {
            var product = _productRepository.GetProduct(productId);
            return product is null ? null : new ProductDetailsDTO(product.Id, product.WarehouseId, product.Name, product.Quantity, product.Price, product.Category, product.Description);
        }
    }
}