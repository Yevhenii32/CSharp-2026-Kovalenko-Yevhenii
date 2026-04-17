using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManager.DTOModels.Warehouse;
using ProductManager.DBModels;

namespace ProductManager.Services
{
    public interface IWarehouseService
    {
        IAsyncEnumerable<WarehouseListDTO> GetAllWarehousesAsync();
        Task<WarehouseDetailsDTO> GetWarehouseAsync(Guid warehouseId);

        Task CreateWarehouseAsync(string name, Location location);
        Task UpdateWarehouseAsync(Guid id, string name, Location location);
        Task DeleteWarehouseAsync(Guid warehouseId);
    }
}