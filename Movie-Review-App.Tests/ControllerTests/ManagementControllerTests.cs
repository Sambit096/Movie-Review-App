//using Moq;
//using NUnit.Framework;
//using Microsoft.AspNetCore.Mvc;
//using MovieReviewApp.Controllers;
//using MovieReviewApp.Interfaces;
//using MovieReviewApp.Models;
//using MovieReviewApp.Tools;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace MovieReviewApp.Tests.Controllers {
//    [TestFixture]
//    public class ManagementControllerTests {
//        private Mock<IManagementService> _mockManagementService;
//        private ManagementController _controller;

//        [SetUp]
//        public void Setup() {
//            _mockManagementService = new Mock<IManagementService>();
//            _controller = new ManagementController(_mockManagementService.Object);
//        }

//        #region AddTicketsToMovie Tests

//        [Test]
//        public async Task AddTicketsToMovie_ShouldReturnOk_WhenTicketsAreAdded() {
//            // Arrange
//            int movieId = 1;
//            var ticket = new Ticket { Price = 10.0, ShowTimeId = 1, Availability = true };
//            _mockManagementService.Setup(service => service.AddTicketsToMovie(movieId, ticket)).ReturnsAsync(true);

//            // Act
//            var result = await _controller.AddTicketsToMovie(movieId, ticket);

//            // Assert
//            var okResult = result as OkObjectResult;
//            Assert.IsNotNull(okResult);
//            Assert.AreEqual(200, okResult.StatusCode); // 200 OK
//            Assert.AreEqual("Tickets added successfully.", okResult.Value);
//        }

//        [Test]
//        public async Task AddTicketsToMovie_ShouldReturnNotFound_WhenMovieNotFound() {
//            // Arrange
//            int movieId = 1;
//            var ticket = new Ticket { Price = 10.0, ShowTimeId = 1, Availability = true };
//            _mockManagementService.Setup(service => service.AddTicketsToMovie(movieId, ticket)).ThrowsAsync(new KeyNotFoundException());

//            // Act
//            var result = await _controller.AddTicketsToMovie(movieId, ticket);

//            // Assert
//            var notFoundResult = result as ObjectResult;
//            Assert.IsNotNull(notFoundResult);
//            Assert.AreEqual(404, notFoundResult.StatusCode);
//            Assert.AreEqual(ErrorDictionary.ErrorLibrary[404], notFoundResult.Value);
//        }

//        [Test]
//        public async Task AddTicketsToMovie_ShouldReturnInternalServerError_WhenExceptionOccurs() {
//            // Arrange
//            int movieId = 1;
//            var ticket = new Ticket { Price = 10.0, ShowTimeId = 1, Availability = true };
//            _mockManagementService.Setup(service => service.AddTicketsToMovie(movieId, ticket)).ThrowsAsync(new Exception());

//            // Act
//            var result = await _controller.AddTicketsToMovie(movieId, ticket);

//            // Assert
//            var statusCodeResult = result as ObjectResult;
//            Assert.IsNotNull(statusCodeResult);
//            Assert.AreEqual(500, statusCodeResult.StatusCode);
//            Assert.AreEqual(ErrorDictionary.ErrorLibrary[500], statusCodeResult.Value);
//        }

//        #endregion

//        #region RemoveTicketsFromMovie Tests

//        [Test]
//        public async Task RemoveTicketsFromMovie_ShouldReturnOk_WhenTicketsAreRemoved() {
//            // Arrange
//            int movieId = 1;
//            int numberOfTickets = 2;
//            _mockManagementService.Setup(service => service.RemoveTicketsFromMovie(movieId, numberOfTickets)).ReturnsAsync(true);

//            // Act
//            var result = await _controller.RemoveTicketsFromMovie(movieId, numberOfTickets);

//            // Assert
//            var okResult = result as OkObjectResult;
//            Assert.IsNotNull(okResult);
//            Assert.AreEqual(200, okResult.StatusCode); // 200 OK
//            Assert.AreEqual("Tickets removed successfully.", okResult.Value);
//        }

