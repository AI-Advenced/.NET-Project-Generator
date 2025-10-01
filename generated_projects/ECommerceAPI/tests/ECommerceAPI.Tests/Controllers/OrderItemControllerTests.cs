using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECommerceAPI.Controllers;
using ECommerceAPI.Models;
using ECommerceAPI.Services;
using System.Web.Http;
using System.Web.Http.Results;

namespace ECommerceAPI.Tests.Controllers
{
    [TestClass]
    public class OrderItemControllerTests
    {
        private OrderItemController _controller;
        private MockOrderItemService _mockService;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new MockOrderItemService();
            _controller = new OrderItemController(_mockService);
        }

        [TestMethod]
        public void Get_ReturnsOkResult()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<System.Collections.Generic.IEnumerable<OrderItem>>));
        }

        [TestMethod]
        public void GetById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var orderitem = new OrderItem { /* Set properties */ };
            _mockService.SetupGetById(orderitem);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<OrderItem>));
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
            var orderitem = new OrderItem { /* Set properties */ };
            _mockService.SetupCreate(orderitem);

            // Act
            var result = _controller.Post(orderitem);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<OrderItem>));
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
    public class MockOrderItemService : IOrderItemService
    {
        private OrderItem _returnValue;
        private bool _deleteResult = true;

        public void SetupGetById(OrderItem returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupCreate(OrderItem returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupDelete(bool result)
        {
            _deleteResult = result;
        }

        public System.Collections.Generic.IEnumerable<OrderItem> GetAll()
        {
            return new[] { _returnValue ?? new OrderItem() };
        }

        public OrderItem GetById(int id)
        {
            return _returnValue;
        }

        public OrderItem Create(OrderItem orderitem)
        {
            return _returnValue ?? orderitem;
        }

        public OrderItem Update(OrderItem orderitem)
        {
            return _returnValue ?? orderitem;
        }

        public bool Delete(int id)
        {
            return _deleteResult;
        }
    }
}