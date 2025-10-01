using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogAPI.Data;
using BlogAPI.Models;
using BlogAPI.Services;
using System.Linq;

namespace BlogAPI.Tests.Services
{
    [TestClass]
    public class TagServiceTests
    {
        private BlogAPIContext _context;
        private TagService _service;

        [TestInitialize]
        public void Setup()
        {
            // Use in-memory database for testing
            _context = new BlogAPIContext();
            _service = new TagService(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context?.Dispose();
        }

        [TestMethod]
        public void GetAll_ReturnsAllTags()
        {
            // Arrange
            var tag1 = new Tag { /* Set properties */ };
            var tag2 = new Tag { /* Set properties */ };
            
            _context.Tags.Add(tag1);
            _context.Tags.Add(tag2);
            _context.SaveChanges();

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetById_ReturnsCorrectTag()
        {
            // Arrange
            var tag = new Tag { /* Set properties */ };
            _context.Tags.Add(tag);
            _context.SaveChanges();

            // Act
            var result = _service.GetById(tag.Id);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_AddsTagSuccessfully()
        {
            // Arrange
            var tag = new Tag { /* Set properties */ };

            // Act
            var result = _service.Create(tag);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, _context.Tags.Count());
        }

        [TestMethod]
        public void Delete_RemovesTagSuccessfully()
        {
            // Arrange
            var tag = new Tag { /* Set properties */ };
            _context.Tags.Add(tag);
            _context.SaveChanges();

            // Act
            var result = _service.Delete(tag.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _context.Tags.Count());
        }
    }
}