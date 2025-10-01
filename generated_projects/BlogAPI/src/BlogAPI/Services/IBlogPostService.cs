using System;
using System.Collections.Generic;
using BlogAPI.Models;

namespace BlogAPI.Services
{
    public interface IBlogPostService
    {
        IEnumerable<BlogPost> GetAll();
        BlogPost GetById(int id);
        BlogPost Create(BlogPost blogpost);
        BlogPost Update(BlogPost blogpost);
        bool Delete(int id);
    }
}