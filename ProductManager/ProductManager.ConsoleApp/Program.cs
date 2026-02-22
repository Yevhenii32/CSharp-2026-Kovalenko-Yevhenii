using System;
using System.Collections.Generic;
using System.Linq;
using ProductManager.Services;
using ProductManager.ViewModels;
using ProductManager.Models;

namespace ProductManager.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            // Налаштовуємо кодування для правильного відображення кирилиці
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            // Ініціалізуємо сервіс для роботи зі сховищем
            StorageService storageService = new StorageService();
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("*** Менеджер товарів ***");

                // Отримуємо колекцію сутностей першого рівня через сервіс
                var warehouses = storageService.GetAllWarehouses();
                var warehouseViewModels = new List<WarehouseViewModel>();

                // Формуємо об'єкти класів відображення 
                foreach (var w in warehouses)
                {
                    // Завантажуємо сутності другого рівня для даного екземпляра
                    var products = storageService.GetProductsByWarehouseId(w.Id);

                    // Перетворюємо моделі товарів на моделі відображення
                    var productViewModels = products.Select(p => new ProductViewModel(p)).ToList();

                    warehouseViewModels.Add(new WarehouseViewModel(w, productViewModels));
                }

                // Виводимо список екземплярів складів
                Console.WriteLine("\nДоступні склади:");
                for (int i = 0; i < warehouseViewModels.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {warehouseViewModels[i].Name} ({warehouseViewModels[i].Location})");
                }
                Console.WriteLine("0. Штатно завершити роботу застосунку");

                Console.Write("\nОберіть склад (введіть номер) або 0 для виходу: ");
                string input = Console.ReadLine();

                if (input == "0")
                {
                    // Штатне завершення роботи
                    isRunning = false;
                    continue;
                }

                // Обробка вибору користувача
                if (int.TryParse(input, out int selectedIndex) && selectedIndex > 0 && selectedIndex <= warehouseViewModels.Count)
                {
                    var selectedWarehouse = warehouseViewModels[selectedIndex - 1];
                    ShowWarehouseDetails(selectedWarehouse);
                }
                else
                {
                    Console.WriteLine("Некоректний ввід. Натисніть будь-яку клавішу для продовження...");
                    Console.ReadKey();
                }
            }
        }

        // Метод для відображення деталей складу та списку його товарів
        static void ShowWarehouseDetails(WarehouseViewModel warehouse)
        {
            bool inWarehouseMenu = true;
            while (inWarehouseMenu)
            {
                Console.Clear();
                Console.WriteLine($"*** Деталі складу: {warehouse.Name} ***");
                Console.WriteLine($"Місцезнаходження: {warehouse.Location}");

                // Використання обчислюваного поля
                Console.WriteLine($"Загальна вартість товарів на складі: {warehouse.TotalWarehouseValue} грн");
                Console.WriteLine("\nТовари на складі:");

                if (warehouse.Products.Count == 0)
                {
                    Console.WriteLine("Цей склад наразі порожній.");
                }
                else
                {
                    // Відображаємо всі екземпляри сутності другого рівня
                    for (int i = 0; i < warehouse.Products.Count; i++)
                    {
                        var p = warehouse.Products[i];
                        Console.WriteLine($"{i + 1}. {p.Name} - Ціна: {p.Price} грн | Кількість: {p.Quantity} шт.");
                    }
                }

                Console.WriteLine("\nВведіть номер товару для перегляду повної інформації або 0, щоб повернутися до списку складів:");
                string input = Console.ReadLine();

                if (input == "0")
                {
                    inWarehouseMenu = false; // Повернення на попередній крок
                }
                else if (int.TryParse(input, out int productIndex) && productIndex > 0 && productIndex <= warehouse.Products.Count)
                {
                    ShowProductDetails(warehouse.Products[productIndex - 1]);
                }
                else
                {
                    Console.WriteLine("Некоректний ввід. Натисніть будь-яку клавішу для продовження...");
                    Console.ReadKey();
                }
            }
        }

        // Метод для відображення повної інформації по конкретній сутності
        static void ShowProductDetails(ProductViewModel product)
        {
            Console.Clear();
            Console.WriteLine($"*** Детальна інформація про товар: {product.Name} ***");
            Console.WriteLine($"Категорія: {product.Category}");
            Console.WriteLine($"Опис: {product.Description}");
            Console.WriteLine($"Ціна за одиницю: {product.Price} грн");
            Console.WriteLine($"Кількість на складі: {product.Quantity} шт.");

            // Використання обчислюваного поля товару
            Console.WriteLine($"Загальна вартість партії: {product.TotalValue} грн");

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися до складу...");
            Console.ReadKey();
        }
    }
}