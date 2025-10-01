using System;
using System.Collections.Generic;
using System.Linq;
using BlogAPI.Data;
using BlogAPI.Models;

namespace BlogAPI.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly BlogAPIContext _context;

        public BlogPostService(BlogAPIContext context)
        {
            _context = context;
        }

        public IEnumerable<BlogPost> GetAll()
        {
            return _context.BlogPosts.ToList();
        }

        public BlogPost GetById(int id)
        {
            return _context.BlogPosts.Find(id);
        }

        public BlogPost Create(BlogPost blogpost)
        {
            _context.BlogPosts.Add(blogpost);
            _context.SaveChanges();
            return blogpost;
        }

        public BlogPost Update(BlogPost blogpost)
        {
            _context.Entry(blogpost).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return blogpost;
        }

        public bool Delete(int id)
        {
            var blogpost = _context.BlogPosts.Find(id);
            if (blogpost == null)
                return false;

            _context.BlogPosts.Remove(blogpost);
            _context.SaveChanges();
            return true;
        }
    }
}