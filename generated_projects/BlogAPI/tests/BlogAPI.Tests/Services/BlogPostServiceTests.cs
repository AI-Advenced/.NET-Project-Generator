using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogAPI.Data;
using BlogAPI.Models;
using BlogAPI.Services;
using System.Linq;

namespace BlogAPI.Tests.Services
{
    [TestClass]
    public class BlogPostServiceTests
    {
        private BlogAPIContext _context;
        private BlogPostService _service;

        [TestInitialize]
        public void Setup()
        {
            // Use in-memory database for testing
            _context = new BlogAPIContext();
            _service = new BlogPostService(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context?.Dispose();
        }

        [TestMethod]
        public void GetAll_ReturnsAllBlogPosts()
        {
            // Arrange
            var blogpost1 = new BlogPost { /* Set properties */ };
            var blogpost2 = new BlogPost { /* Set properties */ };
            
            _context.BlogPosts.Add(blogpost1);
            _context.BlogPosts.Add(blogpost2);
            _context.SaveChanges();

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetById_ReturnsCorrectBlogPost()
        {
            // Arrange
            var blogpost = new BlogPost { /* Set properties */ };
            _context.BlogPosts.Add(blogpost);
            _context.SaveChanges();

            // Act
            var result = _service.GetById(blogpost.Id);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_AddsBlogPostSuccessfully()
        {
            // Arrange
            var blogpost = new BlogPost { /* Set properties */ };

            // Act
            var result = _service.Create(blogpost);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, _context.BlogPosts.Count());
        }

        [TestMethod]
        public void Delete_RemovesBlogPostSuccessfully()
        {
            // Arrange
            var blogpost = new BlogPost { /* Set properties */ };
            _context.BlogPosts.Add(blogpost);
            _context.SaveChanges();

            // Act
            var result = _service.Delete(blogpost.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _context.BlogPosts.Count());
        }
    }
}