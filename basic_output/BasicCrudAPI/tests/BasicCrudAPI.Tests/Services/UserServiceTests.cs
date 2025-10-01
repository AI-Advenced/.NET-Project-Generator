using Microsoft.VisualStudio.TestTools.UnitTesting;
using BasicCrudAPI.Data;
using BasicCrudAPI.Models;
using BasicCrudAPI.Services;
using System.Linq;

namespace BasicCrudAPI.Tests.Services
{
    [TestClass]
    public class UserServiceTests
    {
        private BasicCrudAPIContext _context;
        private UserService _service;

        [TestInitialize]
        public void Setup()
        {
            // Use in-memory database for testing
            _context = new BasicCrudAPIContext();
            _service = new UserService(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context?.Dispose();
        }

        [TestMethod]
        public void GetAll_ReturnsAllUsers()
        {
            // Arrange
            var user1 = new User { /* Set properties */ };
            var user2 = new User { /* Set properties */ };
            
            _context.Users.Add(user1);
            _context.Users.Add(user2);
            _context.SaveChanges();

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetById_ReturnsCorrectUser()
        {
            // Arrange
            var user = new User { /* Set properties */ };
            _context.Users.Add(user);
            _context.SaveChanges();

            // Act
            var result = _service.GetById(user.Id);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_AddsUserSuccessfully()
        {
            // Arrange
            var user = new User { /* Set properties */ };

            // Act
            var result = _service.Create(user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, _context.Users.Count());
        }

        [TestMethod]
        public void Delete_RemovesUserSuccessfully()
        {
            // Arrange
            var user = new User { /* Set properties */ };
            _context.Users.Add(user);
            _context.SaveChanges();

            // Act
            var result = _service.Delete(user.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _context.Users.Count());
        }
    }
}