using System;
using System.Collections.Generic;
using System.Linq;
using InventoryAPI.Data;
using InventoryAPI.Models;

namespace InventoryAPI.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly InventoryAPIContext _context;

        public SupplierService(InventoryAPIContext context)
        {
            _context = context;
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _context.Suppliers.ToList();
        }

        public Supplier GetById(int id)
        {
            return _context.Suppliers.Find(id);
        }

        public Supplier Create(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
            return supplier;
        }

        public Supplier Update(Supplier supplier)
        {
            _context.Entry(supplier).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return supplier;
        }

        public bool Delete(int id)
        {
            var supplier = _context.Suppliers.Find(id);
            if (supplier == null)
                return false;

            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
            return true;
        }
    }
}