using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECommerceAPI.Controllers;
using ECommerceAPI.Models;
using ECommerceAPI.Services;
using System.Web.Http;
using System.Web.Http.Results;

namespace ECommerceAPI.Tests.Controllers
{
    [TestClass]
    public class ReviewControllerTests
    {
        private ReviewController _controller;
        private MockReviewService _mockService;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new MockReviewService();
            _controller = new ReviewController(_mockService);
        }

        [TestMethod]
        public void Get_ReturnsOkResult()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<System.Collections.Generic.IEnumerable<Review>>));
        }

        [TestMethod]
        public void GetById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var review = new Review { /* Set properties */ };
            _mockService.SetupGetById(review);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Review>));
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
            var review = new Review { /* Set properties */ };
            _mockService.SetupCreate(review);

            // Act
            var result = _controller.Post(review);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<Review>));
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
    public class MockReviewService : IReviewService
    {
        private Review _returnValue;
        private bool _deleteResult = true;

        public void SetupGetById(Review returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupCreate(Review returnValue)
        {
            _returnValue = returnValue;
        }

        public void SetupDelete(bool result)
        {
            _deleteResult = result;
        }

        public System.Collections.Generic.IEnumerable<Review> GetAll()
        {
            return new[] { _returnValue ?? new Review() };
        }

        public Review GetById(int id)
        {
            return _returnValue;
        }

        public Review Create(Review review)
        {
            return _returnValue ?? review;
        }

        public Review Update(Review review)
        {
            return _returnValue ?? review;
        }

        public bool Delete(int id)
        {
            return _deleteResult;
        }
    }
}