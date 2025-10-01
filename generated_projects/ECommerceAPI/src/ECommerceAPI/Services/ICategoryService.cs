using System;
using System.Collections.Generic;
using ECommerceAPI.Models;

namespace ECommerceAPI.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        Category Create(Category category);
        Category Update(Category category);
        bool Delete(int id);
    }
}