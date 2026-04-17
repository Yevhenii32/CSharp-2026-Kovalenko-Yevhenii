using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManager.DBModels;

namespace ProductManager.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDBModel>> GetProductsByWarehouseAsync(Guid warehouseId);
        Task<ProductDBModel> GetProductAsync(Guid productId);
        Task AddProductAsync(ProductDBModel product);
        Task UpdateProductAsync(ProductDBModel product);
        Task DeleteProductAsync(Guid productId);
    }
}