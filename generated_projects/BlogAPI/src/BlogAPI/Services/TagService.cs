using System;
using System.Collections.Generic;
using System.Linq;
using BlogAPI.Data;
using BlogAPI.Models;

namespace BlogAPI.Services
{
    public class TagService : ITagService
    {
        private readonly BlogAPIContext _context;

        public TagService(BlogAPIContext context)
        {
            _context = context;
        }

        public IEnumerable<Tag> GetAll()
        {
            return _context.Tags.ToList();
        }

        public Tag GetById(int id)
        {
            return _context.Tags.Find(id);
        }

        public Tag Create(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return tag;
        }

        public Tag Update(Tag tag)
        {
            _context.Entry(tag).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return tag;
        }

        public bool Delete(int id)
        {
            var tag = _context.Tags.Find(id);
            if (tag == null)
                return false;

            _context.Tags.Remove(tag);
            _context.SaveChanges();
            return true;
        }
    }
}