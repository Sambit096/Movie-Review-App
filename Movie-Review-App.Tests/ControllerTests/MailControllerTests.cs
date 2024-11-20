using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Controllers;
using MovieReviewApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieReviewApp.Tools;

namespace MovieReviewApp.Tests.Controllers {
    [TestFixture]
    public class MailControllerTests {
        private Mock<IMailService> _mockMailService;
        private MailController _controller;

        [SetUp]
        public void Setup() {
            // Initialize the mock and controller before each test
            _mockMailService = new Mock<IMailService>();
            _controller = new MailController(_mockMailService.Object);
        }

        #region SendMail Tests

        [Test]
        public async Task SendEmail_ShouldReturnOk_WhenEmailIsSentSuccessfully() {
            // Arrange
            int cartId = 1;
            _mockMailService.Setup(service => service.SendEmail(cartId)).ReturnsAsync(true);

            // Act
            var result = await _controller.SendEmail(cartId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual("Email Sent!", okResult.Value);
        }

        [Test]
        public async Task SendEmail_ShouldReturnInternalServerError_WhenEmailSendingFails() {
            // Arrange
            int cartId = 1;
            _mockMailService.Setup(service => service.SendEmail(cartId)).ReturnsAsync(false);

            // Act
            var result = await _controller.SendEmail(cartId);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[500], statusCodeResult.Value);
        }

        [Test]
        public async Task SendEmail_ShouldReturnNotFound_WhenKeyNotFoundExceptionIsThrown() {
            // Arrange
            int cartId = 1;
            _mockMailService.Setup(service => service.SendEmail(cartId))
                            .ThrowsAsync(new KeyNotFoundException());

            // Act
            var result = await _controller.SendEmail(cartId);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[400], notFoundResult.Value);
        }

        [Test]
        public async Task SendEmail_ShouldReturnInternalServerError_WhenGeneralExceptionIsThrown() {
            // Arrange
            int cartId = 1;
            _mockMailService.Setup(service => service.SendEmail(cartId))
                            .ThrowsAsync(new Exception());

            // Act
            var result = await _controller.SendEmail(cartId);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[500], statusCodeResult.Value);
        }

        #endregion
    }
}