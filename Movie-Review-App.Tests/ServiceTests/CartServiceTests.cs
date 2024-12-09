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

namespace MovieReviewApp.Tests.Services {
    [TestFixture]
    public class CartServiceTests {
        private MovieReviewDbContext _dbContext;
        private CartService _cartService;

        [SetUp]
        public void Setup() {
            var options = new DbContextOptionsBuilder<MovieReviewDbContext>()
                .UseInMemoryDatabase(databaseName: "MovieReviewTestDb")
                .Options;

            _dbContext = new MovieReviewDbContext(options);
            _cartService = new CartService(_dbContext);

            // Clear existing data, to stop issue with reusing key
            ClearDbContext();
            
        }

        [TearDown]
        public void TearDown() {
            _dbContext.Dispose();
        }

        private void ClearDbContext() {
            _dbContext.ChangeTracker.Clear();
        }

        #region GetCart Tests

        //[Test]
        //public async Task GetCart_ShouldReturnCart_WhenCartExists() {

        //    // Arrange
        //    var cart = new Cart { CartId = 1, Total = 0.0 };
        //    _dbContext.Carts.Add(cart);
        //    await _dbContext.SaveChangesAsync();

        //    // Act
        //    var result = await _cartService.GetCart(1);

        //    // Assert
        //    Assert.NotNull(cart);
        //    Assert.AreEqual(1, result.Cart.CartId);
        //}

        [Test]
        public async Task GetCart_ShouldCreateNewCart_WhenCartDoesNotExist() {
            // Act
            var cart = await _cartService.GetCart(999);

            // Assert
            Assert.NotNull(cart);
            Assert.AreEqual(0.0, cart.Cart.Total);
        }

        #endregion

        #region AddTicketToCart Tests

        [Test]
        public async Task AddTicketToCart_ShouldAddTicket_WhenCartExists() {
            // Arrange
            //var cart = new Cart { CartId = 11, Total = 0.0 };
            //_dbContext.Carts.Add(cart);
            //await _dbContext.SaveChangesAsync();
            var cart = _cartService.GetCart(1);

            var ticket = new Ticket { TicketId = 1, CartId = 1, ShowTimeId = 1, Price = 10.0, Availability = true };
            _dbContext.Tickets.Add(ticket);
            await _dbContext.SaveChangesAsync();

            // Act
            var resultCart = await _cartService.AddTicketToCart(1, 1, 2); // Add 2 tickets to cart

            // Assert
            Assert.NotNull(resultCart);
            Assert.AreEqual(20.0, resultCart.Total); // 10 * 2 = 20
        }

        [Test]
        public async Task AddTicketToCart_ShouldThrowError_WhenCartDoesNotExist() {
            // Act
            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _cartService.AddTicketToCart(999, 1, 2)); // Cart with Id 999 does not exist

            // Assert
            Assert.AreEqual("Error when adding ticket to cart:", ex.Message);
        }

        [Test]
        public async Task AddTicketToCart_ShouldAddNewTicket_WhenTicketDoesNotExist() {
            // Act
            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _cartService.AddTicketToCart(1, 999, 2)); // Ticket with Id 999 does not exist

            // Assert
            Assert.AreEqual("Error when adding ticket to cart:", ex.Message);
        }

        #endregion

        #region RemoveTicketFromCart Tests

        //[Test]
        //public async Task RemoveTicketFromCart_ShouldRemoveTicket_WhenTicketExists() {
        //    // Arrange
        //    var cart = new Cart { CartId = 10, Total = 10.0 };
        //    _dbContext.Carts.Add(cart);
        //    var ticket = new Ticket { TicketId = 2, CartId = 10, ShowTimeId = 1, Price = 10.0, Availability = false };
        //    _dbContext.Tickets.Add(ticket);
        //    await _dbContext.SaveChangesAsync();

        //    // Act
        //    var resultCart = await _cartService.RemoveTicketFromCart(10, 2); // Remove ticket from cart

        //    // Assert
        //    Assert.NotNull(resultCart);
        //    var removedTicket = await _dbContext.Tickets.FindAsync(2);
        //    Assert.Null(removedTicket); // Ticket should be removed
        //}

        [Test]
        public async Task RemoveTicketFromCart_ThrowsException_When_CartDoesNotExist() {
            // Act
            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _cartService.RemoveTicketFromCart(999, 1)); // Cart with Id 999 does not exist

            // Assert
            Assert.AreEqual("Error when removing ticket from cart:", ex.Message);
        }

        [Test]
        public async Task RemoveTicketFromCart_ThrowsException_WhenTicketDoesNotExist() {
            // Arrange
            var cart = new Cart { CartId = 8, Total = 10.0 };
            _dbContext.Carts.Add(cart);
            await _dbContext.SaveChangesAsync();

            // Act
            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _cartService.RemoveTicketFromCart(8, 999)); // Ticket with Id 999 does not exist

            // Assert
            Assert.AreEqual("Error when removing ticket from cart:", ex.Message);
        }


        #endregion

        #region ProcessPayment Tests

        //[Test]
        //public async Task ProcessPayment_Processes_Payment_When_Cart_Exists() {
        //    // Arrange
        //    var cart = new Cart { CartId = 6, Total = 20.0 };
        //    _dbContext.Carts.Add(cart);
        //    var paymentGateway = new PaymentGateway { CardNumber = "1234567890123456", CardHolderName = "John Doe", ExpirationDate = "12/25", CVC = "123" };
        //    _dbContext.PaymentGateways.Add(paymentGateway);
        //    await _dbContext.SaveChangesAsync();

        //    // Act
        //    var result = await _cartService.ProcessPayment(6, "1234567890123456", "12/25", "John Doe", "123");

        //    // Assert
        //    Assert.True(result); // Payment is successfully processed
        //}

        [Test]
        public async Task ProcessPayment_Throws_Exception_When_Cart_Does_Not_Exist() {
            // Act
            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _cartService.ProcessPayment(999, "1234567890123456", "12/25", "John Doe", "123")); // Cart with Id 999 does not exist

            // Assert
            Assert.AreEqual("Cart does not exist.", ex.Message);
        }

        [Test]
        public async Task ProcessPayment_Throws_Exception_When_CreditCard_Invalid() {
            // Arrange
            var cart = new Cart { CartId = 7, Total = 20.0 };
            _dbContext.Carts.Add(cart);
            await _dbContext.SaveChangesAsync();

            // Act
            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _cartService.ProcessPayment(7, "invalidCard", "12/25", "John Doe", "123")); // Invalid card number

            // Assert
            Assert.AreEqual("Invalid card number", ex.Message);
        }

        #endregion
    }
}
