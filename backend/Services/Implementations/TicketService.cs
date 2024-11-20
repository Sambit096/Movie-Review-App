using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using MovieReviewApp.Data;
using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Tools; // Include the ErrorDictionary namespace
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MovieReviewApp.Services
{
    public class TicketService : ITicketService
    {
        private readonly MovieReviewDbContext _dbContext;

        public TicketService(MovieReviewDbContext dbContext)
        {
            _dbContext = dbContext;
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
        public async Task<Ticket> AddTicket(int movieId, Ticket ticket) {
            try {
                var showTime = await _dbContext.ShowTimes
                    .FirstOrDefaultAsync(st => st.MovieId == movieId);

                if (showTime == null) {
                    throw new KeyNotFoundException(ErrorDictionary.ErrorLibrary[404]);
                }

                ticket.ShowTimeId = showTime.ShowTimeId;
                ticket.Availability = true;

                await _dbContext.Tickets.AddAsync(ticket);
                await _dbContext.SaveChangesAsync();

                return ticket;
            } catch (Exception) {
                throw new Exception(ErrorDictionary.ErrorLibrary[500]);
            }
        }


        /// <summary>
        /// Updates an existing ticket in the database
        /// </summary>
        /// <param name="movieId">The movie ID of the ticket to update</param>
        /// <param name="ticket">The updated ticket information</param>
        /// <returns>True if the ticket was updated, otherwise false</returns>
        public async Task<bool> EditTicket(int movieId, Ticket ticket) {
            try {
                var existingTicket = await _dbContext.Tickets
                    .FirstOrDefaultAsync(t => t.ShowTime.MovieId == movieId);

                if (existingTicket == null) {
                    throw new KeyNotFoundException(ErrorDictionary.ErrorLibrary[404]);
                }

                existingTicket.Price = ticket.Price;
                existingTicket.Availability = ticket.Availability;

                _dbContext.Tickets.Update(existingTicket);
                await _dbContext.SaveChangesAsync();

                return true;
            } catch (Exception) {
                throw new Exception(ErrorDictionary.ErrorLibrary[500]);
            }
        }

        /// <summary>
        /// Deletes a ticket by its ID from the database
        /// </summary>
        /// <param name="ticketId">The ID of the ticket to delete</param>
        ///<param name="movieId">The movie ID of the ticket to delete</param>
        /// <returns>True if the ticket was deleted, otherwise false</returns>
         public async Task<bool> RemoveTickets(int movieId, int numberOfTickets) {
            try {
                var tickets = await _dbContext.Tickets
                    .Where(t => t.ShowTime.MovieId == movieId && t.Availability)
                    .Take(numberOfTickets)
                    .ToListAsync();

                if (tickets.Count < numberOfTickets) {
                    throw new KeyNotFoundException(ErrorDictionary.ErrorLibrary[404]);
                }

                _dbContext.Tickets.RemoveRange(tickets);
                await _dbContext.SaveChangesAsync();

                return true;
            } catch (Exception) {
                throw new Exception(ErrorDictionary.ErrorLibrary[500]);
            }
        }
    }
}
