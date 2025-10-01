using System;
using System.Collections.Generic;
using System.Linq;
using InventoryAPI.Data;
using InventoryAPI.Models;

namespace InventoryAPI.Services
{
    public class ItemService : IItemService
    {
        private readonly InventoryAPIContext _context;

        public ItemService(InventoryAPIContext context)
        {
            _context = context;
        }

        public IEnumerable<Item> GetAll()
        {
            return _context.Items.ToList();
        }

        public Item GetById(int id)
        {
            return _context.Items.Find(id);
        }

        public Item Create(Item item)
        {
            _context.Items.Add(item);
            _context.SaveChanges();
            return item;
        }

        public Item Update(Item item)
        {
            _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public bool Delete(int id)
        {
            var item = _context.Items.Find(id);
            if (item == null)
                return false;

            _context.Items.Remove(item);
            _context.SaveChanges();
            return true;
        }
    }
}