using System;
using System.Collections.Generic;
using BlogAPI.Models;

namespace BlogAPI.Services
{
    public interface IAuthorService
    {
        IEnumerable<Author> GetAll();
        Author GetById(int id);
        Author Create(Author author);
        Author Update(Author author);
        bool Delete(int id);
    }
}