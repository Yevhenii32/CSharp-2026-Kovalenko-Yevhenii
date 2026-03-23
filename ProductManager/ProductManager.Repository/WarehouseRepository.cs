using System;
using System.Collections.Generic;
using ProductManager.DBModels;
using ProductManager.Storage;

namespace ProductManager.Repository
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly IStorageContext _storageContext;

        // Впроваджуємо сховище через конструктор
        public WarehouseRepository(IStorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        public IEnumerable<WarehouseDBModel> GetAllWarehouses()
        {
            return _storageContext.GetAllWarehouses();
        }

        public WarehouseDBModel GetWarehouse(Guid warehouseId)
        {
            return _storageContext.GetWarehouse(warehouseId);
        }
    }
}