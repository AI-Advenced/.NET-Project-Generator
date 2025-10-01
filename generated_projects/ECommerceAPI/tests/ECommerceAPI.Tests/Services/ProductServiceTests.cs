using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Services;
using System.Linq;

namespace ECommerceAPI.Tests.Services
{
    [TestClass]
    public class ProductServiceTests
    {
        private ECommerceAPIContext _context;
        private ProductService _service;

        [TestInitialize]
        public void Setup()
        {
            // Use in-memory database for testing
            _context = new ECommerceAPIContext();
            _service = new ProductService(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context?.Dispose();
        }

        [TestMethod]
        public void GetAll_ReturnsAllProducts()
        {
            // Arrange
            var product1 = new Product { /* Set properties */ };
            var product2 = new Product { /* Set properties */ };
            
            _context.Products.Add(product1);
            _context.Products.Add(product2);
            _context.SaveChanges();

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetById_ReturnsCorrectProduct()
        {
            // Arrange
            var product = new Product { /* Set properties */ };
            _context.Products.Add(product);
            _context.SaveChanges();

            // Act
            var result = _service.GetById(product.Id);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_AddsProductSuccessfully()
        {
            // Arrange
            var product = new Product { /* Set properties */ };

            // Act
            var result = _service.Create(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, _context.Products.Count());
        }

        [TestMethod]
        public void Delete_RemovesProductSuccessfully()
        {
            // Arrange
            var product = new Product { /* Set properties */ };
            _context.Products.Add(product);
            _context.SaveChanges();

            // Act
            var result = _service.Delete(product.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _context.Products.Count());
        }
    }
}