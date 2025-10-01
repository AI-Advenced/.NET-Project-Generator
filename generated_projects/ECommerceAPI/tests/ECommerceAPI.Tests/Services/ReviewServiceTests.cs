using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Services;
using System.Linq;

namespace ECommerceAPI.Tests.Services
{
    [TestClass]
    public class ReviewServiceTests
    {
        private ECommerceAPIContext _context;
        private ReviewService _service;

        [TestInitialize]
        public void Setup()
        {
            // Use in-memory database for testing
            _context = new ECommerceAPIContext();
            _service = new ReviewService(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context?.Dispose();
        }

        [TestMethod]
        public void GetAll_ReturnsAllReviews()
        {
            // Arrange
            var review1 = new Review { /* Set properties */ };
            var review2 = new Review { /* Set properties */ };
            
            _context.Reviews.Add(review1);
            _context.Reviews.Add(review2);
            _context.SaveChanges();

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetById_ReturnsCorrectReview()
        {
            // Arrange
            var review = new Review { /* Set properties */ };
            _context.Reviews.Add(review);
            _context.SaveChanges();

            // Act
            var result = _service.GetById(review.Id);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_AddsReviewSuccessfully()
        {
            // Arrange
            var review = new Review { /* Set properties */ };

            // Act
            var result = _service.Create(review);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, _context.Reviews.Count());
        }

        [TestMethod]
        public void Delete_RemovesReviewSuccessfully()
        {
            // Arrange
            var review = new Review { /* Set properties */ };
            _context.Reviews.Add(review);
            _context.SaveChanges();

            // Act
            var result = _service.Delete(review.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _context.Reviews.Count());
        }
    }
}