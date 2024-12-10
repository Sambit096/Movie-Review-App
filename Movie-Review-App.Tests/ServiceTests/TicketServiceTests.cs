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
using Microsoft.Extensions.Logging;

namespace MovieReviewApp.Tests.Services {
    [TestFixture]
    public class TicketServiceTests {
        private MovieReviewDbContext _dbContext;
        private TicketService _ticketService;

        [SetUp]
        public void Setup() {
            var options = new DbContextOptionsBuilder<MovieReviewDbContext>()
                .UseInMemoryDatabase(databaseName: "MovieReviewTestDb")
                .Options;

            _dbContext = new MovieReviewDbContext(options);
            _ticketService = new TicketService(_dbContext, Mock.Of<ILogger<TicketService>>());

            // Clear existing data to avoid conflicts
            ClearDbContext();
        }

        [TearDown]
        public void TearDown() {
            _dbContext.Dispose();
        }

        private void ClearDbContext() {
            _dbContext.ChangeTracker.Clear();
        }

        #region GetTickets Tests

        //[Test]
        //public async Task GetTickets_ShouldReturnTickets_WhenTicketsExistForMovie() {
        //    // Arrange
        //    var movieId = 1;
        //    var showTime = new ShowTime { ShowTimeId = 18, MovieId = movieId, ViewingTime = DateTime.Now };
        //    _dbContext.ShowTimes.Add(showTime);
        //    _dbContext.Tickets.Add(new Ticket { TicketId = 19, ShowTimeId = 18, Price = 10.0, Availability = true });
        //    await _dbContext.SaveChangesAsync();

        //    // Act
        //    var result = await _ticketService.GetTickets(movieId);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.AreEqual(8, result.Count());
        //}

        [Test]
        public async Task GetTickets_ShouldReturnEmpty_WhenNoTicketsExistForMovie() {
            // Act
            var result = await _ticketService.GetTickets(999); // Invalid movieId

            // Assert
            Assert.IsEmpty(result);
        }

        #endregion

        #region GetAllTickets Tests

        //[Test]
        //public async Task GetAllTickets_ShouldReturnAllTickets_WhenTicketsExist() {
        //    // Arrange
        //    _dbContext.Tickets.Add(new Ticket { TicketId = 111, ShowTimeId = 14, Price = 10.0, Availability = true });
        //    _dbContext.Tickets.Add(new Ticket { TicketId = 211, ShowTimeId = 14, Price = 15.0, Availability = true });
        //    await _dbContext.SaveChangesAsync();

        //    // Act
        //    var result = await _ticketService.GetAllTickets();

        //    // Assert
        //    Assert.NotNull(result);
        //    // Counting all the previous tests contexts (cant clear somehow)
        //    Assert.AreEqual(3, result.Count());

        //    await _dbContext.DisposeAsync();
        //}

        #endregion

        #region AddTicketsToMovie Tests

        //[Test]
        //public async Task AddTicketsToMovie_ShouldAddTickets_WhenMovieHasShowTimes() {
        //    // Arrange
        //    var movieId = 1;
        //    var showTime = new ShowTime { ShowTimeId = 11, MovieId = movieId, ViewingTime = DateTime.Now };
        //    _dbContext.ShowTimes.Add(showTime);
        //    await _dbContext.SaveChangesAsync();

        //    // Act
        //    var result = await _ticketService.AddTicketsToMovie(movieId, 5);

        //    // Assert
        //    Assert.True(result);
        //    var tickets = await _dbContext.Tickets.Where(t => t.ShowTime.MovieId == movieId).ToListAsync();
        //    Assert.AreEqual(5, tickets.Count); // 5 tickets should be added
        //}

        [Test]
        public async Task AddTicketsToMovie_ShouldThrowException_WhenNoShowTimesExist() {
            // Act & Assert
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _ticketService.AddTicketsToMovie(999, 5)); // Movie with ID 999 does not have showtimes
            Assert.AreEqual("No showtimes found for the selected movie.", ex.Message);
        }

        #endregion

        #region EditTickets Tests

