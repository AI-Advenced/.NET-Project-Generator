using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryAPI.Controllers;
using InventoryAPI.Models;
using InventoryAPI.Services;
using System.Web.Http;
using System.Web.Http.Results;

namespace InventoryAPI.Tests.Controllers
{
    [TestClass]
    public class ItemControllerTests
    {
        private ItemController _controller;
        private MockItemService _mockService;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new MockItemService();
            _controller = new ItemController(_mockService);
        }

        [TestMethod]
        public void Get_ReturnsOkResult()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<System.Collections.Generic.IEnumerable<Item>>));
        }

        [TestMethod]
        public void GetById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var item = new Item { /* Set properties */ };
            _mockService.SetupGetById(item);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Item>));
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
            var item = new Item { /* Set properties */ };
            _mockService.SetupCreate(item);

            // Act
            var result = _controller.Post(item);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<Item>));
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
    public class MockItemService : IItemService
    {
        private Item _returnValue;
        private bool _deleteResult = true;

        public void SetupGetById(Item returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupCreate(Item returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupDelete(bool result)
        {
            _deleteResult = result;
        }

        public System.Collections.Generic.IEnumerable<Item> GetAll()
        {
            return new[] { _returnValue ?? new Item() };
        }

        public Item GetById(int id)
        {
            return _returnValue;
        }

        public Item Create(Item item)
        {
            return _returnValue ?? item;
        }

        public Item Update(Item item)
        {
            return _returnValue ?? item;
        }

        public bool Delete(int id)
        {
            return _deleteResult;
        }
    }
}