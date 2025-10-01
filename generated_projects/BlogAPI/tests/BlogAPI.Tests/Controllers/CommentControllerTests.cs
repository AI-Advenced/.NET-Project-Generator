using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogAPI.Controllers;
using BlogAPI.Models;
using BlogAPI.Services;
using System.Web.Http;
using System.Web.Http.Results;

namespace BlogAPI.Tests.Controllers
{
    [TestClass]
    public class CommentControllerTests
    {
        private CommentController _controller;
        private MockCommentService _mockService;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new MockCommentService();
            _controller = new CommentController(_mockService);
        }

        [TestMethod]
        public void Get_ReturnsOkResult()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<System.Collections.Generic.IEnumerable<Comment>>));
        }

        [TestMethod]
        public void GetById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var comment = new Comment { /* Set properties */ };
            _mockService.SetupGetById(comment);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Comment>));
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
            var comment = new Comment { /* Set properties */ };
            _mockService.SetupCreate(comment);

            // Act
            var result = _controller.Post(comment);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<Comment>));
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
    public class MockCommentService : ICommentService
    {
        private Comment _returnValue;
        private bool _deleteResult = true;

        public void SetupGetById(Comment returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupCreate(Comment returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupDelete(bool result)
        {
            _deleteResult = result;
        }

        public System.Collections.Generic.IEnumerable<Comment> GetAll()
        {
            return new[] { _returnValue ?? new Comment() };
        }

        public Comment GetById(int id)
        {
            return _returnValue;
        }

        public Comment Create(Comment comment)
        {
            return _returnValue ?? comment;
        }

        public Comment Update(Comment comment)
        {
            return _returnValue ?? comment;
        }

        public bool Delete(int id)
        {
            return _deleteResult;
        }
    }
}