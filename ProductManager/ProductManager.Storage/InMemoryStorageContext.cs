using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProductManager.DBModels;

namespace ProductManager.Storage
{
    public class InMemoryStorageContext : IStorageContext
    {
        private record class WarehouseRecord(Guid Id, string Name, Location Location);
        private record class ProductRecord(Guid Id, Guid WarehouseId, string Name, int Quantity, decimal Price, ProductCategory Category, string Description);

        private static readonly List<WarehouseRecord> _warehouses = new List<WarehouseRecord>();
        private static readonly List<ProductRecord> _products = new List<ProductRecord>();

        static InMemoryStorageContext()
        {
            var mainWarehouse = new WarehouseRecord(Guid.NewGuid(), "Головний склад", Location.Kyiv);
            var reserveWarehouse = new WarehouseRecord(Guid.NewGuid(), "Резервний склад", Location.Lviv);
            var thirdWarehouse = new WarehouseRecord(Guid.NewGuid(), "Південний склад", Location.Kharkiv);

            _warehouses.Add(mainWarehouse);
            _warehouses.Add(reserveWarehouse);
            _warehouses.Add(thirdWarehouse);

            _products.Add(new ProductRecord(Guid.NewGuid(), mainWarehouse.Id, "Ноутбук Lenovo", 10, 25000m, ProductCategory.Electronics, "Ігровий ноутбук"));
            _products.Add(new ProductRecord(Guid.NewGuid(), mainWarehouse.Id, "Мишка Logitech", 50, 1500m, ProductCategory.Electronics, "Бездротова миша"));
            _products.Add(new ProductRecord(Guid.NewGuid(), mainWarehouse.Id, "Клавіатура Keychron", 20, 3500m, ProductCategory.Electronics, "Механічна клавіатура"));

            _products.Add(new ProductRecord(Guid.NewGuid(), reserveWarehouse.Id, "Стіл офісний", 5, 4000m, ProductCategory.Furniture, "Дерев'яний стіл"));
            _products.Add(new ProductRecord(Guid.NewGuid(), reserveWarehouse.Id, "Крісло ергономічне", 12, 7500m, ProductCategory.Furniture, "Офісне крісло з підтримкою спини"));

            _products.Add(new ProductRecord(Guid.NewGuid(), thirdWarehouse.Id, "Яблука Голден", 500, 35m, ProductCategory.Groceries, "Свіжі яблука"));
            _products.Add(new ProductRecord(Guid.NewGuid(), thirdWarehouse.Id, "Кава в зернах", 100, 450m, ProductCategory.Groceries, "Арабіка 100%"));
        }

        public async IAsyncEnumerable<WarehouseDBModel> GetAllWarehousesAsync()
        {
            foreach (var w in _warehouses)
            {
                await Task.Delay(100);
                yield return new WarehouseDBModel { Id = w.Id, Name = w.Name, Location = w.Location };
            }
        }

        public Task<WarehouseDBModel> GetWarehouseAsync(Guid warehouseId)
        {
            return Task.Run(() =>
            {
                var w = _warehouses.FirstOrDefault(warehouse => warehouse.Id == warehouseId);
                return w is null ? null : new WarehouseDBModel { Id = w.Id, Name = w.Name, Location = w.Location };
            });
        }

        public Task<IEnumerable<ProductDBModel>> GetProductsByWarehouseAsync(Guid warehouseId)
        {
            return Task.Run(() =>
            {
                return _products
                    .Where(p => p.WarehouseId == warehouseId)
                    .Select(p => new ProductDBModel(p.WarehouseId, p.Name, p.Quantity, p.Price, p.Category, p.Description) { Id = p.Id });
            });
        }

        public Task<ProductDBModel> GetProductAsync(Guid productId)
        {
            return Task.Run(() =>
            {
                var p = _products.FirstOrDefault(product => product.Id == productId);
                return p is null ? null : new ProductDBModel(p.WarehouseId, p.Name, p.Quantity, p.Price, p.Category, p.Description) { Id = p.Id };
            });
        }

        public Task AddWarehouseAsync(WarehouseDBModel warehouse) => throw new NotImplementedException();
        public Task UpdateWarehouseAsync(WarehouseDBModel warehouse) => throw new NotImplementedException();
        public Task DeleteWarehouseAsync(Guid warehouseId) => throw new NotImplementedException();
        public Task AddProductAsync(ProductDBModel product) => throw new NotImplementedException();
        public Task UpdateProductAsync(ProductDBModel product) => throw new NotImplementedException();
        public Task DeleteProductAsync(Guid productId) => throw new NotImplementedException();
    }
}