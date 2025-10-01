using System;
using System.Collections.Generic;
using ECommerceAPI.Models;

namespace ECommerceAPI.Services
{
    public interface IReviewService
    {
        IEnumerable<Review> GetAll();
        Review GetById(int id);
        Review Create(Review review);
        Review Update(Review review);
        bool Delete(int id);
    }
}