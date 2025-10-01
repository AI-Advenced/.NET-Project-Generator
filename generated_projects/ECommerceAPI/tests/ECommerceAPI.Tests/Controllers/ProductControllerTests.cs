using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECommerceAPI.Controllers;
using ECommerceAPI.Models;
using ECommerceAPI.Services;
using System.Web.Http;
using System.Web.Http.Results;

namespace ECommerceAPI.Tests.Controllers
{
    [TestClass]
    public class ProductControllerTests
    {
        private ProductController _controller;
        private MockProductService _mockService;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new MockProductService();
            _controller = new ProductController(_mockService);
        }

        [TestMethod]
        public void Get_ReturnsOkResult()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<System.Collections.Generic.IEnumerable<Product>>));
        }

        [TestMethod]
        public void GetById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var product = new Product { /* Set properties */ };
            _mockService.SetupGetById(product);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Product>));
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
            var product = new Product { /* Set properties */ };
            _mockService.SetupCreate(product);

            // Act
            var result = _controller.Post(product);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<Product>));
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
    public class MockProductService : IProductService
    {
        private Product _returnValue;
        private bool _deleteResult = true;

        public void SetupGetById(Product returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupCreate(Product returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupDelete(bool result)
        {
            _deleteResult = result;
        }

        public System.Collections.Generic.IEnumerable<Product> GetAll()
        {
            return new[] { _returnValue ?? new Product() };
        }

        public Product GetById(int id)
        {
            return _returnValue;
        }

        public Product Create(Product product)
        {
            return _returnValue ?? product;
        }

        public Product Update(Product product)
        {
            return _returnValue ?? product;
        }

        public bool Delete(int id)
        {
            return _deleteResult;
        }
    }
}