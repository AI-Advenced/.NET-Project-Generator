using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Services;
using System.Linq;

namespace ECommerceAPI.Tests.Services
{
    [TestClass]
    public class OrderServiceTests
    {
        private ECommerceAPIContext _context;
        private OrderService _service;

        [TestInitialize]
        public void Setup()
        {
            // Use in-memory database for testing
            _context = new ECommerceAPIContext();
            _service = new OrderService(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context?.Dispose();
        }

        [TestMethod]
        public void GetAll_ReturnsAllOrders()
        {
            // Arrange
            var order1 = new Order { /* Set properties */ };
            var order2 = new Order { /* Set properties */ };
            
            _context.Orders.Add(order1);
            _context.Orders.Add(order2);
            _context.SaveChanges();

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetById_ReturnsCorrectOrder()
        {
            // Arrange
            var order = new Order { /* Set properties */ };
            _context.Orders.Add(order);
            _context.SaveChanges();

            // Act
            var result = _service.GetById(order.Id);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_AddsOrderSuccessfully()
        {
            // Arrange
            var order = new Order { /* Set properties */ };

            // Act
            var result = _service.Create(order);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, _context.Orders.Count());
        }

        [TestMethod]
        public void Delete_RemovesOrderSuccessfully()
        {
            // Arrange
            var order = new Order { /* Set properties */ };
            _context.Orders.Add(order);
            _context.SaveChanges();

            // Act
            var result = _service.Delete(order.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _context.Orders.Count());
        }
    }
}