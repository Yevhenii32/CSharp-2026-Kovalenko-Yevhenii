using System;
using System.Collections.Generic;
using ProductManager.Models;

namespace ProductManager.Services
{
    internal static class TempStorage
    {
        public static List<Warehouse> Warehouses { get; private set; } = new List<Warehouse>();
        public static List<Product> Products { get; private set; } = new List<Product>();

        static TempStorage()
        {
            InitializeData();
        }

        private static void InitializeData()
        {
            // Створюємо 3 склади
            var warehouse1 = new Warehouse(Guid.NewGuid(), "Центральний склад", Location.Kyiv);
            var warehouse2 = new Warehouse(Guid.NewGuid(), "Західний склад", Location.Lviv);
            var warehouse3 = new Warehouse(Guid.NewGuid(), "Південний резервний склад", Location.Odesa);

            Warehouses.Add(warehouse1);
            Warehouses.Add(warehouse2);
            Warehouses.Add(warehouse3);

            // Створюємо 12 товарів 

            // 10 товарів для Центрального складу 
            Products.Add(new Product(Guid.NewGuid(), warehouse1.Id, "Ноутбук Lenovo Legion", 15, 45000m, ProductCategory.Electronics, "Ігровий ноутбук 15.6 дюймів"));
            Products.Add(new Product(Guid.NewGuid(), warehouse1.Id, "Мишка Logitech G Pro", 30, 4500m, ProductCategory.Electronics, "Бездротова ігрова миша"));
            Products.Add(new Product(Guid.NewGuid(), warehouse1.Id, "Клавіатура Razer BlackWidow", 20, 5000m, ProductCategory.Electronics, "Механічна клавіатура"));
            Products.Add(new Product(Guid.NewGuid(), warehouse1.Id, "Футболка базового крою", 50, 800m, ProductCategory.Clothing, "Чорна базова футболка з бавовни, розмір L"));
            Products.Add(new Product(Guid.NewGuid(), warehouse1.Id, "Джинси Levi's 501", 40, 2500m, ProductCategory.Clothing, "Класичні сині джинси прямого крою"));
            Products.Add(new Product(Guid.NewGuid(), warehouse1.Id, "Кава Lavazza 1кг", 100, 600m, ProductCategory.Groceries, "Кава в зернах середнього обсмаження"));
            Products.Add(new Product(Guid.NewGuid(), warehouse1.Id, "Чай Greenfield", 150, 120m, ProductCategory.Groceries, "Чорний чай, 100 пакетиків"));
            Products.Add(new Product(Guid.NewGuid(), warehouse1.Id, "Офісне крісло GT Racer", 10, 3500m, ProductCategory.Furniture, "Ергономічне крісло чорного кольору"));
            Products.Add(new Product(Guid.NewGuid(), warehouse1.Id, "Стіл комп'ютерний", 5, 4000m, ProductCategory.Furniture, "Стіл з ДСП 120х60 см"));
            Products.Add(new Product(Guid.NewGuid(), warehouse1.Id, "Набір викруток Bosch", 25, 1200m, ProductCategory.Tools, "Набір інструментів з 46 предметів"));

            // 2 товари для Західного філіалу
            Products.Add(new Product(Guid.NewGuid(), warehouse2.Id, "Монітор Dell 27\"", 8, 9000m, ProductCategory.Electronics, "IPS панель, 144Hz"));
            Products.Add(new Product(Guid.NewGuid(), warehouse2.Id, "Шуруповерт Makita", 12, 3200m, ProductCategory.Tools, "Акумуляторний шуруповерт 18V"));

            // Третій резервний склад залишається порожнім.
        }
    }
}