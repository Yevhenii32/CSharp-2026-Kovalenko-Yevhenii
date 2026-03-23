using System;
using System.Collections.Generic;
using ProductManager.DTOModels.Warehouse;

namespace ProductManager.Services
{
    public interface IWarehouseService
    {
        IEnumerable<WarehouseListDTO> GetAllWarehouses();
        WarehouseDetailsDTO GetWarehouse(Guid warehouseId);
    }
}