using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Controllers;
using MovieReviewApp.Models;
using MovieReviewApp.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using MovieReviewApp.Tools;

namespace MovieReviewApp.Tests.Controllers {
    [TestFixture]
    public class UserControllerTests {
        private Mock<IUserService> _mockUserService;
        private UserController _userController;

        [SetUp]
        public void Setup() {
            _mockUserService = new Mock<IUserService>();
            _userController = new UserController(_mockUserService.Object);
        }

        #region GetAllUsers Tests

        [Test]
        public async Task GetUsers_ReturnsOk_WhenUsersExist() {
            // Arrange
            var users = new[] {
                new User { UserId = 1, Email = "user1@example.com", FirstName = "User", LastName = "1", Username = "user1", Password = "password" },
                new User { UserId = 2, Email = "user2@example.com", FirstName = "User", LastName = "2", Username = "user2", Password = "password" }
            };
            _mockUserService.Setup(service => service.GetUsers()).ReturnsAsync(users);

            // Act
            var result = await _userController.GetUsers();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(users, okResult.Value);
        }

        [Test]
        public async Task GetUsers_ReturnsNotFound_WhenNoUsersExist() {

            // var users = Array.Empty<User>();
            // Arrange
            _mockUserService.Setup(service => service.GetUsers()).ReturnsAsync(Array.Empty<User>());

            // Act
            var result = await _userController.GetUsers();

            // Assert
            var notFoundResult = result as ObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        #endregion

        #region GetUsersByID Tests

        [Test]
        public async Task GetUserById_ReturnsOk_WhenUserExists() {
            // Arrange
            var user = new User { UserId = 1, Email = "user1@example.com", FirstName = "User", LastName = "1", Username = "user1", Password = "password" };
            _mockUserService.Setup(service => service.GetUserById(1)).ReturnsAsync(user);

            // Act
            var result = await _userController.GetUserById(1);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(user, okResult.Value);
        }

        [Test]
        public async Task GetUserById_ReturnsNotFound_WhenUserDoesNotExist() {
            // Arrange
            _mockUserService.Setup(service => service.GetUserById(1)).ReturnsAsync((User)null);

            // Act
            var result = await _userController.GetUserById(1);

            // Assert
            var notFoundResult = result as ObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        #endregion

        #region GetUsersByEmail Tests

        // Test for GetUserByEmail
        [Test]
        public async Task GetUserByEmail_ReturnsOk_WhenUserExists() {
            // Arrange
            var user = new User { UserId = 1, Email = "user1@example.com", FirstName = "User", LastName = "1", Username = "user1", Password = "password" };
            _mockUserService.Setup(service => service.GetUserByEmail("user1@example.com")).ReturnsAsync(user);

            // Act
            var result = await _userController.GetUserByEmail("user1@example.com");

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(user, okResult.Value);
        }

        [Test]
        public async Task GetUserByEmail_ReturnsNotFound_WhenUserDoesNotExist() {
            // Arrange
            _mockUserService.Setup(service => service.GetUserByEmail("nonexistent@example.com")).ReturnsAsync((User)null);

            // Act
            var result = await _userController.GetUserByEmail("nonexistent@example.com");

            // Assert
            var notFoundResult = result as ObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        #endregion

        #region UpdateUser Tests

        // Test for UpdateUser
        [Test]
        public async Task UpdateUser_ReturnsNoContent_WhenUserIsUpdated() {
            // Arrange
            var user = new User { UserId = 1, Email = "user1@example.com", Username = "user1", FirstName = "User", LastName = "1", Password = "password" };
            _mockUserService.Setup(service => service.GetUserById(1)).ReturnsAsync(user);
            _mockUserService.Setup(service => service.UpdateUser(It.IsAny<User>())).Returns(Task.CompletedTask);

            // Act
            var result = await _userController.UpdateUser(user);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task UpdateUser_ReturnsNotFound_WhenUserDoesNotExist() {
            // Arrange
            var user = new User { UserId = 999, Email = "nonexistent@example.com", Username = "nonexistent", FirstName = "Non", LastName = "Existant", Password = "password" };
            _mockUserService.Setup(service => service.GetUserById(999)).ReturnsAsync((User)null);

            // Act
            var result = await _userController.UpdateUser(user);

            // Assert
            var notFoundResult = result as ObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        #endregion

        #region RemoveUser Tests

        // Test for RemoveUser
        [Test]
        public async Task RemoveUser_ReturnsNoContent_WhenUserIsRemoved() {
            // Arrange
            var user = new User {UserId = 1, Email = "user1@example.com", FirstName = "User", LastName = "1", Username = "user1", Password = "password" };
            _mockUserService.Setup(service => service.GetUserById(1)).ReturnsAsync(user);
            _mockUserService.Setup(service => service.RemoveUser(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _userController.RemoveUser(1);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task RemoveUser_ReturnsNotFound_WhenUserDoesNotExist() {
            // Arrange
            _mockUserService.Setup(service => service.GetUserById(999)).ReturnsAsync((User)null);

            // Act
            var result = await _userController.RemoveUser(999);

            // Assert
            var notFoundResult = result as ObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        #endregion

        #region LoginValidation Tests

        // Test for Login
        [Test]
        public async Task Login_ReturnsOk_WhenValidCredentials() {
            // Arrange
            var request = new LoginRequest { Email = "user1@example.com", Password = "password" };
            var user = new User {Email = "user1@example.com", Password = "password", FirstName = "User", LastName = "1", Username = "user1"};
            _mockUserService.Setup(service => service.ValidateUser(request.Email, request.Password)).ReturnsAsync(user);

            // Act
            var result = await _userController.Login(request);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            // Assert.AreEqual("Login successful!", ((dynamic)okResult.Value).message);
        }

        [Test]
        public async Task Login_ReturnsUnauthorized_WhenInvalidCredentials() {
            // Arrange
            var request = new LoginRequest { Email = "user1@example.com", Password = "wrongpassword" };
            _mockUserService.Setup(service => service.ValidateUser(request.Email, request.Password)).ReturnsAsync((User)null);

            // Act
            var result = await _userController.Login(request);

            // Assert
            var unauthorizedResult = result as ObjectResult;
            Assert.IsNotNull(unauthorizedResult);
            Assert.AreEqual(401, unauthorizedResult.StatusCode);
        }

        #endregion

        #region AddUser Tests

        // Test for AddUser (SignUp)
        [Test]
        public async Task AddUser_ReturnsCreated_WhenUserIsAdded() {
            // Arrange
            var userDTO = new UserDTO { Email = "user1@example.com", Username = "user1", Password = "password", FirstName = "Test", LastName = "Test" };
            _mockUserService.Setup(service => service.AddUser(It.IsAny<User>())).Returns(Task.CompletedTask);

            // Act
            var result = await _userController.AddUser(userDTO);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var statusCodeResult = result as OkObjectResult;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }

        [Test]
        public async Task AddUser_ReturnsBadRequest_WhenModelStateIsInvalid() {
            // Arrange
            _userController.ModelState.AddModelError("Email", "Required");

            var userDTO = new UserDTO { Email = "user1@example.com", Username = "user1", Password = "password", FirstName = "Test", LastName = "Test" };

            // Act
            var result = await _userController.AddUser(userDTO);

            // Assert
            var badRequestResult = result as ObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        #endregion
    }
}

