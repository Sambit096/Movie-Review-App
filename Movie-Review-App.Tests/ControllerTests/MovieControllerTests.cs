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

        #region AddMovie Tests

        [Test]
        public async Task AddMovie_ShouldReturnCreated_WhenMovieIsAdded() {
            // Arrange
            var movie = new Movie { Title = "New Movie" };
            _mockMovieService.Setup(service => service.AddMovie(movie)).ReturnsAsync(true);

            // Act
            var result = await _controller.AddMovie(movie);

            // Assert
            var statusCodeResult = result as StatusCodeResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(201, statusCodeResult.StatusCode); // 201 Created
        }

        [Test]
        public async Task AddMovie_ShouldReturnUnprocessableEntity_WhenMovieCannotBeAdded() {
            // Arrange
            var movie = new Movie { Title = "New Movie" };
            _mockMovieService.Setup(service => service.AddMovie(movie)).ReturnsAsync(false);

            // Act
            var result = await _controller.AddMovie(movie);

            // Assert
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(422, statusCodeResult.StatusCode); // 422 Unprocessable Entity
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[422], statusCodeResult.Value);
        }

        [Test]
        public async Task AddMovie_ShouldReturnInternalServerError_WhenExceptionOccurs() {
            // Arrange
            var movie = new Movie { Title = "New Movie" };
            _mockMovieService.Setup(service => service.AddMovie(movie)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.AddMovie(movie);

            // Assert
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[500], statusCodeResult.Value);  // Assuming ErrorDictionary[500] returns this
        }

        #endregion

        #region RemoveMovie Tests

        [Test]
        public async Task RemoveMovie_ShouldReturnOk_WhenMovieIsRemoved() {
            // Arrange
            var movie = new Movie { MovieId = 1, Title = "Test Movie" };
            _mockMovieService.Setup(service => service.RemoveMovie(movie)).ReturnsAsync(true);

            // Act
            var result = await _controller.RemoveMovie(movie);

            // Assert
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode); // 200 OK
        }

        [Test]
        public async Task RemoveMovie_ShouldReturnNotFound_WhenMovieDoesNotExist() {
            // Arrange
            var movie = new Movie { MovieId = 1, Title = "Test Movie" };
            _mockMovieService.Setup(service => service.RemoveMovie(movie)).ReturnsAsync(false);

            // Act
            var result = await _controller.RemoveMovie(movie);

            // Assert
            var notFoundResult = result as ObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[404], notFoundResult.Value);
        }

        [Test]
        public async Task RemoveMovie_ShouldReturnInternalServerError_WhenExceptionOccurs() {
            // Arrange
            var movie = new Movie { MovieId = 1, Title = "Test Movie" };
            _mockMovieService.Setup(service => service.RemoveMovie(movie)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.RemoveMovie(movie);

            // Assert
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[500], statusCodeResult.Value);  // Assuming ErrorDictionary[500] returns this
        }

        #endregion

        #region EditMovie Tests

        [Test]
        public async Task EditMovie_ShouldReturnOk_WhenMovieIsEdited() {
            // Arrange
            var movie = new Movie { MovieId = 1, Title = "Updated Movie" };
            _mockMovieService.Setup(service => service.EditMovie(movie)).ReturnsAsync(true);

            // Act
            var result = await _controller.EditMovie(movie);

            // Assert
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode); // 200 OK
        }

        [Test]
        public async Task EditMovie_ShouldReturnNotFound_WhenMovieDoesNotExist() {
            // Arrange
            var movie = new Movie { MovieId = 1, Title = "Updated Movie" };
            _mockMovieService.Setup(service => service.EditMovie(movie)).ReturnsAsync(false);

            // Act
            var result = await _controller.EditMovie(movie);

            // Assert
            var notFoundResult = result as ObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[404], notFoundResult.Value);  // Assuming ErrorDictionary[404] returns this
        }

        [Test]
        public async Task EditMovie_ShouldReturnInternalServerError_WhenExceptionOccurs() {
            // Arrange
            var movie = new Movie { MovieId = 1, Title = "Updated Movie" };
            _mockMovieService.Setup(service => service.EditMovie(movie)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.EditMovie(movie);

            // Assert
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual(ErrorDictionary.ErrorLibrary[500], statusCodeResult.Value);  // Assuming ErrorDictionary[500] returns this
        }

        #endregion
    }
}
