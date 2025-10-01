using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogAPI.Controllers;
using BlogAPI.Models;
using BlogAPI.Services;
using System.Web.Http;
using System.Web.Http.Results;

namespace BlogAPI.Tests.Controllers
{
    [TestClass]
    public class TagControllerTests
    {
        private TagController _controller;
        private MockTagService _mockService;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new MockTagService();
            _controller = new TagController(_mockService);
        }

        [TestMethod]
        public void Get_ReturnsOkResult()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<System.Collections.Generic.IEnumerable<Tag>>));
        }

        [TestMethod]
        public void GetById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var tag = new Tag { /* Set properties */ };
            _mockService.SetupGetById(tag);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Tag>));
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
            var tag = new Tag { /* Set properties */ };
            _mockService.SetupCreate(tag);

            // Act
            var result = _controller.Post(tag);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<Tag>));
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
    public class MockTagService : ITagService
    {
        private Tag _returnValue;
        private bool _deleteResult = true;

        public void SetupGetById(Tag returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupCreate(Tag returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupDelete(bool result)
        {
            _deleteResult = result;
        }

        public System.Collections.Generic.IEnumerable<Tag> GetAll()
        {
            return new[] { _returnValue ?? new Tag() };
        }

        public Tag GetById(int id)
        {
            return _returnValue;
        }

        public Tag Create(Tag tag)
        {
            return _returnValue ?? tag;
        }

        public Tag Update(Tag tag)
        {
            return _returnValue ?? tag;
        }

        public bool Delete(int id)
        {
            return _deleteResult;
        }
    }
}