using System;
using System.Collections.Generic;
using InventoryAPI.Models;

namespace InventoryAPI.Services
{
    public interface ISupplierService
    {
        IEnumerable<Supplier> GetAll();
        Supplier GetById(int id);
        Supplier Create(Supplier supplier);
        Supplier Update(Supplier supplier);
        bool Delete(int id);
    }
}