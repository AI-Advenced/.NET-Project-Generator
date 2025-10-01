using System;
using System.Collections.Generic;
using ECommerceAPI.Models;

namespace ECommerceAPI.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAll();
        Order GetById(int id);
        Order Create(Order order);
        Order Update(Order order);
        bool Delete(int id);
    }
}