using System;
using System.Collections.Generic;
using ProductManager.Models;

namespace ProductManager.Services
{
    public interface IStorageService
    {
        IReadOnlyList<Warehouse> GetAllWarehouses();
        Warehouse GetWarehouseById(Guid id);
        IReadOnlyList<Product> GetProductsByWarehouseId(Guid warehouseId);
        Product GetProductById(Guid id);
    }
}