using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManager.DTOModels.Product;
using ProductManager.DBModels;

namespace ProductManager.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductListDTO>> GetProductsByWarehouseAsync(Guid warehouseId);
        Task<ProductDetailsDTO> GetProductAsync(Guid productId);

        Task CreateProductAsync(Guid warehouseId, string name, int quantity, decimal price, ProductCategory category, string description);
        Task UpdateProductAsync(Guid id, string name, int quantity, decimal price, ProductCategory category, string description);
        Task DeleteProductAsync(Guid productId);
    }
}