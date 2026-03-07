using System;
using System.Collections.Generic;
using System.Linq;
using ProductManager.Models;

namespace ProductManager.Services
{
    public class StorageService
    {
        // Отримуємо всі склади
        public IReadOnlyList<Warehouse> GetAllWarehouses()
        {
            // Повертаємо копію списку, щоб уникнути прямого втручання в оригінальну колекцію
            return TempStorage.Warehouses.AsReadOnly();
        }

        // Отримуємо конкретний склад за його ідентифікатором
        public Warehouse GetWarehouseById(Guid id)
        {
            return TempStorage.Warehouses.FirstOrDefault(w => w.Id == id);
        }

        // Отримуємо всі товари, які належать до конкретного складу
        public IReadOnlyList<Product> GetProductsByWarehouseId(Guid warehouseId)
        {
            return TempStorage.Products.Where(p => p.WarehouseId == warehouseId).ToList().AsReadOnly();
        }

        // Отримуємо конкретний товар за його ідентифікатором
        public Product GetProductById(Guid id)
        {
            return TempStorage.Products.FirstOrDefault(p => p.Id == id);
        }
    }
}