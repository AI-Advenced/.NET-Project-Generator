using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogAPI.Data;
using BlogAPI.Models;
using BlogAPI.Services;
using System.Linq;

namespace BlogAPI.Tests.Services
{
    [TestClass]
    public class CommentServiceTests
    {
        private BlogAPIContext _context;
        private CommentService _service;

        [TestInitialize]
        public void Setup()
        {
            // Use in-memory database for testing
            _context = new BlogAPIContext();
            _service = new CommentService(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context?.Dispose();
        }

        [TestMethod]
        public void GetAll_ReturnsAllComments()
        {
            // Arrange
            var comment1 = new Comment { /* Set properties */ };
            var comment2 = new Comment { /* Set properties */ };
            
            _context.Comments.Add(comment1);
            _context.Comments.Add(comment2);
            _context.SaveChanges();

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetById_ReturnsCorrectComment()
        {
            // Arrange
            var comment = new Comment { /* Set properties */ };
            _context.Comments.Add(comment);
            _context.SaveChanges();

            // Act
            var result = _service.GetById(comment.Id);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_AddsCommentSuccessfully()
        {
            // Arrange
            var comment = new Comment { /* Set properties */ };

            // Act
            var result = _service.Create(comment);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, _context.Comments.Count());
        }

        [TestMethod]
        public void Delete_RemovesCommentSuccessfully()
        {
            // Arrange
            var comment = new Comment { /* Set properties */ };
            _context.Comments.Add(comment);
            _context.SaveChanges();

            // Act
            var result = _service.Delete(comment.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _context.Comments.Count());
        }
    }
}