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
    public class ShowTimeControllerTests {
        private Mock<IShowTimeService> _mockShowTimeService;
        private ShowTimeController _controller;

        [SetUp]
        public void Setup() {
            // Initialize the mock and controller before each test
            _mockShowTimeService = new Mock<IShowTimeService>();
            _controller = new ShowTimeController(_mockShowTimeService.Object);
        }

        #region GetAllShowTimes Tests

        [Test]
        public async Task GetAllShowTimes_ShouldReturnOk_WhenShowTimesExist() {
            // Arrange
            var showTimes = new List<ShowTime> { new ShowTime { ShowTimeId = 1, MovieId = 1, ViewingTime = DateTime.Now, Status = MovieStatus.Available } };
            _mockShowTimeService.Setup(service => service.GetAllShowTimes()).ReturnsAsync(showTimes);

            // Act
            var result = await _controller.GetAllShowTimes();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(showTimes, okResult.Value);
        }

        [Test]
        public async Task GetAllShowTimes_ShouldReturnNotFound_WhenNoShowTimesExist() {
            // Arrange
            _mockShowTimeService.Setup(service => service.GetAllShowTimes()).ReturnsAsync(Array.Empty<ShowTime>());

            // Act
            var result = await _controller.GetAllShowTimes();

            // Assert
            var notFoundResult = result as ObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[404], notFoundResult.Value);
        }

        [Test]
        public async Task GetAllShowTimes_ShouldReturnInternalServerError_WhenExceptionOccurs() {
            // Arrange
            _mockShowTimeService.Setup(service => service.GetAllShowTimes()).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetAllShowTimes();

            // Assert
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[500], statusCodeResult.Value);
        }

        #endregion

        #region GetShowTimes Tests

        [Test]
        public async Task GetShowTimes_ShouldReturnOk_WhenShowTimesExistForMovie() {
            // Arrange
            int movieId = 1;
            var showTimes = new List<ShowTime> { new ShowTime { ShowTimeId = 1, MovieId = movieId, ViewingTime = DateTime.Now, Status = MovieStatus.Available } };
            _mockShowTimeService.Setup(service => service.GetShowTimes(movieId)).ReturnsAsync(showTimes);

            // Act
            var result = await _controller.GetShowTimes(movieId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(showTimes, okResult.Value);
        }

        [Test]
        public async Task GetShowTimes_ShouldReturnNotFound_WhenNoShowTimesExistForMovie() {
            // Arrange
            int movieId = 1;
            _mockShowTimeService.Setup(service => service.GetShowTimes(movieId)).ReturnsAsync(Array.Empty<ShowTime>());

            // Act
            var result = await _controller.GetShowTimes(movieId);

            // Assert
            var notFoundResult = result as ObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[404], notFoundResult.Value);
        }

        [Test]
        public async Task GetShowTimes_ShouldReturnInternalServerError_WhenExceptionOccurs() {
            // Arrange
            int movieId = 1;
            _mockShowTimeService.Setup(service => service.GetShowTimes(movieId)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetShowTimes(movieId);

            // Assert
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[500], statusCodeResult.Value);
        }

        #endregion

        #region GetTicketsForShowTime Tests

        [Test]
        public async Task GetTicketsForShowTime_ShouldReturnOk_WhenTicketsExist() {
            // Arrange
            int showTimeId = 1;
            var tickets = new List<Ticket> { new Ticket { TicketId = 1, ShowTimeId = showTimeId, Price = 10.0, Availability = true } };
            _mockShowTimeService.Setup(service => service.GetTicketsForShowTime(showTimeId)).ReturnsAsync(tickets);

            // Act
            var result = await _controller.GetTicketsForShowTime(showTimeId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(tickets, okResult.Value);
        }

        [Test]
        public async Task GetTicketsForShowTime_ShouldReturnNotFound_WhenNoTicketsExistForShowTime() {
            // Arrange
            int showTimeId = 1;
            _mockShowTimeService.Setup(service => service.GetTicketsForShowTime(showTimeId)).ReturnsAsync(Array.Empty<Ticket>());

            // Act
            var result = await _controller.GetTicketsForShowTime(showTimeId);

            // Assert
            var notFoundResult = result as ObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[404], notFoundResult.Value);
        }

        [Test]
        public async Task GetTicketsForShowTime_ShouldReturnInternalServerError_WhenExceptionOccurs() {
            // Arrange
            int showTimeId = 1;
            _mockShowTimeService.Setup(service => service.GetTicketsForShowTime(showTimeId)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetTicketsForShowTime(showTimeId);

            // Assert
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[500], statusCodeResult.Value);
        }

        #endregion

        #region AddShowTime Tests

        [Test]
        public async Task AddShowTime_ShouldReturnCreated_WhenShowTimeIsAdded() {
            // Arrange
            var showTime = new ShowTime { MovieId = 1, ViewingTime = DateTime.Now, Status = MovieStatus.Available };
            _mockShowTimeService.Setup(service => service.AddShowTime(showTime)).ReturnsAsync(true);

            // Act
            var result = await _controller.AddShowTime(showTime);

            // Assert
            var statusCodeResult = result as StatusCodeResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(201, statusCodeResult.StatusCode); // 201 Created
        }

        [Test]
        public async Task AddShowTime_ShouldReturnBadRequest_WhenShowTimeCannotBeAdded() {
            // Arrange
            var showTime = new ShowTime { MovieId = 1, ViewingTime = DateTime.Now, Status = MovieStatus.SoldOut };
            _mockShowTimeService.Setup(service => service.AddShowTime(showTime)).ReturnsAsync(false);

            // Act
            var result = await _controller.AddShowTime(showTime);

            // Assert
            var badRequestResult = result as ObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode); // 400 Bad Request
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[400], badRequestResult.Value);
        }

        [Test]
        public async Task AddShowTime_ShouldReturnInternalServerError_WhenExceptionOccurs() {
            // Arrange
            var showTime = new ShowTime { MovieId = 1, ViewingTime = DateTime.Now, Status = MovieStatus.Available };
            _mockShowTimeService.Setup(service => service.AddShowTime(showTime)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.AddShowTime(showTime);

            // Assert
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[500], statusCodeResult.Value);
        }

        #endregion
    }
}

