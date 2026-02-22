using System;

namespace ProductManager.Models
{
    public class Warehouse
    {
        
        public Guid Id { get; }
        public string Name { get; set; }
        public Location Location { get; set; }

        public Warehouse(Guid id, string name, Location location)
        {
            Id = id;
            Name = name;
            Location = location;
        }
    }
}