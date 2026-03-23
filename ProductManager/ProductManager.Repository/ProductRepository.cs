using System;
using System.Collections.Generic;
using ProductManager.DBModels;
using ProductManager.Storage;

namespace ProductManager.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IStorageContext _storageContext;

        public ProductRepository(IStorageContext storageContext)
        {
            _storageContext = storageContext;
        }
        // Отримуємо товари за ідентифікатором складу
        public IEnumerable<ProductDBModel> GetProductsByWarehouse(Guid warehouseId)
        {
            return _storageContext.GetProductsByWarehouse(warehouseId);
        }
        // Шукаємо конкретний товар
        public ProductDBModel GetProduct(Guid productId)
        {
            return _storageContext.GetProduct(productId);
        }
    }
}