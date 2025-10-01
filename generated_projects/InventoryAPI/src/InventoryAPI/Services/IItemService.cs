using System;
using System.Collections.Generic;
using InventoryAPI.Models;

namespace InventoryAPI.Services
{
    public interface IItemService
    {
        IEnumerable<Item> GetAll();
        Item GetById(int id);
        Item Create(Item item);
        Item Update(Item item);
        bool Delete(int id);
    }
}