using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManager.DBModels;

namespace ProductManager.Storage
{
    public interface IStorageContext
    {
        IAsyncEnumerable<WarehouseDBModel> GetAllWarehousesAsync();
        Task<WarehouseDBModel> GetWarehouseAsync(Guid warehouseId);
        Task AddWarehouseAsync(WarehouseDBModel warehouse);
        Task UpdateWarehouseAsync(WarehouseDBModel warehouse);
        Task DeleteWarehouseAsync(Guid warehouseId);

        Task<IEnumerable<ProductDBModel>> GetProductsByWarehouseAsync(Guid warehouseId);
        Task<ProductDBModel> GetProductAsync(Guid productId);
        Task AddProductAsync(ProductDBModel product);
        Task UpdateProductAsync(ProductDBModel product);
        Task DeleteProductAsync(Guid productId);
    }
}