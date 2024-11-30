using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using MovieReviewApp.Data;
using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Tools; // Include the ErrorDictionary namespace
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
namespace MovieReviewApp.Services
{
    public class TicketService : ITicketService
    {
        private readonly MovieReviewDbContext _dbContext;
        private readonly ILogger<TicketService> _logger;

        public TicketService(MovieReviewDbContext dbContext,ILogger<TicketService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Gets tickets by movie ID from the database
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Ticket>> GetTickets(int movieId)
        {
            try
            {
                // Step 1: Retrieve ShowTimeIds associated with the specified MovieId
                var showTimeIds = await _dbContext.ShowTimes
                    .Where(st => st.MovieId == movieId)
                    .Select(st => st.ShowTimeId)
                    .ToListAsync();

                // Step 2: Retrieve Tickets associated with those ShowTimeIds
                var tickets = await _dbContext.Tickets
                    .Where(t => showTimeIds.Contains(t.ShowTimeId))
                    .ToListAsync();

                return tickets;
            }
            catch (Exception)
            {
                throw new Exception(ErrorDictionary.ErrorLibrary[500]);
            }
        }

        /// <summary>
        /// Gets all tickets from the database
        /// </summary>
        /// <returns>A collection of all tickets</returns>
        public async Task<IEnumerable<Ticket>> GetAllTickets()
        {
            try
            {
                return await _dbContext.Tickets.ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception(ErrorDictionary.ErrorLibrary[500]);
            }
        }

    /// <summary>
    /// Adds a new ticket to the database
    /// </summary>
    /// <param name="ticket">The ticket to add</param>
    ///<param name="movieId">The The movie ID for which to add the ticket for</param>
    /// <returns>The added ticket</returns>
    public async Task<bool> AddTicketsToMovie(int movieId, int numberOfTickets)
    {
        _logger.LogInformation("Attempting to add {NumberOfTickets} tickets to Movie ID {MovieId}.", numberOfTickets, movieId);

        try
        {
            // Fetch all showtimes for the movie
            var showTimes = await _dbContext.ShowTimes
                .Where(st => st.MovieId == movieId)
                .ToListAsync();

            if (!showTimes.Any())
            {
                _logger.LogWarning("No showtimes found for Movie ID {MovieId}.", movieId);
                throw new KeyNotFoundException("No showtimes found for the selected movie.");
            }

            _logger.LogInformation("{ShowTimeCount} showtimes found for Movie ID {MovieId}.", showTimes.Count, movieId);

            var ticketsToAdd = new List<Ticket>();

            // Create tickets for each showtime
            foreach (var showTime in showTimes)
            {
                for (int i = 0; i < numberOfTickets; i++)
                {
                    ticketsToAdd.Add(new Ticket
                    {
                        ShowTimeId = showTime.ShowTimeId,
                        Price = 15, // Default price, adjust as needed
                        Availability = true,
                    });
                }

                _logger.LogInformation("Prepared {NumberOfTickets} tickets for ShowTime ID {ShowTimeId}.", numberOfTickets, showTime.ShowTimeId);
            }

            _logger.LogInformation("Adding {TotalTickets} tickets to the database for Movie ID {MovieId}.", ticketsToAdd.Count, movieId);

            await _dbContext.Tickets.AddRangeAsync(ticketsToAdd);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Successfully added {TotalTickets} tickets to Movie ID {MovieId}.", ticketsToAdd.Count, movieId);

            return true;
        }
        catch (KeyNotFoundException knfEx)
        {
            _logger.LogError(knfEx, "KeyNotFoundException in AddTicketsToMovie for MovieId {MovieId}.", movieId);
            throw; // Propagate the exception to be handled by the controller
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while adding tickets to Movie ID {MovieId}.", movieId);
            throw new Exception(ErrorDictionary.ErrorLibrary[500], ex);
        }
    }




        /// <summary>
        /// Updates an existing ticket in the database
        /// </summary>
        /// <param name="movieId">The movie ID of the ticket to update</param>
        /// <param name="ticket">The updated ticket information</param>
        /// <returns>True if the ticket was updated, otherwise false</returns>
        public async Task<bool> EditTickets(int movieId, Ticket newTicketData)
        {
            try
            {
                // Validate that the new ShowTime exists and is associated with the movie
                var newShowTime = await _dbContext.ShowTimes
                    .FirstOrDefaultAsync(st => st.ShowTimeId == newTicketData.ShowTimeId && st.MovieId == movieId);

                if (newShowTime == null)
                {
                    throw new KeyNotFoundException($"ShowTime with ID {newTicketData.ShowTimeId} does not exist for Movie ID {movieId}.");
                }

                // Fetch all tickets associated with the movie
                var ticketsToEdit = await _dbContext.Tickets
                    .Include(t => t.ShowTime)
                    .Where(t => t.ShowTime.MovieId == movieId)
                    .ToListAsync();

                if (!ticketsToEdit.Any())
                {
                    throw new KeyNotFoundException("No tickets found for the specified movie.");
                }

                // Update each ticket's ShowTimeId, Price, and Availability
                foreach (var ticket in ticketsToEdit)
                {
                    ticket.ShowTimeId = newTicketData.ShowTimeId;
                    ticket.Price = newTicketData.Price;
                    ticket.Availability = newTicketData.Availability;
                    
                }

                _dbContext.Tickets.UpdateRange(ticketsToEdit);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception if logging is implemented
                // _logger.LogError(ex, "Error editing tickets for MovieId {MovieId}", movieId);
                throw new Exception(ErrorDictionary.ErrorLibrary[500], ex);
            }
        }



        /// <summary>
        /// Deletes a ticket by its ID from the database
        /// </summary>
        /// <param name="ticketId">The ID of the ticket to delete</param>
        ///<param name="movieId">The movie ID of the ticket to delete</param>
        /// <returns>True if the ticket was deleted, otherwise false</returns>
        public async Task<bool> RemoveTicketsFromMovie(int movieId, int numberOfTickets)
        {
            _logger.LogInformation("Attempting to remove {NumberOfTickets} tickets from Movie ID {MovieId}.", numberOfTickets, movieId);

            var ticketsToRemove = await _dbContext.Tickets
                .Include(t => t.ShowTime)
                .Where(t => t.ShowTime.MovieId == movieId && t.Availability == true)
                .OrderBy(t => t.TicketId)
                .Take(numberOfTickets)
                .ToListAsync();

            _logger.LogInformation("Fetched {Count} tickets to remove for Movie ID {MovieId}.", ticketsToRemove.Count, movieId);

            if (ticketsToRemove.Count < numberOfTickets)
            {
                _logger.LogWarning("Not enough available tickets to remove. Requested: {Requested}, Available: {Available}.", numberOfTickets, ticketsToRemove.Count);
                throw new InvalidOperationException("Not enough available tickets to remove.");
            }

            _dbContext.Tickets.RemoveRange(ticketsToRemove);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Successfully removed {Count} tickets from Movie ID {MovieId}.", ticketsToRemove.Count, movieId);

            return true;
        }


    public async Task<int> GetAvailableTicketsByMovie(int movieId)
    {
        _logger.LogInformation("Fetching available tickets for Movie ID {MovieId}.", movieId);

        var availableTickets = await _dbContext.Tickets
            .Include(t => t.ShowTime)
            .Where(t => t.ShowTime.MovieId == movieId && t.Availability == true)
            .CountAsync();

        _logger.LogInformation("Found {AvailableTickets} available tickets for Movie ID {MovieId}.", availableTickets, movieId);

        return availableTickets;
    }
        

    }
}
