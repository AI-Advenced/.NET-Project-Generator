using System;
using System.Collections.Generic;
using System.Linq;
using BasicCrudAPI.Data;
using BasicCrudAPI.Models;

namespace BasicCrudAPI.Services
{
    public class UserService : IUserService
    {
        private readonly BasicCrudAPIContext _context;

        public UserService(BasicCrudAPIContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User Update(User user)
        {
            _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return user;
        }

        public bool Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }
    }
}