//        [Test]
//        public async Task RemoveTicketsFromMovie_ShouldReturnNotFound_WhenTicketsNotFound() {
//            // Arrange
//            int movieId = 1;
//            int numberOfTickets = 2;
//            _mockManagementService.Setup(service => service.RemoveTicketsFromMovie(movieId, numberOfTickets)).ReturnsAsync(false);

//            // Act
//            var result = await _controller.RemoveTicketsFromMovie(movieId, numberOfTickets);

//            // Assert
//            var notFoundResult = result as ObjectResult;
//            Assert.IsNotNull(notFoundResult);
//            Assert.AreEqual(404, notFoundResult.StatusCode);
//            Assert.AreEqual(ErrorDictionary.ErrorLibrary[404], notFoundResult.Value);
//        }

//        [Test]
//        public async Task RemoveTicketsFromMovie_ShouldReturnInternalServerError_WhenExceptionOccurs() {
//            // Arrange
//            int movieId = 1;
//            int numberOfTickets = 2;
//            _mockManagementService.Setup(service => service.RemoveTicketsFromMovie(movieId, numberOfTickets)).ThrowsAsync(new Exception());

//            // Act
//            var result = await _controller.RemoveTicketsFromMovie(movieId, numberOfTickets);

//            // Assert
//            var statusCodeResult = result as ObjectResult;
//            Assert.IsNotNull(statusCodeResult);
//            Assert.AreEqual(500, statusCodeResult.StatusCode);
//            Assert.AreEqual(ErrorDictionary.ErrorLibrary[500], statusCodeResult.Value);
//        }

//        #endregion

//        #region EditTickets Tests

//        [Test]
//        public async Task EditTickets_ShouldReturnOk_WhenTicketsAreEdited() {
//            // Arrange
//            int movieId = 1;
//            var ticket = new Ticket { TicketId = 1, Price = 20.0, Availability = false };
//            _mockManagementService.Setup(service => service.EditTickets(movieId, ticket)).ReturnsAsync(true);

//            // Act
//            var result = await _controller.EditTickets(movieId, ticket);

//            // Assert
//            var okResult = result as OkObjectResult;
//            Assert.IsNotNull(okResult);
//            Assert.AreEqual(200, okResult.StatusCode); // 200 OK
//            Assert.AreEqual("Ticket updated successfully.", okResult.Value);
//        }

//        [Test]
//        public async Task EditTickets_ShouldReturnNotFound_WhenTicketNotFound() {
//            // Arrange
//            int movieId = 1;
//            var ticket = new Ticket { TicketId = 1, Price = 20.0, Availability = false };
//            _mockManagementService.Setup(service => service.EditTickets(movieId, ticket)).ReturnsAsync(false);

//            // Act
//            var result = await _controller.EditTickets(movieId, ticket);

//            // Assert
//            var notFoundResult = result as ObjectResult;
//            Assert.IsNotNull(notFoundResult);
//            Assert.AreEqual(404, notFoundResult.StatusCode);
//            Assert.AreEqual(ErrorDictionary.ErrorLibrary[404], notFoundResult.Value);
//        }

//        [Test]
//        public async Task EditTickets_ShouldReturnInternalServerError_WhenExceptionOccurs() {
//            // Arrange
//            int movieId = 1;
//            var ticket = new Ticket { TicketId = 1, Price = 20.0, Availability = false };
//            _mockManagementService.Setup(service => service.EditTickets(movieId, ticket)).ThrowsAsync(new Exception());

//            // Act
//            var result = await _controller.EditTickets(movieId, ticket);

//            // Assert
//            var statusCodeResult = result as ObjectResult;
//            Assert.IsNotNull(statusCodeResult);
//            Assert.AreEqual(500, statusCodeResult.StatusCode);
//            Assert.AreEqual(ErrorDictionary.ErrorLibrary[500], statusCodeResult.Value);
//        }

//        #endregion
//    }
//}
