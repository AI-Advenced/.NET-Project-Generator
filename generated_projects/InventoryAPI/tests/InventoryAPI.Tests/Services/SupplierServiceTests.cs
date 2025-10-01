using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryAPI.Data;
using InventoryAPI.Models;
using InventoryAPI.Services;
using System.Linq;

namespace InventoryAPI.Tests.Services
{
    [TestClass]
    public class SupplierServiceTests
    {
        private InventoryAPIContext _context;
        private SupplierService _service;

        [TestInitialize]
        public void Setup()
        {
            // Use in-memory database for testing
            _context = new InventoryAPIContext();
            _service = new SupplierService(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context?.Dispose();
        }

        [TestMethod]
        public void GetAll_ReturnsAllSuppliers()
        {
            // Arrange
            var supplier1 = new Supplier { /* Set properties */ };
            var supplier2 = new Supplier { /* Set properties */ };
            
            _context.Suppliers.Add(supplier1);
            _context.Suppliers.Add(supplier2);
            _context.SaveChanges();

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetById_ReturnsCorrectSupplier()
        {
            // Arrange
            var supplier = new Supplier { /* Set properties */ };
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();

            // Act
            var result = _service.GetById(supplier.Id);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_AddsSupplierSuccessfully()
        {
            // Arrange
            var supplier = new Supplier { /* Set properties */ };

            // Act
            var result = _service.Create(supplier);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, _context.Suppliers.Count());
        }

        [TestMethod]
        public void Delete_RemovesSupplierSuccessfully()
        {
            // Arrange
            var supplier = new Supplier { /* Set properties */ };
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();

            // Act
            var result = _service.Delete(supplier.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _context.Suppliers.Count());
        }
    }
}