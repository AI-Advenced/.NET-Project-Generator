using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryAPI.Data;
using InventoryAPI.Models;
using InventoryAPI.Services;
using System.Linq;

namespace InventoryAPI.Tests.Services
{
    [TestClass]
    public class WarehouseServiceTests
    {
        private InventoryAPIContext _context;
        private WarehouseService _service;

        [TestInitialize]
        public void Setup()
        {
            // Use in-memory database for testing
            _context = new InventoryAPIContext();
            _service = new WarehouseService(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context?.Dispose();
        }

        [TestMethod]
        public void GetAll_ReturnsAllWarehouses()
        {
            // Arrange
            var warehouse1 = new Warehouse { /* Set properties */ };
            var warehouse2 = new Warehouse { /* Set properties */ };
            
            _context.Warehouses.Add(warehouse1);
            _context.Warehouses.Add(warehouse2);
            _context.SaveChanges();

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetById_ReturnsCorrectWarehouse()
        {
            // Arrange
            var warehouse = new Warehouse { /* Set properties */ };
            _context.Warehouses.Add(warehouse);
            _context.SaveChanges();

            // Act
            var result = _service.GetById(warehouse.Id);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_AddsWarehouseSuccessfully()
        {
            // Arrange
            var warehouse = new Warehouse { /* Set properties */ };

            // Act
            var result = _service.Create(warehouse);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, _context.Warehouses.Count());
        }

        [TestMethod]
        public void Delete_RemovesWarehouseSuccessfully()
        {
            // Arrange
            var warehouse = new Warehouse { /* Set properties */ };
            _context.Warehouses.Add(warehouse);
            _context.SaveChanges();

            // Act
            var result = _service.Delete(warehouse.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _context.Warehouses.Count());
        }
    }
}