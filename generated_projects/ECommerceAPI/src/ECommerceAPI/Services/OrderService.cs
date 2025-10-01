using System;
using System.Collections.Generic;
using System.Linq;
using ECommerceAPI.Data;
using ECommerceAPI.Models;

namespace ECommerceAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly ECommerceAPIContext _context;

        public OrderService(ECommerceAPIContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public Order GetById(int id)
        {
            return _context.Orders.Find(id);
        }

        public Order Create(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }

        public Order Update(Order order)
        {
            _context.Entry(order).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return order;
        }

        public bool Delete(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
                return false;

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return true;
        }
    }
}