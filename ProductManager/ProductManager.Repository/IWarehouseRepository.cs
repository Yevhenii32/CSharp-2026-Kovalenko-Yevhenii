using System;
using System.Collections.Generic;
using ProductManager.DBModels;

namespace ProductManager.Repository
{
    public interface IWarehouseRepository
    {
        // Отримуємо всі склади
        IEnumerable<WarehouseDBModel> GetAllWarehouses();

        // Отримуємо конкретний склад за його ID
        WarehouseDBModel GetWarehouse(Guid warehouseId);
    }
}