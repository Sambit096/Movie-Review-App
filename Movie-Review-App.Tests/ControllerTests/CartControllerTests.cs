using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Controllers;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using System;
using System.Threading.Tasks;

namespace MovieReviewApp.Tests {
    [TestFixture]
    public class CartControllerTests {
        private Mock<ICartService> _mockCartService;
        private CartController _cartController;

        [SetUp]
        public void Setup() {
            _mockCartService = new Mock<ICartService>();
            _cartController = new CartController(_mockCartService.Object);
        }

        // Test AddTicketToCart
        [Test]
        public async Task AddTicketToCart_ReturnsOk_WhenTicketIsAdded() {
            // Arrange
            int cartId = 1, ticketId = 101, quantity = 2;
            var cart = new Cart { CartId = cartId, User = null };
            _mockCartService.Setup(service => service.AddTicketToCart(cartId, ticketId, quantity))
                            .ReturnsAsync(cart);

            // Act
            var result = await _cartController.AddTicketToCart(cartId, ticketId, quantity);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task AddTicketToCart_ReturnsInternalServerError_WhenServiceFails() {
            // Arrange
            int cartId = 1, ticketId = 101, quantity = 1;
            _mockCartService.Setup(service => service.AddTicketToCart(cartId, ticketId, quantity)).ReturnsAsync((Cart)null);

            // Act
            var result = await _cartController.AddTicketToCart(cartId, ticketId, quantity);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        // Test RemoveTicketFromCart
        [Test]
        public async Task RemoveTicketFromCart_ReturnsOk_WhenTicketIsRemoved() {
            // Arrange
            int cartId = 1, ticketId = 101;
            var cart = new Cart { CartId = cartId };
            _mockCartService.Setup(service => service.RemoveTicketFromCart(cartId, ticketId))
                            .ReturnsAsync(cart);

            // Act
            var result = await _cartController.RemoveTicketFromCart(cartId, ticketId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task RemoveTicketFromCart_ReturnsInternalServerError_WhenServiceFails() {
            // Arrange
            int cartId = 1, ticketId = 101;
            _mockCartService.Setup(service => service.RemoveTicketFromCart(cartId, ticketId))
                            .ReturnsAsync((Cart)null);

            // Act
            var result = await _cartController.RemoveTicketFromCart(cartId, ticketId);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        // Test GetCart
        //[Test]
        //public async Task GetCart_ReturnsOk_WhenCartIsFound() {
        //    // Arrange
        //    int cartId = 1;
        //    var cart = new Cart();
        //    _mockCartService.Setup(service => service.GetCart(cartId))
        //                    .ReturnsAsync(cart);

        //    // Act
        //    var result = await _cartController.GetCart(cartId);

        //    // Assert
        //    var okResult = result as OkObjectResult;
        //    Assert.IsNotNull(okResult);
        //    Assert.AreEqual(200, okResult.StatusCode);
        //    Assert.AreEqual(cart, okResult.Value);
        //}

        //[Test]
        //public async Task GetCart_ReturnsNotFound_WhenCartNotFound() {
        //    // Arrange
        //    int cartId = 1;
        //    _mockCartService.Setup(service => service.GetCart(cartId))
        //                    .ReturnsAsync((Cart)null);

        //    // Act
        //    var result = await _cartController.GetCart(cartId);

        //    // Assert
        //    var notFoundResult = result as NotFoundResult;
        //    Assert.IsNotNull(notFoundResult);
        //    Assert.AreEqual(404, notFoundResult.StatusCode);
        //}

        // Test ProcessPayment
        [Test]
        public async Task ProcessPayment_ReturnsOk_WhenPaymentIsProcessed() {
            // Arrange
            int cartId = 1;
            string cardNumber = "4111111111111111", exp = "12/25", cardHolderName = "John Doe", cvc = "123";
            _mockCartService.Setup(service => service.ProcessPayment(cartId, cardNumber, exp, cardHolderName, cvc))
                            .ReturnsAsync(true);

            // Act
            var result = await _cartController.ProcessPayment(cartId, cardNumber, exp, cardHolderName, cvc);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual("Payment Processed!", okResult.Value);
        }

        [Test]
        public async Task ProcessPayment_ReturnsInternalServerError_WhenPaymentFails() {
            // Arrange
            int cartId = 1;
            string cardNumber = "4111111111111111", exp = "12/25", cardHolderName = "John Doe", cvc = "123";
            _mockCartService.Setup(service => service.ProcessPayment(cartId, cardNumber, exp, cardHolderName, cvc))
                            .ReturnsAsync(false);

            // Act
            var result = await _cartController.ProcessPayment(cartId, cardNumber, exp, cardHolderName, cvc);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        // Test AddTicketsByShowtime
        [Test]
        public async Task AddTicketsByShowtime_ReturnsOk_WhenTicketsAreAdded() {
            // Arrange
            int cartId = 1, showtimeId = 202, quantity = 3;
            _mockCartService.Setup(service => service.AddTicketsByShowtime(cartId, showtimeId, quantity))
                            .ReturnsAsync(true);

            // Act
            var result = await _cartController.AddTicketsByShowtime(cartId, showtimeId, quantity);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(true, okResult.Value);
        }

        [Test]
        public async Task AddTicketsByShowtime_ReturnsInternalServerError_WhenServiceFails() {
            // Arrange
            int cartId = 1, showtimeId = 202, quantity = 3;
            _mockCartService.Setup(service => service.AddTicketsByShowtime(cartId, showtimeId, quantity))
                            .ReturnsAsync(false);

            // Act
            var result = await _cartController.AddTicketsByShowtime(cartId, showtimeId, quantity);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }
    }
}

