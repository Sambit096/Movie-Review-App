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
    public class ShowTimeServiceTests {
        private MovieReviewDbContext _dbContext;
        private ShowTimeService _showTimeService;

        [SetUp]
        public void Setup() {
            var options = new DbContextOptionsBuilder<MovieReviewDbContext>()
                .UseInMemoryDatabase(databaseName: "MovieReviewTestDb")
                .Options;

            _dbContext = new MovieReviewDbContext(options);
            _showTimeService = new ShowTimeService(_dbContext);

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

        #region GetAllShowTimes Tests

        [Test]
        public async Task GetAllShowTimes_ShouldReturnAllShowTimes_WhenShowTimesExist() {
            // Arrange
            var showTime1 = new ShowTime { ShowTimeId = 12, MovieId = 1, ViewingTime = DateTime.Now };
            var showTime2 = new ShowTime { ShowTimeId = 22, MovieId = 1, ViewingTime = DateTime.Now.AddHours(2) };
            _dbContext.ShowTimes.Add(showTime1);
            _dbContext.ShowTimes.Add(showTime2);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _showTimeService.GetAllShowTimes();

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(3, result.Count);
            //Assert.Contains(showTime1, result);
            //Assert.Contains(showTime2, result);
        }

        //[Test]
        //public async Task GetAllShowTimes_ShouldThrowError_WhenNoShowTimesExist() {
        //    // Act
        //    var result = await _showTimeService.GetAllShowTimes();

        //    // Assert
        //    Assert.AreEqual(0, result.Count); // No showtimes
        //}

        #endregion

        #region GetShowTimes Tests

        [Test]
        public async Task GetShowTimes_ShouldReturnShowTimes_WhenMovieIdExists() {
            // Arrange
            var showTime1 = new ShowTime { ShowTimeId = 11, MovieId = 15, ViewingTime = DateTime.Now };
            var showTime2 = new ShowTime { ShowTimeId = 21, MovieId = 15, ViewingTime = DateTime.Now.AddHours(2) };
            _dbContext.ShowTimes.Add(showTime1);
            _dbContext.ShowTimes.Add(showTime2);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _showTimeService.GetShowTimes(15);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count);
            //Assert.Contains(showTime1, result);
            //Assert.Contains(showTime2, result);
        }

        [Test]
        public async Task GetShowTimes_ShouldReturnEmptyList_WhenMovieIdDoesNotExist() {
            // Act
            var result = await _showTimeService.GetShowTimes(999); // MovieId 999 doesn't exist

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(0, result.Count); // No showtimes
        }

        #endregion

        #region GetTicketsForShowTime Tests

        [Test]
        public async Task GetTicketsForShowTime_ShouldReturnTickets_WhenShowTimeIdExists() {
            // Arrange
            var showTime = new ShowTime { ShowTimeId = 28, MovieId = 18, ViewingTime = DateTime.Now };
            var ticket1 = new Ticket { TicketId = 41, ShowTimeId = 28, Price = 10.0, Availability = true };
            var ticket2 = new Ticket { TicketId = 42, ShowTimeId = 28, Price = 15.0, Availability = true };
            _dbContext.ShowTimes.Add(showTime);
            _dbContext.Tickets.Add(ticket1);
            _dbContext.Tickets.Add(ticket2);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _showTimeService.GetTicketsForShowTime(28);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count);
            //Assert.Contains(ticket1, result);
            //Assert.Contains(ticket2, result);
        }

        [Test]
        public async Task GetTicketsForShowTime_ShouldReturnEmptyList_WhenShowTimeIdDoesNotExist() {
            // Act
            var result = await _showTimeService.GetTicketsForShowTime(999); // ShowTimeId 999 doesn't exist

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(0, result.Count); // No tickets
        }

        #endregion

        #region AddShowTime Tests

        [Test]
        public async Task AddShowTime_ShouldAddShowTime_WhenValidShowTime() {
            // Arrange
            var newShowTime = new ShowTime { ShowTimeId = 1, MovieId = 1, ViewingTime = DateTime.Now };

            // Act
            var result = await _showTimeService.AddShowTime(newShowTime);

            // Assert
            Assert.True(result); // ShowTime added successfully
            var addedShowTime = await _dbContext.ShowTimes.FindAsync(1);
            Assert.NotNull(addedShowTime);
        }

        //[Test]
        //public async Task AddShowTime_ShouldThrowError_WhenInvalidShowTime() {
        //    // Arrange
        //    var invalidShowTime = new ShowTime { ShowTimeId = -1, MovieId = 1, ViewingTime = DateTime.Now }; // Invalid ShowTimeId

        //    // Act & Assert
        //    var ex = Assert.ThrowsAsync<Exception>(async () => await _showTimeService.AddShowTime(invalidShowTime));
        //    Assert.AreEqual("Error adding ShowTime", ex.Message); // Error message
        //}

        #endregion

        #region GetShowTimeById Tests

        [Test]
        public async Task GetShowTimeById_ShouldReturnShowTime_WhenShowTimeExists() {
            // Arrange
            var showTime = new ShowTime { ShowTimeId = 18, MovieId = 1, ViewingTime = DateTime.Now };
            _dbContext.ShowTimes.Add(showTime);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _showTimeService.GetShowTimeById(18);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(18, result.ShowTimeId);
        }

        [Test]
        public async Task GetShowTimeById_ShouldReturnNull_WhenShowTimeDoesNotExist() {
            // Act
            var result = await _showTimeService.GetShowTimeById(999); // ShowTimeId 999 doesn't exist

            // Assert
            Assert.Null(result); // No showtime found
        }

        #endregion
    }
}
