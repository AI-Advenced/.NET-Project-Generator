using System;
using System.Collections.Generic;
using BlogAPI.Models;

namespace BlogAPI.Services
{
    public interface ITagService
    {
        IEnumerable<Tag> GetAll();
        Tag GetById(int id);
        Tag Create(Tag tag);
        Tag Update(Tag tag);
        bool Delete(int id);
    }
}