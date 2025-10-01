using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Services;
using System.Linq;

namespace ECommerceAPI.Tests.Services
{
    [TestClass]
    public class CategoryServiceTests
    {
        private ECommerceAPIContext _context;
        private CategoryService _service;

        [TestInitialize]
        public void Setup()
        {
            // Use in-memory database for testing
            _context = new ECommerceAPIContext();
            _service = new CategoryService(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context?.Dispose();
        }

        [TestMethod]
        public void GetAll_ReturnsAllCategorys()
        {
            // Arrange
            var category1 = new Category { /* Set properties */ };
            var category2 = new Category { /* Set properties */ };
            
            _context.Categorys.Add(category1);
            _context.Categorys.Add(category2);
            _context.SaveChanges();

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetById_ReturnsCorrectCategory()
        {
            // Arrange
            var category = new Category { /* Set properties */ };
            _context.Categorys.Add(category);
            _context.SaveChanges();

            // Act
            var result = _service.GetById(category.Id);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_AddsCategorySuccessfully()
        {
            // Arrange
            var category = new Category { /* Set properties */ };

            // Act
            var result = _service.Create(category);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, _context.Categorys.Count());
        }

        [TestMethod]
        public void Delete_RemovesCategorySuccessfully()
        {
            // Arrange
            var category = new Category { /* Set properties */ };
            _context.Categorys.Add(category);
            _context.SaveChanges();

            // Act
            var result = _service.Delete(category.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _context.Categorys.Count());
        }
    }
}