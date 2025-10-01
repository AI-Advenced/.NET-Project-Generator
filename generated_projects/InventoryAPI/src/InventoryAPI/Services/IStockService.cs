using System;
using System.Collections.Generic;
using InventoryAPI.Models;

namespace InventoryAPI.Services
{
    public interface IStockService
    {
        IEnumerable<Stock> GetAll();
        Stock GetById(int id);
        Stock Create(Stock stock);
        Stock Update(Stock stock);
        bool Delete(int id);
    }
}