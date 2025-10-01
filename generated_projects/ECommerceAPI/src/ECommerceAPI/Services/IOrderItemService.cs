using System;
using System.Collections.Generic;
using ECommerceAPI.Models;

namespace ECommerceAPI.Services
{
    public interface IOrderItemService
    {
        IEnumerable<OrderItem> GetAll();
        OrderItem GetById(int id);
        OrderItem Create(OrderItem orderitem);
        OrderItem Update(OrderItem orderitem);
        bool Delete(int id);
    }
}