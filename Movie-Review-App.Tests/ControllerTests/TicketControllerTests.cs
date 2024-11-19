using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Controllers;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using MovieReviewApp.Tools;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieReviewApp.Tests.Controllers {
    [TestFixture]
    public class TicketControllerTests {
        private Mock<ITicketService> _mockTicketService;
        private TicketController _controller;

        [SetUp]
        public void Setup() {
            // Initialize the mock and controller before each test
            _mockTicketService = new Mock<ITicketService>();
            _controller = new TicketController(_mockTicketService.Object);
        }

        #region GetAllTickets Tests

        [Test]
        public async Task GetAllTickets_ShouldReturnOk_WhenTicketsExist() {
            // Arrange
            var tickets = new List<Ticket> { new Ticket { TicketId = 1, Price = 10.0, ShowTimeId = 1, Availability = true  } };
            _mockTicketService.Setup(service => service.GetAllTickets()).ReturnsAsync(tickets);

            // Act
            var result = await _controller.GetAllTickets();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(tickets, okResult.Value);
        }

        [Test]
        public async Task GetAllTickets_ShouldReturnNotFound_WhenNoTicketsExist() {
            // Arrange
            _mockTicketService.Setup(service => service.GetAllTickets()).ReturnsAsync(Array.Empty<Ticket>());

            // Act
            var result = await _controller.GetAllTickets();

            // Assert
            var notFoundResult = result as ObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[404], notFoundResult.Value);
        }

        [Test]
        public async Task GetAllTickets_ShouldReturnInternalServerError_WhenExceptionOccurs() {
            // Arrange
            _mockTicketService.Setup(service => service.GetAllTickets()).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetAllTickets();

            // Assert
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[500], statusCodeResult.Value);
        }

        #endregion

        #region GetTickets Tests

        [Test]
        public async Task GetTickets_ShouldReturnOk_WhenTicketsExistForMovie() {
            // Arrange
            int movieId = 1;
            var tickets = new List<Ticket> { new Ticket { TicketId = 1, Price = 10.0, ShowTimeId = 1, Availability = true } };
            _mockTicketService.Setup(service => service.GetTickets(movieId)).ReturnsAsync(tickets);

            // Act
            var result = await _controller.GetTickets(movieId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(tickets, okResult.Value);
        }

        [Test]
        public async Task GetTickets_ShouldReturnNotFound_WhenNoTicketsExistForMovie() {
            // Arrange
            int movieId = 1;
            _mockTicketService.Setup(service => service.GetTickets(movieId)).ReturnsAsync(Array.Empty<Ticket>());

            // Act
            var result = await _controller.GetTickets(movieId);

            // Assert
            var notFoundResult = result as ObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[404], notFoundResult.Value);
        }

        [Test]
        public async Task GetTickets_ShouldReturnInternalServerError_WhenExceptionOccurs() {
            // Arrange
            int movieId = 1;
            _mockTicketService.Setup(service => service.GetTickets(movieId)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetTickets(movieId);

            // Assert
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[500], statusCodeResult.Value);
        }

        #endregion

        #region CreateTicket Tests

        [Test]
        public async Task CreateTicket_ShouldReturnCreated_WhenTicketIsCreated() {
            // Arrange
            var ticket = new Ticket { Price = 10.0, ShowTimeId = 1, Availability = true };
            var createdTicket = new Ticket { TicketId = 1, Price = 10.0, ShowTimeId = 1, Availability = true };
            _mockTicketService.Setup(service => service.AddTicket(ticket)).ReturnsAsync(createdTicket);

            // Act
            var result = await _controller.CreateTicket(ticket);

            // Assert
            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);  // Created
            Assert.AreEqual("GetAllTickets", createdResult.ActionName);
            Assert.AreEqual(createdTicket, createdResult.Value);
        }

        [Test]
        public async Task CreateTicket_ShouldReturnConflict_WhenTicketCannotBeCreated() {
            // Arrange
            var ticket = new Ticket { Price = 10.0, ShowTimeId = 1, Availability = true };
            _mockTicketService.Setup(service => service.AddTicket(ticket)).ReturnsAsync((Ticket)null);

            // Act
            var result = await _controller.CreateTicket(ticket);

            // Assert
            var conflictResult = result as ObjectResult;
            Assert.IsNotNull(conflictResult);
            Assert.AreEqual(409, conflictResult.StatusCode);  // Conflict
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[409], conflictResult.Value);
        }

        [Test]
        public async Task CreateTicket_ShouldReturnInternalServerError_WhenExceptionOccurs() {
            // Arrange
            var ticket = new Ticket { Price = 10.0, ShowTimeId = 1, Availability = true };
            _mockTicketService.Setup(service => service.AddTicket(ticket)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.CreateTicket(ticket);

            // Assert
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[500], statusCodeResult.Value);
        }

        #endregion

        #region UpdateTicket Tests

        [Test]
        public async Task UpdateTicket_ShouldReturnNoContent_WhenTicketIsUpdated() {
            // Arrange
            int ticketId = 1;
            var ticket = new Ticket { TicketId = ticketId, Price = 15.0, ShowTimeId = 1, Availability = true };
            _mockTicketService.Setup(service => service.UpdateTicket(ticketId, ticket)).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateTicket(ticketId, ticket);

            // Assert
            var noContentResult = result as NoContentResult;
            Assert.IsNotNull(noContentResult);
            Assert.AreEqual(204, noContentResult.StatusCode);  // No Content
        }

        [Test]
        public async Task UpdateTicket_ShouldReturnNotFound_WhenTicketDoesNotExist() {
            // Arrange
            int ticketId = 1;
            var ticket = new Ticket { TicketId = ticketId, Price = 15.0, ShowTimeId = 1, Availability = true };
            _mockTicketService.Setup(service => service.UpdateTicket(ticketId, ticket)).ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateTicket(ticketId, ticket);

            // Assert
            var notFoundResult = result as ObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);  // Not Found
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[404], notFoundResult.Value);
        }

        [Test]
        public async Task UpdateTicket_ShouldReturnInternalServerError_WhenExceptionOccurs() {
            // Arrange
            int ticketId = 1;
            var ticket = new Ticket { TicketId = ticketId, Price = 15.0, ShowTimeId = 1, Availability = true };
            _mockTicketService.Setup(service => service.UpdateTicket(ticketId, ticket)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.UpdateTicket(ticketId, ticket);

            // Assert
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[500], statusCodeResult.Value);
        }

        #endregion

        #region DeleteTicket Tests

        [Test]
        public async Task DeleteTicket_ShouldReturnNoContent_WhenTicketIsDeleted() {
            // Arrange
            int ticketId = 1;
            _mockTicketService.Setup(service => service.DeleteTicket(ticketId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteTicket(ticketId);

            // Assert
            var noContentResult = result as NoContentResult;
            Assert.IsNotNull(noContentResult);
            Assert.AreEqual(204, noContentResult.StatusCode);  // No Content
        }

        [Test]
        public async Task DeleteTicket_ShouldReturnNotFound_WhenTicketDoesNotExist() {
            // Arrange
            int ticketId = 1;
            _mockTicketService.Setup(service => service.DeleteTicket(ticketId)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteTicket(ticketId);

            // Assert
            var notFoundResult = result as ObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);  // Not Found
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[404], notFoundResult.Value);
        }

        [Test]
        public async Task DeleteTicket_ShouldReturnInternalServerError_WhenExceptionOccurs() {
            // Arrange
            int ticketId = 1;
            _mockTicketService.Setup(service => service.DeleteTicket(ticketId)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.DeleteTicket(ticketId);

            // Assert
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[500], statusCodeResult.Value);
        }

        #endregion
    }
}

