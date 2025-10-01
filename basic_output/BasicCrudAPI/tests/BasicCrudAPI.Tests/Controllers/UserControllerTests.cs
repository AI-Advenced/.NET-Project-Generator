using Microsoft.VisualStudio.TestTools.UnitTesting;
using BasicCrudAPI.Controllers;
using BasicCrudAPI.Models;
using BasicCrudAPI.Services;
using System.Web.Http;
using System.Web.Http.Results;

namespace BasicCrudAPI.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        private UserController _controller;
        private MockUserService _mockService;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new MockUserService();
            _controller = new UserController(_mockService);
        }

        [TestMethod]
        public void Get_ReturnsOkResult()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<System.Collections.Generic.IEnumerable<User>>));
        }

        [TestMethod]
        public void GetById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var user = new User { /* Set properties */ };
            _mockService.SetupGetById(user);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<User>));
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
            var user = new User { /* Set properties */ };
            _mockService.SetupCreate(user);

            // Act
            var result = _controller.Post(user);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<User>));
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
    public class MockUserService : IUserService
    {
        private User _returnValue;
        private bool _deleteResult = true;

        public void SetupGetById(User returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupCreate(User returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupDelete(bool result)
        {
            _deleteResult = result;
        }

        public System.Collections.Generic.IEnumerable<User> GetAll()
        {
            return new[] { _returnValue ?? new User() };
        }

        public User GetById(int id)
        {
            return _returnValue;
        }

        public User Create(User user)
        {
            return _returnValue ?? user;
        }

        public User Update(User user)
        {
            return _returnValue ?? user;
        }

        public bool Delete(int id)
        {
            return _deleteResult;
        }
    }
}