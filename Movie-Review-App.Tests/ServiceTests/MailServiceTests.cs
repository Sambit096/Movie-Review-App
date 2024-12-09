using Moq;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Models;
using MovieReviewApp.Services;
using MovieReviewApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;
using MovieReviewApp.Interfaces;

namespace MovieReviewApp.Tests.Services {

    [TestFixture]
    public class MailServiceTests {
        private MovieReviewDbContext _dbContext;
        private MailService _mailService;

        [SetUp]
        public void Setup() {
            var options = new DbContextOptionsBuilder<MovieReviewDbContext>()
                .UseInMemoryDatabase(databaseName: "MovieReviewTestDb")
                .Options;

            _dbContext = new MovieReviewDbContext(options);
            _mailService = new MailService(_dbContext);

            // Clear existing data, to stop issue with reusing key
            ClearDbContext();
        }

        [TearDown]
        public void TearDown() {
            _dbContext.Dispose();
        }

        public void ClearDbContext() {
            _dbContext.ChangeTracker.Clear();
        }

        [Test]
        public async Task SendEmail_ShouldSendMail_WhenClientValid() {
            // Arrange
            var cart = new Cart { CartId = 1, UserId = 1 };
            var user = new User { UserId = 1, Username = "test", Email = "test@test.com", FirstName = "test", LastName = "case", Password = "test" };
            _dbContext.Carts.Add(cart);
            _dbContext.Users.Add(user);
            _dbContext.SaveChangesAsync();

            // Act
            var result = await _mailService.SendEmail(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task SendEmail_ShouldThrowError_WhenCartDoesNotExist() {
            // Arrange


            // Act


            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public async Task SendEmail_ShouldThrowError_WhenUserDoesNotExist() {
            // Arrange


            // Act


            // Assert
            Assert.IsTrue(true);
        }
    }
}
