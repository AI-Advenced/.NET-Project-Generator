using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogAPI.Controllers;
using BlogAPI.Models;
using BlogAPI.Services;
using System.Web.Http;
using System.Web.Http.Results;

namespace BlogAPI.Tests.Controllers
{
    [TestClass]
    public class BlogPostControllerTests
    {
        private BlogPostController _controller;
        private MockBlogPostService _mockService;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new MockBlogPostService();
            _controller = new BlogPostController(_mockService);
        }

        [TestMethod]
        public void Get_ReturnsOkResult()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<System.Collections.Generic.IEnumerable<BlogPost>>));
        }

        [TestMethod]
        public void GetById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var blogpost = new BlogPost { /* Set properties */ };
            _mockService.SetupGetById(blogpost);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<BlogPost>));
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
            var blogpost = new BlogPost { /* Set properties */ };
            _mockService.SetupCreate(blogpost);

            // Act
            var result = _controller.Post(blogpost);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<BlogPost>));
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
    public class MockBlogPostService : IBlogPostService
    {
        private BlogPost _returnValue;
        private bool _deleteResult = true;

        public void SetupGetById(BlogPost returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupCreate(BlogPost returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupDelete(bool result)
        {
            _deleteResult = result;
        }

        public System.Collections.Generic.IEnumerable<BlogPost> GetAll()
        {
            return new[] { _returnValue ?? new BlogPost() };
        }

        public BlogPost GetById(int id)
        {
            return _returnValue;
        }

        public BlogPost Create(BlogPost blogpost)
        {
            return _returnValue ?? blogpost;
        }

        public BlogPost Update(BlogPost blogpost)
        {
            return _returnValue ?? blogpost;
        }

        public bool Delete(int id)
        {
            return _deleteResult;
        }
    }
}