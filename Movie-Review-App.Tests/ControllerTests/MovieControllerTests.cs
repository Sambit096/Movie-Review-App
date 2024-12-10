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
    public class MovieControllerTests {
        private Mock<IMovieService> _mockMovieService;
        private MovieController _controller;

        [SetUp]
        public void Setup() {
            // Initialize the mock and controller before each test
            _mockMovieService = new Mock<IMovieService>();
            _controller = new MovieController(_mockMovieService.Object);
        }

        #region GetMovies Tests

        [Test]
        public async Task GetMovies_ShouldReturnOk_WhenMoviesExist() {
            // Arrange
            var movies = new List<Movie> { new Movie { MovieId = 1, Title = "Test Movie" } };
            _mockMovieService.Setup(service => service.GetMovies()).ReturnsAsync(movies);

            // Act
            var result = await _controller.GetMovies();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(movies, okResult.Value);
        }

        [Test]
        public async Task GetMovies_ShouldReturnNotFound_WhenNoMoviesExist() {
            // Arrange
            _mockMovieService.Setup(service => service.GetMovies()).ReturnsAsync(Array.Empty<Movie>());

            // Act
            var result = await _controller.GetMovies();

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var notFoundResult = result as ObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[404], notFoundResult.Value);
        }

        [Test]
        public async Task GetMovies_ShouldReturnInternalServerError_WhenExceptionOccurs() {
            // Arrange
            _mockMovieService.Setup(service => service.GetMovies()).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetMovies();

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[500], statusCodeResult.Value);
        }

        #endregion

        #region GetMovieById Tests

        [Test]
        public async Task GetMovieById_ShouldReturnOk_WhenMovieExists() {
            // Arrange
            int movieId = 1;
            var movie = new Movie { MovieId = movieId, Title = "Test Movie" };
            _mockMovieService.Setup(service => service.GetMovieById(movieId)).ReturnsAsync(movie);

            // Act
            var result = await _controller.GetMovieById(movieId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(movie, okResult.Value);
        }

        [Test]
        public async Task GetMovieById_ShouldReturnNotFound_WhenMovieNotFound() {
            // Arrange
            int movieId = 1;
            _mockMovieService.Setup(service => service.GetMovieById(movieId)).ThrowsAsync(new KeyNotFoundException());

            // Act
            var result = await _controller.GetMovieById(movieId);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var notFoundResult = result as ObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[404], notFoundResult.Value);
        }

        [Test]
        public async Task GetMovieById_ShouldReturnInternalServerError_WhenExceptionOccurs() {
            // Arrange
            int movieId = 1;
            _mockMovieService.Setup(service => service.GetMovieById(movieId)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetMovieById(movieId);

            // Assert
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[500], statusCodeResult.Value);
        }

        #endregion

    }
}
