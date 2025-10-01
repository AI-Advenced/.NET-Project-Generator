using System;
using System.Collections.Generic;
using ECommerceAPI.Models;

namespace ECommerceAPI.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user);
        User Update(User user);
        bool Delete(int id);
    }
}