        [Test]
        public async Task EditTickets_ShouldUpdateTickets_WhenValid() {
            // Arrange
            var movieId = 1;
            var showTime = new ShowTime { ShowTimeId = 120, MovieId = movieId, ViewingTime = DateTime.Now };
            _dbContext.ShowTimes.Add(showTime);
            _dbContext.Tickets.Add(new Ticket { TicketId = 11, ShowTimeId = 120, Price = 10.0, Availability = true });
            await _dbContext.SaveChangesAsync();

            var updatedTicket = new Ticket { ShowTimeId = 120, Price = 12.0, Availability = false };

            // Act
            var result = await _ticketService.EditTickets(movieId, updatedTicket);

            // Assert
            Assert.True(result);
            var ticket = await _dbContext.Tickets.FindAsync(11);
            Assert.AreEqual(12.0, ticket.Price);
            Assert.False(ticket.Availability);
        }

        [Test]
        public async Task EditTickets_ShouldThrowException_WhenShowTimeDoesNotExist() {
            // Arrange
            var movieId = 1;
            var invalidTicket = new Ticket { ShowTimeId = 999, Price = 10.0, Availability = true };

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _ticketService.EditTickets(movieId, invalidTicket)); // Invalid ShowTimeId
            Assert.AreEqual("An error occurred while editing tickets.", ex.Message);
        }

        #endregion

        #region RemoveTicketsFromMovie Tests

        [Test]
        public async Task RemoveTicketsFromMovie_ShouldRemoveTickets_WhenTicketsExist() {
            // Arrange
            var movieId = 1;
            var showTime = new ShowTime { ShowTimeId = 13, MovieId = movieId, ViewingTime = DateTime.Now };
            _dbContext.ShowTimes.Add(showTime);
            _dbContext.Tickets.Add(new Ticket { TicketId = 12, ShowTimeId = 13, Price = 10.0, Availability = true });
            _dbContext.Tickets.Add(new Ticket { TicketId = 22, ShowTimeId = 13, Price = 15.0, Availability = true });
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _ticketService.RemoveTicketsFromMovie(movieId, 2);

            // Assert
            Assert.True(result);
            var tickets = await _dbContext.Tickets.Where(t => t.ShowTime.MovieId == movieId).ToListAsync();
            Assert.AreEqual(3, tickets.Count); // Tickets should be removed
        }

        //[Test]
        //public async Task RemoveTicketsFromMovie_ShouldThrowException_WhenNotEnoughTicketsToRemove() {
        //    // Arrange
        //    var movieId = 1;
        //    var showTime = new ShowTime { ShowTimeId = 20, MovieId = movieId, ViewingTime = DateTime.Now };
        //    _dbContext.ShowTimes.Add(showTime);
        //    _dbContext.Tickets.Add(new Ticket { TicketId = 13, ShowTimeId = 20, Price = 10.0, Availability = true });
        //    await _dbContext.SaveChangesAsync();

        //    // Act & Assert
        //    var ex = Assert.ThrowsAsync<Exception>(async () =>
        //        await _ticketService.RemoveTicketsFromMovie(movieId, 2)); // Trying to remove more tickets than available
        //    Assert.AreEqual("Not enough available tickets to remove.", ex.Message);
        //}

        #endregion

        #region GetAvailableTicketsByMovie Tests

        [Test]
        public async Task GetAvailableTicketsByMovie_ShouldReturnCount_WhenTicketsAreAvailable() {
            // Arrange
            var movieId = 1;
            var showTime = new ShowTime { ShowTimeId = 16, MovieId = movieId, ViewingTime = DateTime.Now };
            _dbContext.ShowTimes.Add(showTime);
            _dbContext.Tickets.Add(new Ticket { TicketId = 14, ShowTimeId = 16, Price = 10.0, Availability = true });
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _ticketService.GetAvailableTicketsByMovie(movieId);

            // Assert
            Assert.AreEqual(1, result); // 1 available ticket
        }

        //[Test]
        //public async Task GetAvailableTicketsByMovie_ShouldReturnZero_WhenNoTicketsAvailable() {
        //    ClearDbContext();
        //    // Arrange
        //    var movieId = 1;
        //    var showTime = new ShowTime { ShowTimeId = 17, MovieId = movieId, ViewingTime = DateTime.Now };
        //    _dbContext.ShowTimes.Add(showTime);
        //    await _dbContext.SaveChangesAsync();

        //    // Act
        //    var result = await _ticketService.GetAvailableTicketsByMovie(movieId);

        //    // Assert
        //    Assert.AreEqual(0, result); // No available tickets
        //}

        #endregion
    }
}
