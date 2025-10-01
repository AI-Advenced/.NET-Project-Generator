using System;
using System.Collections.Generic;
using ECommerceAPI.Models;

namespace ECommerceAPI.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        Product Create(Product product);
        Product Update(Product product);
        bool Delete(int id);
    }
}