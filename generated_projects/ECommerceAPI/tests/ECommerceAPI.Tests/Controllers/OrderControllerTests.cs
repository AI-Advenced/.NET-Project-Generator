using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECommerceAPI.Controllers;
using ECommerceAPI.Models;
using ECommerceAPI.Services;
using System.Web.Http;
using System.Web.Http.Results;

namespace ECommerceAPI.Tests.Controllers
{
    [TestClass]
    public class OrderControllerTests
    {
        private OrderController _controller;
        private MockOrderService _mockService;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new MockOrderService();
            _controller = new OrderController(_mockService);
        }

        [TestMethod]
        public void Get_ReturnsOkResult()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<System.Collections.Generic.IEnumerable<Order>>));
        }

        [TestMethod]
        public void GetById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var order = new Order { /* Set properties */ };
            _mockService.SetupGetById(order);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Order>));
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
            var order = new Order { /* Set properties */ };
            _mockService.SetupCreate(order);

            // Act
            var result = _controller.Post(order);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<Order>));
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
    public class MockOrderService : IOrderService
    {
        private Order _returnValue;
        private bool _deleteResult = true;

        public void SetupGetById(Order returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupCreate(Order returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupDelete(bool result)
        {
            _deleteResult = result;
        }

        public System.Collections.Generic.IEnumerable<Order> GetAll()
        {
            return new[] { _returnValue ?? new Order() };
        }

        public Order GetById(int id)
        {
            return _returnValue;
        }

        public Order Create(Order order)
        {
            return _returnValue ?? order;
        }

        public Order Update(Order order)
        {
            return _returnValue ?? order;
        }

        public bool Delete(int id)
        {
            return _deleteResult;
        }
    }
}