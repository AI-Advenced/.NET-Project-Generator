using System;
using System.Collections.Generic;
using InventoryAPI.Models;

namespace InventoryAPI.Services
{
    public interface IWarehouseService
    {
        IEnumerable<Warehouse> GetAll();
        Warehouse GetById(int id);
        Warehouse Create(Warehouse warehouse);
        Warehouse Update(Warehouse warehouse);
        bool Delete(int id);
    }
}