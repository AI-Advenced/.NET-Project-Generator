using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogAPI.Controllers;
using BlogAPI.Models;
using BlogAPI.Services;
using System.Web.Http;
using System.Web.Http.Results;

namespace BlogAPI.Tests.Controllers
{
    [TestClass]
    public class AuthorControllerTests
    {
        private AuthorController _controller;
        private MockAuthorService _mockService;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new MockAuthorService();
            _controller = new AuthorController(_mockService);
        }

        [TestMethod]
        public void Get_ReturnsOkResult()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<System.Collections.Generic.IEnumerable<Author>>));
        }

        [TestMethod]
        public void GetById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var author = new Author { /* Set properties */ };
            _mockService.SetupGetById(author);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Author>));
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
            var author = new Author { /* Set properties */ };
            _mockService.SetupCreate(author);

            // Act
            var result = _controller.Post(author);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<Author>));
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
    public class MockAuthorService : IAuthorService
    {
        private Author _returnValue;
        private bool _deleteResult = true;

        public void SetupGetById(Author returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupCreate(Author returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupDelete(bool result)
        {
            _deleteResult = result;
        }

        public System.Collections.Generic.IEnumerable<Author> GetAll()
        {
            return new[] { _returnValue ?? new Author() };
        }

        public Author GetById(int id)
        {
            return _returnValue;
        }

        public Author Create(Author author)
        {
            return _returnValue ?? author;
        }

        public Author Update(Author author)
        {
            return _returnValue ?? author;
        }

        public bool Delete(int id)
        {
            return _deleteResult;
        }
    }
}