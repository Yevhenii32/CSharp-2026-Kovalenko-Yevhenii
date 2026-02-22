using System;
using System.Collections.Generic;
using System.Linq;
using ProductManager.Models;

namespace ProductManager.ViewModels
{
    public class WarehouseViewModel
    {
        private readonly Warehouse _warehouse;

        // Колекція товарів для відображення, що належать цьому складу
        public List<ProductViewModel> Products { get; set; }

        // Конструктор приймає модель складу та список моделей відображення товарів
        public WarehouseViewModel(Warehouse warehouse, List<ProductViewModel> products)
        {
            _warehouse = warehouse;
            Products = products;
        }

        public Guid Id => _warehouse.Id;
        public string Name => _warehouse.Name;
        public Location Location => _warehouse.Location;

        // Обчислюване поле: загальна вартість усіх товарів на складі 
        public decimal TotalWarehouseValue => Products.Sum(p => p.TotalValue);
    }
}