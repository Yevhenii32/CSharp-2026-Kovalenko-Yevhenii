using System;
using System.Collections.Generic;
using ProductManager.DTOModels.Product;

namespace ProductManager.Services
{
    public interface IProductService
    {
        IEnumerable<ProductListDTO> GetProductsByWarehouse(Guid warehouseId);
        ProductDetailsDTO GetProduct(Guid productId);
    }
}