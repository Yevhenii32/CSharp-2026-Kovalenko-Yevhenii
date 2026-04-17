using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManager.DBModels;

namespace ProductManager.Repository
{
    public interface IWarehouseRepository
    {
        IAsyncEnumerable<WarehouseDBModel> GetAllWarehousesAsync();
        Task<WarehouseDBModel> GetWarehouseAsync(Guid warehouseId);
        Task AddWarehouseAsync(WarehouseDBModel warehouse);
        Task UpdateWarehouseAsync(WarehouseDBModel warehouse);
        Task DeleteWarehouseAsync(Guid warehouseId);
    }
}