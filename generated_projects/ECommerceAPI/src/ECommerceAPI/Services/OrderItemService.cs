using System;
using System.Collections.Generic;
using System.Linq;
using ECommerceAPI.Data;
using ECommerceAPI.Models;

namespace ECommerceAPI.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly ECommerceAPIContext _context;

        public OrderItemService(ECommerceAPIContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderItem> GetAll()
        {
            return _context.OrderItems.ToList();
        }

        public OrderItem GetById(int id)
        {
            return _context.OrderItems.Find(id);
        }

        public OrderItem Create(OrderItem orderitem)
        {
            _context.OrderItems.Add(orderitem);
            _context.SaveChanges();
            return orderitem;
        }

        public OrderItem Update(OrderItem orderitem)
        {
            _context.Entry(orderitem).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return orderitem;
        }

        public bool Delete(int id)
        {
            var orderitem = _context.OrderItems.Find(id);
            if (orderitem == null)
                return false;

            _context.OrderItems.Remove(orderitem);
            _context.SaveChanges();
            return true;
        }
    }
}