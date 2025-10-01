using System;
using System.Collections.Generic;
using BlogAPI.Models;

namespace BlogAPI.Services
{
    public interface ICommentService
    {
        IEnumerable<Comment> GetAll();
        Comment GetById(int id);
        Comment Create(Comment comment);
        Comment Update(Comment comment);
        bool Delete(int id);
    }
}