using System;
using System.Collections.Generic;
using System.Linq;
using BlogAPI.Data;
using BlogAPI.Models;

namespace BlogAPI.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly BlogAPIContext _context;

        public AuthorService(BlogAPIContext context)
        {
            _context = context;
        }

        public IEnumerable<Author> GetAll()
        {
            return _context.Authors.ToList();
        }

        public Author GetById(int id)
        {
            return _context.Authors.Find(id);
        }

        public Author Create(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
            return author;
        }

        public Author Update(Author author)
        {
            _context.Entry(author).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return author;
        }

        public bool Delete(int id)
        {
            var author = _context.Authors.Find(id);
            if (author == null)
                return false;

            _context.Authors.Remove(author);
            _context.SaveChanges();
            return true;
        }
    }
}