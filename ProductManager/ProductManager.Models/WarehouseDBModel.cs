using System;

namespace ProductManager.DBModels
{
    public class WarehouseDBModel
    {
        
        public Guid Id { get; }
        public string Name { get; set; }
        public Location Location { get; set; }

        public WarehouseDBModel(string name, Location location)
        {
            Id = Guid.NewGuid();
            Name = name;
            Location = location;
        }
    }
}