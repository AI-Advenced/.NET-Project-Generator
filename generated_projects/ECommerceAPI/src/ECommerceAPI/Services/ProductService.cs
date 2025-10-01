using System;
using System.Collections.Generic;
using System.Linq;
using ECommerceAPI.Data;
using ECommerceAPI.Models;

namespace ECommerceAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly ECommerceAPIContext _context;

        public ProductService(ECommerceAPIContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product GetById(int id)
        {
            return _context.Products.Find(id);
        }

        public Product Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public Product Update(Product product)
        {
            _context.Entry(product).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return product;
        }

        public bool Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }
    }
}