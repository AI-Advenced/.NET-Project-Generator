using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryAPI.Data;
using InventoryAPI.Models;
using InventoryAPI.Services;
using System.Linq;

namespace InventoryAPI.Tests.Services
{
    [TestClass]
    public class ItemServiceTests
    {
        private InventoryAPIContext _context;
        private ItemService _service;

        [TestInitialize]
        public void Setup()
        {
            // Use in-memory database for testing
            _context = new InventoryAPIContext();
            _service = new ItemService(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context?.Dispose();
        }

        [TestMethod]
        public void GetAll_ReturnsAllItems()
        {
            // Arrange
            var item1 = new Item { /* Set properties */ };
            var item2 = new Item { /* Set properties */ };
            
            _context.Items.Add(item1);
            _context.Items.Add(item2);
            _context.SaveChanges();

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetById_ReturnsCorrectItem()
        {
            // Arrange
            var item = new Item { /* Set properties */ };
            _context.Items.Add(item);
            _context.SaveChanges();

            // Act
            var result = _service.GetById(item.Id);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_AddsItemSuccessfully()
        {
            // Arrange
            var item = new Item { /* Set properties */ };

            // Act
            var result = _service.Create(item);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, _context.Items.Count());
        }

        [TestMethod]
        public void Delete_RemovesItemSuccessfully()
        {
            // Arrange
            var item = new Item { /* Set properties */ };
            _context.Items.Add(item);
            _context.SaveChanges();

            // Act
            var result = _service.Delete(item.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _context.Items.Count());
        }
    }
}