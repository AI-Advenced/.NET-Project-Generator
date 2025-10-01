using System;
using System.Collections.Generic;
using BasicCrudAPI.Models;

namespace BasicCrudAPI.Services
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