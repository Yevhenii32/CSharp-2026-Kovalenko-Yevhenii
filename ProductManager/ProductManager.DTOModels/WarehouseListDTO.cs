using System;
using ProductManager.DBModels; 

namespace ProductManager.DTOModels.Warehouse
{
    public class WarehouseListDTO
    {
        public Guid Id { get; }
        public string Name { get; }
        public Location Location { get; }

        public WarehouseListDTO(Guid id, string name, Location location)
        {
            Id = id;
            Name = name;
            Location = location;
        }
    }
}