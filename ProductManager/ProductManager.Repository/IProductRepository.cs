using System;
using System.Collections.Generic;
using ProductManager.DBModels;

namespace ProductManager.Repository
{
    public interface IProductRepository
    {
        // Отримуємо всі товари, що лежать на конкретному складі
        IEnumerable<ProductDBModel> GetProductsByWarehouse(Guid warehouseId);

        // Отримуємо деталі конкретного товару
        ProductDBModel GetProduct(Guid productId);
    }
}