using System;
using System.Collections.Generic;
using System.Linq;
using BlogAPI.Data;
using BlogAPI.Models;

namespace BlogAPI.Services
{
    public class CommentService : ICommentService
    {
        private readonly BlogAPIContext _context;

        public CommentService(BlogAPIContext context)
        {
            _context = context;
        }

        public IEnumerable<Comment> GetAll()
        {
            return _context.Comments.ToList();
        }

        public Comment GetById(int id)
        {
            return _context.Comments.Find(id);
        }

        public Comment Create(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return comment;
        }

        public Comment Update(Comment comment)
        {
            _context.Entry(comment).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return comment;
        }

        public bool Delete(int id)
        {
            var comment = _context.Comments.Find(id);
            if (comment == null)
                return false;

            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return true;
        }
    }
}