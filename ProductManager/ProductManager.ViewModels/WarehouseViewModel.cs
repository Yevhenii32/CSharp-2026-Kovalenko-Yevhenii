using System;
using System.Collections.Generic;
using System.Linq;
using ProductManager.Models;

namespace ProductManager.ViewModels
{
    public class WarehouseViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }

        // Переклад локації для інтерфейсу 
        public string LocationName => Location switch
        {
            Location.Kyiv => "Київ",
            Location.Lviv => "Львів",
            Location.Kharkiv => "Харків",
            Location.Odesa => "Одеса",
            Location.Chernihiv => "Чернігів",
            _ => "Невідомо"
        };
        public IReadOnlyList<ProductViewModel> Products { get; }
        // Обчислення загальна вартість усіх товарів на складі 
        public decimal TotalWarehouseValue => Products.Sum(p => p.TotalValue);

        // Конструктор для створення нового складу
        public WarehouseViewModel()
        {
            Products = new List<ProductViewModel>().AsReadOnly();
        }
        // Конструктор приймає модель складу та список моделей відображення товарів
        public WarehouseViewModel(Warehouse warehouse, IEnumerable<ProductViewModel> products)
        {
            Id = warehouse.Id;
            Name = warehouse.Name;
            Location = warehouse.Location;
            Products = products.ToList().AsReadOnly();
        }
    }
}