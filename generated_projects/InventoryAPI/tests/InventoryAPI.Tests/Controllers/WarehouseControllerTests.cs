using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryAPI.Controllers;
using InventoryAPI.Models;
using InventoryAPI.Services;
using System.Web.Http;
using System.Web.Http.Results;

namespace InventoryAPI.Tests.Controllers
{
    [TestClass]
    public class WarehouseControllerTests
    {
        private WarehouseController _controller;
        private MockWarehouseService _mockService;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new MockWarehouseService();
            _controller = new WarehouseController(_mockService);
        }

        [TestMethod]
        public void Get_ReturnsOkResult()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<System.Collections.Generic.IEnumerable<Warehouse>>));
        }

        [TestMethod]
        public void GetById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var warehouse = new Warehouse { /* Set properties */ };
            _mockService.SetupGetById(warehouse);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Warehouse>));
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
            var warehouse = new Warehouse { /* Set properties */ };
            _mockService.SetupCreate(warehouse);

            // Act
            var result = _controller.Post(warehouse);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<Warehouse>));
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
    public class MockWarehouseService : IWarehouseService
    {
        private Warehouse _returnValue;
        private bool _deleteResult = true;

        public void SetupGetById(Warehouse returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupCreate(Warehouse returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupDelete(bool result)
        {
            _deleteResult = result;
        }

        public System.Collections.Generic.IEnumerable<Warehouse> GetAll()
        {
            return new[] { _returnValue ?? new Warehouse() };
        }

        public Warehouse GetById(int id)
        {
            return _returnValue;
        }

        public Warehouse Create(Warehouse warehouse)
        {
            return _returnValue ?? warehouse;
        }

        public Warehouse Update(Warehouse warehouse)
        {
            return _returnValue ?? warehouse;
        }

        public bool Delete(int id)
        {
            return _deleteResult;
        }
    }
}