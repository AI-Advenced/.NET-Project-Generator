using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryAPI.Controllers;
using InventoryAPI.Models;
using InventoryAPI.Services;
using System.Web.Http;
using System.Web.Http.Results;

namespace InventoryAPI.Tests.Controllers
{
    [TestClass]
    public class StockControllerTests
    {
        private StockController _controller;
        private MockStockService _mockService;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new MockStockService();
            _controller = new StockController(_mockService);
        }

        [TestMethod]
        public void Get_ReturnsOkResult()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<System.Collections.Generic.IEnumerable<Stock>>));
        }

        [TestMethod]
        public void GetById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var stock = new Stock { /* Set properties */ };
            _mockService.SetupGetById(stock);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Stock>));
        }

        [TestMethod]
        public void GetById_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            _mockService.SetupGetById(null);

            // Act
            var result = _controller.Get(999);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Post_WithValidModel_ReturnsCreatedResult()
        {
            // Arrange
            var stock = new Stock { /* Set properties */ };
            _mockService.SetupCreate(stock);

            // Act
            var result = _controller.Post(stock);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<Stock>));
        }

        [TestMethod]
        public void Delete_WithValidId_ReturnsOkResult()
        {
            // Arrange
            _mockService.SetupDelete(true);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void Delete_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            _mockService.SetupDelete(false);

            // Act
            var result = _controller.Delete(999);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }

    // Mock service for testing
    public class MockStockService : IStockService
    {
        private Stock _returnValue;
        private bool _deleteResult = true;

        public void SetupGetById(Stock returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupCreate(Stock returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupDelete(bool result)
        {
            _deleteResult = result;
        }

        public System.Collections.Generic.IEnumerable<Stock> GetAll()
        {
            return new[] { _returnValue ?? new Stock() };
        }

        public Stock GetById(int id)
        {
            return _returnValue;
        }

        public Stock Create(Stock stock)
        {
            return _returnValue ?? stock;
        }

        public Stock Update(Stock stock)
        {
            return _returnValue ?? stock;
        }

        public bool Delete(int id)
        {
            return _deleteResult;
        }
    }
}