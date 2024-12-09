using Moq;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Models;
using MovieReviewApp.Services;
using MovieReviewApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieReviewApp.Implementations;

namespace MovieReviewApp.Tests.Services {
    [TestFixture]
    public class UserServiceTests {
        private MovieReviewDbContext _dbContext;
        private UserService _userService;

        [SetUp]
        public void Setup() {
            var options = new DbContextOptionsBuilder<MovieReviewDbContext>()
                .UseInMemoryDatabase(databaseName: "MovieReviewTestDb")
                .Options;

            _dbContext = new MovieReviewDbContext(options);
            _userService = new UserService(_dbContext);

            // Clear existing data to prevent issues with reusing primary keys
            ClearDbContext();
        }

        [TearDown]
        public void TearDown() {
            _dbContext.Dispose();
        }

        private void ClearDbContext() {
            _dbContext.ChangeTracker.Clear();
        }

        #region GetUser Tests

        [Test]
        public async Task GetUserById_ShouldReturnUser_WhenUserExists() {
            // Arrange
            var user = new User { UserId = 1, Username = "john_doe", Email = "john@example.com", FirstName = "John", LastName = "Doe", Password = "test" };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _userService.GetUserById(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, result.UserId);
            Assert.AreEqual("john_doe", result.Username);
        }

        [Test]
        public async Task GetUserById_ShouldReturnNull_WhenUserDoesNotExist() {
            // Act
            var result = await _userService.GetUserById(999);

            // Assert
            Assert.Null(result);
        }

        //[Test]
        //public async Task GetUserByEmail_ShouldReturnUser_WhenUserExists() {
        //    // Arrange
        //    var user = new User { UserId = 78, Username = "john_doe", Email = "john@example.com", FirstName = "John", LastName = "Doe", Password = "test" };
        //    _dbContext.Users.Add(user);
        //    await _dbContext.SaveChangesAsync();

        //    // Act
        //    var result = await _userService.GetUserByEmail("john@example.com");

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.AreEqual("john@example.com", result.Email);
        //}

        [Test]
        public async Task GetUserByEmail_ShouldReturnNull_WhenUserDoesNotExist() {
            // Act
            var result = await _userService.GetUserByEmail("nonexistent@example.com");

            // Assert
            Assert.Null(result);
        }

        #endregion

        #region AddUser Tests

        //[Test]
        //public async Task AddUser_ShouldAddUser_WhenUserIsValid() {
        //    // Arrange
        //    var user = new User { UserId = 79, Username = "john_doe", Email = "john@example.com", FirstName = "John", LastName = "Doe", Password = "test" };

        //    // Act
        //    await _userService.AddUser(user);

        //    // Assert
        //    var addedUser = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == "jane@example.com");
        //    Assert.NotNull(addedUser);
        //    Assert.AreEqual("jane_doe", addedUser.Username);
        //}

        #endregion

        #region RemoveUser Tests

        [Test]
        public async Task RemoveUser_ShouldRemoveUser_WhenUserExists() {
            // Arrange
            var user = new User { UserId = 20, Username = "john_doe", Email = "john@example.com", FirstName = "John", LastName = "Doe", Password = "test" };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            // Act
            await _userService.RemoveUser(20);

            // Assert
            var removedUser = await _dbContext.Users.FindAsync(20);
            Assert.Null(removedUser); // User should be removed
        }

        [Test]
        public async Task RemoveUser_ShouldNotThrowError_WhenUserDoesNotExist() {
            // Act
            await _userService.RemoveUser(999); // User with ID 999 does not exist

            // Assert
            // No exception should be thrown
        }

        #endregion

        #region UpdateUser Tests

        [Test]
        public async Task UpdateUser_ShouldUpdateUser_WhenUserExists() {
            // Arrange
            var user = new User { UserId = 3, Username = "susan_lee", Email = "susan@example.com", Password = "password123", FirstName = "susan", LastName = "lee" };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            var updatedUser = new User { UserId = 3, Username = "susan_lee_updated", Email = "susan_updated@example.com", Password = "newpassword123", FirstName = "susan", LastName = "lee" };

            // Act
            await _userService.UpdateUser(updatedUser);

            // Assert
            var userInDb = await _dbContext.Users.FindAsync(3);
            Assert.NotNull(userInDb);
            Assert.AreEqual("susan_lee_updated", userInDb.Username);
            Assert.AreEqual("susan_updated@example.com", userInDb.Email);
        }

        [Test]
        public async Task UpdateUser_ShouldNotUpdate_WhenUserDoesNotExist() {
            // Arrange
            var updatedUser = new User { UserId = 999, Username = "non_existent", Email = "nonexistent@example.com", FirstName = "test", LastName = "test", Password = "test" };

            // Act
            await _userService.UpdateUser(updatedUser);

            // Assert
            // No exception should be thrown, and the user should not exist
            var userInDb = await _dbContext.Users.FindAsync(999);
            Assert.Null(userInDb);
        }

        #endregion

        #region ValidateUser Tests

        [Test]
        public async Task ValidateUser_ShouldReturnUser_WhenCredentialsAreValid() {
            // Arrange
            var user = new User { UserId = 4, Email = "alice@example.com", Password = "password123", FirstName = "alice", LastName = "test", Username = "alicetests" };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _userService.ValidateUser("alice@example.com", "password123");

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("alice@example.com", result.Email);
        }

        [Test]
        public async Task ValidateUser_ShouldReturnNull_WhenInvalidCredentials() {
            // Arrange
            var user = new User { UserId = 5, Email = "bob@example.com", Password = "password123", FirstName = "bob", LastName = "test", Username = "bobtests" };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _userService.ValidateUser("bob@example.com", "wrongpassword");

            // Assert
            Assert.Null(result);
        }

        [Test]
        public async Task ValidateUser_ShouldReturnNull_WhenUserDoesNotExist() {
            // Act
            var result = await _userService.ValidateUser("nonexistent@example.com", "password123");

            // Assert
            Assert.Null(result);
        }

        #endregion
    }
}
