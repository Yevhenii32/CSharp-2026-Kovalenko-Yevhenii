using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ProductManager.DBModels;

namespace ProductManager.Storage
{
    public class SQLiteStorageContext : IStorageContext
    {
        private const string DatabaseFileName = "product_manager.db3";
        private static readonly string DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DB storage", DatabaseFileName);
        private SQLiteAsyncConnection _databaseConnection;

        private async Task Init()
        {
            if (_databaseConnection is not null)
                return;

            bool isFirstLaunch = !File.Exists(DatabasePath);

            if (isFirstLaunch)
                await CreateMockStorage();
            else
                _databaseConnection = new SQLiteAsyncConnection(DatabasePath);
        }

        private async Task CreateMockStorage()
        {
            var directory = Path.GetDirectoryName(DatabasePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            _databaseConnection = new SQLiteAsyncConnection(DatabasePath);
            var inMemoryStorage = new InMemoryStorageContext();

            await _databaseConnection.CreateTableAsync<WarehouseDBModel>();
            await _databaseConnection.CreateTableAsync<ProductDBModel>();

            // Завантажуємо дані з InMemoryStorage в SQLite
            await foreach (var warehouse in inMemoryStorage.GetAllWarehousesAsync())
            {
                await _databaseConnection.InsertAsync(warehouse);
                var products = await inMemoryStorage.GetProductsByWarehouseAsync(warehouse.Id);
                await _databaseConnection.InsertAllAsync(products);
            }
        }

        public async IAsyncEnumerable<WarehouseDBModel> GetAllWarehousesAsync()
        {
            await Init();
            foreach (var warehouse in await _databaseConnection.Table<WarehouseDBModel>().ToListAsync())
            {
                yield return warehouse;
            }
        }

        public async Task<WarehouseDBModel> GetWarehouseAsync(Guid warehouseId)
        {
            await Init();
            return await _databaseConnection.Table<WarehouseDBModel>().FirstOrDefaultAsync(w => w.Id == warehouseId);
        }

        public async Task AddWarehouseAsync(WarehouseDBModel warehouse)
        {
            await Init();
            await _databaseConnection.InsertAsync(warehouse);
        }

        public async Task UpdateWarehouseAsync(WarehouseDBModel warehouse)
        {
            await Init();
            await _databaseConnection.UpdateAsync(warehouse);
        }

        public async Task DeleteWarehouseAsync(Guid warehouseId)
        {
            await Init();
            // Спочатку видаляємо всі товари на цьому складі
            var productsToDelete = await _databaseConnection.Table<ProductDBModel>().Where(p => p.WarehouseId == warehouseId).ToListAsync();
            foreach (var product in productsToDelete)
            {
                await _databaseConnection.DeleteAsync(product);
            }
            // Потім видаляємо сам склад
            await _databaseConnection.DeleteAsync<WarehouseDBModel>(warehouseId);
        }

        public async Task<IEnumerable<ProductDBModel>> GetProductsByWarehouseAsync(Guid warehouseId)
        {
            await Init();
            return await _databaseConnection.Table<ProductDBModel>().Where(p => p.WarehouseId == warehouseId).ToListAsync();
        }

        public async Task<ProductDBModel> GetProductAsync(Guid productId)
        {
            await Init();
            return await _databaseConnection.Table<ProductDBModel>().FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task AddProductAsync(ProductDBModel product)
        {
            await Init();
            await _databaseConnection.InsertAsync(product);
        }

        public async Task UpdateProductAsync(ProductDBModel product)
        {
            await Init();
            await _databaseConnection.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(Guid productId)
        {
            await Init();
            await _databaseConnection.DeleteAsync<ProductDBModel>(productId);
        }
    }
}