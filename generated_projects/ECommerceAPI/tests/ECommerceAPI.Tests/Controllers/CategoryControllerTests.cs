using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECommerceAPI.Controllers;
using ECommerceAPI.Models;
using ECommerceAPI.Services;
using System.Web.Http;
using System.Web.Http.Results;

namespace ECommerceAPI.Tests.Controllers
{
    [TestClass]
    public class CategoryControllerTests
    {
        private CategoryController _controller;
        private MockCategoryService _mockService;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new MockCategoryService();
            _controller = new CategoryController(_mockService);
        }

        [TestMethod]
        public void Get_ReturnsOkResult()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<System.Collections.Generic.IEnumerable<Category>>));
        }

        [TestMethod]
        public void GetById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var category = new Category { /* Set properties */ };
            _mockService.SetupGetById(category);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Category>));
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
            var category = new Category { /* Set properties */ };
            _mockService.SetupCreate(category);

            // Act
            var result = _controller.Post(category);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<Category>));
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
    public class MockCategoryService : ICategoryService
    {
        private Category _returnValue;
        private bool _deleteResult = true;

        public void SetupGetById(Category returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupCreate(Category returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupDelete(bool result)
        {
            _deleteResult = result;
        }

        public System.Collections.Generic.IEnumerable<Category> GetAll()
        {
            return new[] { _returnValue ?? new Category() };
        }

        public Category GetById(int id)
        {
            return _returnValue;
        }

        public Category Create(Category category)
        {
            return _returnValue ?? category;
        }

        public Category Update(Category category)
        {
            return _returnValue ?? category;
        }

        public bool Delete(int id)
        {
            return _deleteResult;
        }
    }
}