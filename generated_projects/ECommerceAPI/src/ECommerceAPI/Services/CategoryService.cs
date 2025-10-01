using System;
using System.Collections.Generic;
using System.Linq;
using ECommerceAPI.Data;
using ECommerceAPI.Models;

namespace ECommerceAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ECommerceAPIContext _context;

        public CategoryService(ECommerceAPIContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categorys.ToList();
        }

        public Category GetById(int id)
        {
            return _context.Categorys.Find(id);
        }

        public Category Create(Category category)
        {
            _context.Categorys.Add(category);
            _context.SaveChanges();
            return category;
        }

        public Category Update(Category category)
        {
            _context.Entry(category).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return category;
        }

        public bool Delete(int id)
        {
            var category = _context.Categorys.Find(id);
            if (category == null)
                return false;

            _context.Categorys.Remove(category);
            _context.SaveChanges();
            return true;
        }
    }
}