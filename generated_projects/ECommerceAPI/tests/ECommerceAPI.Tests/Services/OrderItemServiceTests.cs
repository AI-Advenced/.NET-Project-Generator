using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Services;
using System.Linq;

namespace ECommerceAPI.Tests.Services
{
    [TestClass]
    public class OrderItemServiceTests
    {
        private ECommerceAPIContext _context;
        private OrderItemService _service;

        [TestInitialize]
        public void Setup()
        {
            // Use in-memory database for testing
            _context = new ECommerceAPIContext();
            _service = new OrderItemService(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context?.Dispose();
        }

        [TestMethod]
        public void GetAll_ReturnsAllOrderItems()
        {
            // Arrange
            var orderitem1 = new OrderItem { /* Set properties */ };
            var orderitem2 = new OrderItem { /* Set properties */ };
            
            _context.OrderItems.Add(orderitem1);
            _context.OrderItems.Add(orderitem2);
            _context.SaveChanges();

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetById_ReturnsCorrectOrderItem()
        {
            // Arrange
            var orderitem = new OrderItem { /* Set properties */ };
            _context.OrderItems.Add(orderitem);
            _context.SaveChanges();

            // Act
            var result = _service.GetById(orderitem.Id);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_AddsOrderItemSuccessfully()
        {
            // Arrange
            var orderitem = new OrderItem { /* Set properties */ };

            // Act
            var result = _service.Create(orderitem);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, _context.OrderItems.Count());
        }

        [TestMethod]
        public void Delete_RemovesOrderItemSuccessfully()
        {
            // Arrange
            var orderitem = new OrderItem { /* Set properties */ };
            _context.OrderItems.Add(orderitem);
            _context.SaveChanges();

            // Act
            var result = _service.Delete(orderitem.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _context.OrderItems.Count());
        }
    }
}