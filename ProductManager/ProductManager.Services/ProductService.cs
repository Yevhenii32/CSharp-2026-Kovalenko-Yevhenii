using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductManager.DTOModels.Product;
using ProductManager.Repository;
using ProductManager.DBModels;

namespace ProductManager.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductListDTO>> GetProductsByWarehouseAsync(Guid warehouseId)
        {
            return (await _productRepository.GetProductsByWarehouseAsync(warehouseId))
                .Select(product => new ProductListDTO(product.Id, product.Name, product.Price, product.Category));
        }

        public async Task<ProductDetailsDTO> GetProductAsync(Guid productId)
        {
            var product = await _productRepository.GetProductAsync(productId);
            return product is null ? null : new ProductDetailsDTO(product.Id, product.WarehouseId, product.Name, product.Quantity, product.Price, product.Category, product.Description);
        }

        public async Task CreateProductAsync(Guid warehouseId, string name, int quantity, decimal price, ProductCategory category, string description)
        {
            var product = new ProductDBModel(warehouseId, name, quantity, price, category, description);
            await _productRepository.AddProductAsync(product);
        }

        public async Task UpdateProductAsync(Guid id, string name, int quantity, decimal price, ProductCategory category, string description)
        {
            var product = await _productRepository.GetProductAsync(id);
            if (product != null)
            {
                product.Name = name;
                product.Quantity = quantity;
                product.Price = price;
                product.Category = category;
                product.Description = description;
                await _productRepository.UpdateProductAsync(product);
            }
        }

        public async Task DeleteProductAsync(Guid productId)
        {
            await _productRepository.DeleteProductAsync(productId);
        }
    }
}