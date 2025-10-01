using System;
using System.Collections.Generic;
using System.Linq;
using ECommerceAPI.Data;
using ECommerceAPI.Models;

namespace ECommerceAPI.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ECommerceAPIContext _context;

        public ReviewService(ECommerceAPIContext context)
        {
            _context = context;
        }

        public IEnumerable<Review> GetAll()
        {
            return _context.Reviews.ToList();
        }

        public Review GetById(int id)
        {
            return _context.Reviews.Find(id);
        }

        public Review Create(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return review;
        }

        public Review Update(Review review)
        {
            _context.Entry(review).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return review;
        }

        public bool Delete(int id)
        {
            var review = _context.Reviews.Find(id);
            if (review == null)
                return false;

            _context.Reviews.Remove(review);
            _context.SaveChanges();
            return true;
        }
    }
}