using System;
using System.Collections.Generic;
using System.Linq;
using InventoryAPI.Data;
using InventoryAPI.Models;

namespace InventoryAPI.Services
{
    public class StockService : IStockService
    {
        private readonly InventoryAPIContext _context;

        public StockService(InventoryAPIContext context)
        {
            _context = context;
        }

        public IEnumerable<Stock> GetAll()
        {
            return _context.Stocks.ToList();
        }

        public Stock GetById(int id)
        {
            return _context.Stocks.Find(id);
        }

        public Stock Create(Stock stock)
        {
            _context.Stocks.Add(stock);
            _context.SaveChanges();
            return stock;
        }

        public Stock Update(Stock stock)
        {
            _context.Entry(stock).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return stock;
        }

        public bool Delete(int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock == null)
                return false;

            _context.Stocks.Remove(stock);
            _context.SaveChanges();
            return true;
        }
    }
}