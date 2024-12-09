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
using MovieReviewApp.Interfaces;

namespace MovieReviewApp.Tests.Services {
    [TestFixture]
    public class MovieServiceTests {
        private MovieReviewDbContext _dbContext;
        private MovieService _movieService;

        [SetUp]
        public void SetUp() {
            var options = new DbContextOptionsBuilder<MovieReviewDbContext>()
                .UseInMemoryDatabase(databaseName: "MovieReviewTestDb")
                .Options;

            _dbContext = new MovieReviewDbContext(options);
            _movieService = new MovieService(_dbContext);

            ClearDbContext();
        }
        [TearDown]
        public void TearDown() {
            _dbContext.Dispose();
        }

        public void ClearDbContext() {
            _dbContext.ChangeTracker.Clear();
        }

        #region GetMovies Tests

        [Test]
        public async Task GetMovies_ShouldReturnMovies_WhenMoviesExist() {
            // Arrange
            var movie = new Movie { Title = "Test Movie" };
            _dbContext.Movies.Add(movie);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _movieService.GetMovies();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Count);
        }

        //[Test]
        //public async Task GetMovies_ShouldThrowError_WhenNoMoviesExist() {
        //    // Act
        //    var result = await _movieService.GetMovies();

        //    // Assert
        //    // Assert.IsInstanceOf<ObjectResult>(result);
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(0, result.Count);
        //    // Assert.AreEqual(500, result.StatusCode);
        //}

        #endregion

        #region GetMovieById Tests

        [Test]
        public async Task GetMovieById_ShouldReturnMovie_WhenMovieExists() {
            // Arrange
            var movie = new Movie { MovieId = 1, Title = "Test" };
            _dbContext.Movies.Add(movie);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = _movieService.GetMovieById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public async Task GetMovieById_ShouldThrowError_WhenMovieIsNull() {
            // Arrange


            // Act


            // Assert
        }

        [Test]
        public async Task GetMovieById_ShouldThrowError_WhenMovieDoesNotExist() {
            // Arrange


            // Act


            // Assert
        }

        #endregion

    }
}
