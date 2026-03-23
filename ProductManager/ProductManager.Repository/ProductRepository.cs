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

        public IEnumerable<ProductDBModel> GetProductsByWarehouse(Guid warehouseId)
        {
            return _storageContext.GetProductsByWarehouse(warehouseId);
        }

        public ProductDBModel GetProduct(Guid productId)
        {
            return _storageContext.GetProduct(productId);
        }
    }
}