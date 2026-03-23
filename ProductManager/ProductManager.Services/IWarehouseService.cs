using System;
using System.Collections.Generic;
using ProductManager.DTOModels.Warehouse;

namespace ProductManager.Services
{
    public interface IWarehouseService
    {
        // Повертаємо список легких моделей для головного екрана
        IEnumerable<WarehouseListDTO> GetAllWarehouses();
        // Повертаємо повну інформацію про склад разом з його товарами
        WarehouseDetailsDTO GetWarehouse(Guid warehouseId);
    }
}