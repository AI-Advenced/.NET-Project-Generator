using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogAPI.Data;
using BlogAPI.Models;
using BlogAPI.Services;
using System.Linq;

namespace BlogAPI.Tests.Services
{
    [TestClass]
    public class AuthorServiceTests
    {
        private BlogAPIContext _context;
        private AuthorService _service;

        [TestInitialize]
        public void Setup()
        {
            // Use in-memory database for testing
            _context = new BlogAPIContext();
            _service = new AuthorService(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context?.Dispose();
        }

        [TestMethod]
        public void GetAll_ReturnsAllAuthors()
        {
            // Arrange
            var author1 = new Author { /* Set properties */ };
            var author2 = new Author { /* Set properties */ };
            
            _context.Authors.Add(author1);
            _context.Authors.Add(author2);
            _context.SaveChanges();

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetById_ReturnsCorrectAuthor()
        {
            // Arrange
            var author = new Author { /* Set properties */ };
            _context.Authors.Add(author);
            _context.SaveChanges();

            // Act
            var result = _service.GetById(author.Id);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_AddsAuthorSuccessfully()
        {
            // Arrange
            var author = new Author { /* Set properties */ };

            // Act
            var result = _service.Create(author);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, _context.Authors.Count());
        }

        [TestMethod]
        public void Delete_RemovesAuthorSuccessfully()
        {
            // Arrange
            var author = new Author { /* Set properties */ };
            _context.Authors.Add(author);
            _context.SaveChanges();

            // Act
            var result = _service.Delete(author.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _context.Authors.Count());
        }
    }
}