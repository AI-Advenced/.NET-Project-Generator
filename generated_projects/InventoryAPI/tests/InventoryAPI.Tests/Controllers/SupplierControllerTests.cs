using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryAPI.Controllers;
using InventoryAPI.Models;
using InventoryAPI.Services;
using System.Web.Http;
using System.Web.Http.Results;

namespace InventoryAPI.Tests.Controllers
{
    [TestClass]
    public class SupplierControllerTests
    {
        private SupplierController _controller;
        private MockSupplierService _mockService;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new MockSupplierService();
            _controller = new SupplierController(_mockService);
        }

        [TestMethod]
        public void Get_ReturnsOkResult()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<System.Collections.Generic.IEnumerable<Supplier>>));
        }

        [TestMethod]
        public void GetById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var supplier = new Supplier { /* Set properties */ };
            _mockService.SetupGetById(supplier);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Supplier>));
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
            var supplier = new Supplier { /* Set properties */ };
            _mockService.SetupCreate(supplier);

            // Act
            var result = _controller.Post(supplier);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<Supplier>));
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
    public class MockSupplierService : ISupplierService
    {
        private Supplier _returnValue;
        private bool _deleteResult = true;

        public void SetupGetById(Supplier returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupCreate(Supplier returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupDelete(bool result)
        {
            _deleteResult = result;
        }

        public System.Collections.Generic.IEnumerable<Supplier> GetAll()
        {
            return new[] { _returnValue ?? new Supplier() };
        }

        public Supplier GetById(int id)
        {
            return _returnValue;
        }

        public Supplier Create(Supplier supplier)
        {
            return _returnValue ?? supplier;
        }

        public Supplier Update(Supplier supplier)
        {
            return _returnValue ?? supplier;
        }

        public bool Delete(int id)
        {
            return _deleteResult;
        }
    }
}