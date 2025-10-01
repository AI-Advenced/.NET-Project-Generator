using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryAPI.Data;
using InventoryAPI.Models;
using InventoryAPI.Services;
using System.Linq;

namespace InventoryAPI.Tests.Services
{
    [TestClass]
    public class StockServiceTests
    {
        private InventoryAPIContext _context;
        private StockService _service;

        [TestInitialize]
        public void Setup()
        {
            // Use in-memory database for testing
            _context = new InventoryAPIContext();
            _service = new StockService(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context?.Dispose();
        }

        [TestMethod]
        public void GetAll_ReturnsAllStocks()
        {
            // Arrange
            var stock1 = new Stock { /* Set properties */ };
            var stock2 = new Stock { /* Set properties */ };
            
            _context.Stocks.Add(stock1);
            _context.Stocks.Add(stock2);
            _context.SaveChanges();

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetById_ReturnsCorrectStock()
        {
            // Arrange
            var stock = new Stock { /* Set properties */ };
            _context.Stocks.Add(stock);
            _context.SaveChanges();

            // Act
            var result = _service.GetById(stock.Id);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_AddsStockSuccessfully()
        {
            // Arrange
            var stock = new Stock { /* Set properties */ };

            // Act
            var result = _service.Create(stock);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, _context.Stocks.Count());
        }

        [TestMethod]
        public void Delete_RemovesStockSuccessfully()
        {
            // Arrange
            var stock = new Stock { /* Set properties */ };
            _context.Stocks.Add(stock);
            _context.SaveChanges();

            // Act
            var result = _service.Delete(stock.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _context.Stocks.Count());
        }
    }
}