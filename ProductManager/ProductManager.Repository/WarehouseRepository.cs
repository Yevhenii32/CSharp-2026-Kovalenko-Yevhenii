using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManager.DBModels;
using ProductManager.Storage;

namespace ProductManager.Repository
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly IStorageContext _storageContext;

        public WarehouseRepository(IStorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        public IAsyncEnumerable<WarehouseDBModel> GetAllWarehousesAsync() => _storageContext.GetAllWarehousesAsync();
        public Task<WarehouseDBModel> GetWarehouseAsync(Guid warehouseId) => _storageContext.GetWarehouseAsync(warehouseId);
        public Task AddWarehouseAsync(WarehouseDBModel warehouse) => _storageContext.AddWarehouseAsync(warehouse);
        public Task UpdateWarehouseAsync(WarehouseDBModel warehouse) => _storageContext.UpdateWarehouseAsync(warehouse);
        public Task DeleteWarehouseAsync(Guid warehouseId) => _storageContext.DeleteWarehouseAsync(warehouseId);
    }
}