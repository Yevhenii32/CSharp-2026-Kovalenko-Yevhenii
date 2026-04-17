using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManager.DBModels;
using ProductManager.Storage;

namespace ProductManager.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IStorageContext _storageContext;

        public ProductRepository(IStorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        public Task<IEnumerable<ProductDBModel>> GetProductsByWarehouseAsync(Guid warehouseId) => _storageContext.GetProductsByWarehouseAsync(warehouseId);
        public Task<ProductDBModel> GetProductAsync(Guid productId) => _storageContext.GetProductAsync(productId);
        public Task AddProductAsync(ProductDBModel product) => _storageContext.AddProductAsync(product);
        public Task UpdateProductAsync(ProductDBModel product) => _storageContext.UpdateProductAsync(product);
        public Task DeleteProductAsync(Guid productId) => _storageContext.DeleteProductAsync(productId);
    }
}