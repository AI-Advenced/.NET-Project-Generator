using System;
using System.Collections.Generic;
using System.Linq;
using InventoryAPI.Data;
using InventoryAPI.Models;

namespace InventoryAPI.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly InventoryAPIContext _context;

        public WarehouseService(InventoryAPIContext context)
        {
            _context = context;
        }

        public IEnumerable<Warehouse> GetAll()
        {
            return _context.Warehouses.ToList();
        }

        public Warehouse GetById(int id)
        {
            return _context.Warehouses.Find(id);
        }

        public Warehouse Create(Warehouse warehouse)
        {
            _context.Warehouses.Add(warehouse);
            _context.SaveChanges();
            return warehouse;
        }

        public Warehouse Update(Warehouse warehouse)
        {
            _context.Entry(warehouse).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return warehouse;
        }

        public bool Delete(int id)
        {
            var warehouse = _context.Warehouses.Find(id);
            if (warehouse == null)
                return false;

            _context.Warehouses.Remove(warehouse);
            _context.SaveChanges();
            return true;
        }
    }
}