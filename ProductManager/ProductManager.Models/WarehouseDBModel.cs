using System;

namespace ProductManager.DBModels
{
    public class WarehouseDBModel
    {
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }

        public WarehouseDBModel()
        {
        }

        public WarehouseDBModel(string name, Location location)
        {
            Id = Guid.NewGuid();
            Name = name;
            Location = location;
        }
    }
}