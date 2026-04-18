using SQLite; 
using System;

namespace ProductManager.DBModels
{
    public class WarehouseDBModel
    {
        [PrimaryKey] 
        public Guid Id { get; set; }

        public string Name { get; set; }
        public Location Location { get; set; }

        // Порожній конструктор обов'язковий для SQLite
        public WarehouseDBModel() { }

        public WarehouseDBModel(string name, Location location)
        {
            Id = Guid.NewGuid();
            Name = name;
            Location = location;
        }
    }
}