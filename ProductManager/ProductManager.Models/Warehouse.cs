using System;

namespace ProductManager.Models
{
    public class Warehouse
    {
        
        public Guid Id { get; }
        public string Name { get; set; }
        public Location Location { get; set; }

        public Warehouse(string name, Location location)
        {
            Id = Guid.NewGuid();
            Name = name;
            Location = location;
        }
    }
}