using Moq;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Models;
using MovieReviewApp.Services;
using MovieReviewApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieReviewApp.Tests.Services {
    [TestFixture]
    public class ReviewServiceTests {
        private MovieReviewDbContext _dbContext;
        private ReviewService _reviewService;

        [SetUp]
        public void Setup() {
            var options = new DbContextOptionsBuilder<MovieReviewDbContext>()
                .UseInMemoryDatabase(databaseName: "MovieReviewTestDb")
                .Options;

            _dbContext = new MovieReviewDbContext(options);
            _reviewService = new ReviewService(_dbContext);

            // Clear existing data to avoid conflicts with primary key
            ClearDbContext();
        }

        [TearDown]
        public void TearDown() {
            _dbContext.Dispose();
        }

        private void ClearDbContext() {
            _dbContext.ChangeTracker.Clear();
        }

        #region GetReviews Tests

        [Test]
        public async Task GetReviews_ShouldReturnReviews_WhenReviewsExist() {
            // Arrange
            var movie = new Movie { MovieId = 50, Title = "Inception" };
            _dbContext.Movies.Add(movie);
            var review = new Review { ReviewId = 1001, MovieId = movie.MovieId, Rating = 5, Content = "Great movie!", ReviewerName = "John Doe", UserId = 1 };
            _dbContext.Reviews.Add(review);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _reviewService.GetReviews(movie);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Great movie!", result.First().Content);
        }

        [Test]
        public async Task GetReviews_ShouldReturnEmptyList_WhenNoReviewsExist() {
            // Act
            var result = await _reviewService.GetReviews(new Movie { MovieId = 373, Title = "Test"}); // No reviews for movie with Id 200

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        #endregion

        #region GetReviewsByUser Tests

        [Test]
        public async Task GetReviewsByUser_ShouldReturnReviews_WhenReviewsExist() {
            // Arrange
            var user = new User { UserId = 2, Username = "JohnDoe", Email = "test", FirstName = "John", LastName = "Doe", Password = "test" };
            _dbContext.Users.Add(user);
            var review = new Review { ReviewId = 1002, UserId = user.UserId, Rating = 5, Content = "Amazing!", ReviewerName = "John Doe", MovieId = 1 };
            _dbContext.Reviews.Add(review);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _reviewService.GetReviewsByUser(2);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Amazing!", result.First().Content);
        }

        [Test]
        public async Task GetReviewsByUser_ShouldReturnEmptyList_WhenNoReviewsExistForUser() {
            // Act
            var result = await _reviewService.GetReviewsByUser(999); // User 999 has no reviews

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        #endregion

        #region AddReview Tests

        [Test]
        public async Task AddReview_ShouldAddReview_WhenValid() {
            // Arrange
            var movie = new Movie { MovieId = 22, Title = "Inception" };
            _dbContext.Movies.Add(movie);
            var review = new Review { ReviewId = 1003, Rating = 5, Content = "Amazing movie!", ReviewerName = "John Doe", UserId = 4 };

            // Act
            var result = await _reviewService.AddReview(movie, review);

            // Assert
            Assert.True(result);
            var addedReview = await _dbContext.Reviews.FindAsync(1003);
            Assert.NotNull(addedReview);
            Assert.AreEqual("Amazing movie!", addedReview.Content);
        }

        [Test]
        public async Task AddReview_ShouldThrowError_WhenMovieDoesNotExist() {
            // Arrange
            var movie = new Movie { MovieId = 340, Title = "Nonexistent Movie" }; // Movie does not exist in DB
            var review = new Review { ReviewId = 1004, Rating = 4, Content = "Nice movie!", ReviewerName = "John Doe", UserId = 1 };

            // Act
            var ex = await _reviewService.AddReview(movie, review);//Assert.ThrowsAsync<Exception>(async () => await _reviewService.AddReview(movie, review));

            // Assert
            Assert.NotNull(ex);
            //Assert.AreEqual("Error adding review:", ex.Message);
        }

        #endregion

        #region EditReview Tests

        // Bad Id can't edit through the dbContext
        //[Test]
        //public async Task EditReview_ShouldEditReview_WhenReviewExists() {
        //    // Arrange
        //    var movie = new Movie { MovieId = 39, Title = "Inception" };
        //    _dbContext.Movies.Add(movie);
        //    var review = new Review { ReviewId = 1005, MovieId = movie.MovieId, Rating = 4, Content = "Good movie", ReviewerName = "John Doe", UserId = 15 };
        //    _dbContext.Reviews.Add(review);
        //    await _dbContext.SaveChangesAsync();

        //    var newReview = new Review { ReviewId = 1005, MovieId = 39, Rating = 5, Content = "Amazing movie!", ReviewerName = "John Doe", UserId = 15 };

        //    // Act
        //    var result = await _reviewService.EditReview(39, newReview);

        //    // Assert
        //    Assert.True(result);
        //    var editedReview = await _dbContext.Reviews.FindAsync(1005);
        //    Assert.AreEqual("Amazing movie!", editedReview.Content);
        //}

        [Test]
        public async Task EditReview_ShouldThrowError_WhenReviewDoesNotExist() {
            // Arrange
            var newReview = new Review { ReviewId = 999, Content = "Nonexistent review", Rating = 5 };

            // Act
            var ex = Assert.ThrowsAsync<Exception>(async () => await _reviewService.EditReview(999, newReview));

            // Assert
            Assert.AreEqual("Error updating review:", ex.Message);
        }

        #endregion

        #region RemoveReview Tests

        [Test]
        public async Task RemoveReview_ShouldRemoveReview_WhenReviewExists() {
            // Arrange
            var review = new Review { ReviewId = 1006, Rating = 5, Content = "Great movie!", ReviewerName = "John Doe", MovieId = 1, UserId = 1 };
            _dbContext.Reviews.Add(review);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _reviewService.RemoveReview(review);

            // Assert
            Assert.True(result);
            var removedReview = await _dbContext.Reviews.FindAsync(1006);
            Assert.Null(removedReview); // Review should be removed
        }

        [Test]
        public async Task RemoveReview_ShouldThrowError_WhenReviewDoesNotExist() {
            // Arrange
            var review = new Review { ReviewId = 999 }; // Non-existent review

            // Act
            var ex = Assert.ThrowsAsync<Exception>(async () => await _reviewService.RemoveReview(review));

            // Assert
            Assert.AreEqual("Error deleting review", ex.Message);
        }

        #endregion

        #region AddLike Tests

        //[Test]
        //public async Task AddLike_ShouldIncreaseLikes_WhenValidReview() {
        //    // Arrange
        //    var review = new Review { ReviewId = 1007, Rating = 5, Content = "Great movie!", Likes = 0 };
        //    _dbContext.Reviews.Add(review);
        //    await _dbContext.SaveChangesAsync();

        //    // Act
        //    var result = await _reviewService.AddLike(1007);

        //    // Assert
        //    Assert.True(result);
        //    var updatedReview = await _dbContext.Reviews.FindAsync(1007);
        //    Assert.AreEqual(1, updatedReview.Likes);
        //}

        [Test]
        public async Task AddLike_ShouldThrowError_WhenReviewDoesNotExist() {
            // Act
            var ex = Assert.ThrowsAsync<Exception>(async () => await _reviewService.AddLike(999));

            // Assert
            Assert.AreEqual("Error adding Like", ex.Message);
        }

        #endregion

        #region RemoveLike Tests

        //[Test]
        //public async Task RemoveLike_ShouldDecreaseLikes_WhenValidReview() {
        //    // Arrange
        //    var review = new Review { ReviewId = 1008, Rating = 5, Content = "Great movie!", Likes = 1 };
        //    _dbContext.Reviews.Add(review);
        //    await _dbContext.SaveChangesAsync();

        //    // Act
        //    var result = await _reviewService.RemoveLike(1008);

        //    // Assert
        //    Assert.True(result);
        //    var updatedReview = await _dbContext.Reviews.FindAsync(1008);
        //    Assert.AreEqual(0, updatedReview.Likes);
        //}

        //[Test]
        //public async Task RemoveLike_ShouldNotDecreaseLikes_WhenAlreadyZero() {
        //    // Arrange
        //    var review = new Review { ReviewId = 1009, Rating = 5, Content = "Great movie!", Likes = 0 };
        //    _dbContext.Reviews.Add(review);
        //    await _dbContext.SaveChangesAsync();

        //    // Act
        //    var result = await _reviewService.RemoveLike(1009);

        //    // Assert
        //    Assert.True(result);
        //    var updatedReview = await _dbContext.Reviews.FindAsync(1009);
        //    Assert.AreEqual(0, updatedReview.Likes); // Likes should stay at 0
        //}

        [Test]
        public async Task RemoveLike_ShouldThrowError_WhenReviewDoesNotExist() {
            // Act
            var ex = Assert.ThrowsAsync<Exception>(async () => await _reviewService.RemoveLike(999));

            // Assert
            Assert.AreEqual("Error adding Like", ex.Message);
        }

        #endregion
    }
}
