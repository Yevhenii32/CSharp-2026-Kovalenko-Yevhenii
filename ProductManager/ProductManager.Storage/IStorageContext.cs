using System;
using System.Collections.Generic;
using ProductManager.DBModels;

namespace ProductManager.Storage
{
    public interface IStorageContext
    {
        IEnumerable<WarehouseDBModel> GetAllWarehouses();
        WarehouseDBModel GetWarehouse(Guid warehouseId);
        IEnumerable<ProductDBModel> GetProductsByWarehouse(Guid warehouseId);
        ProductDBModel GetProduct(Guid productId);
    }
}