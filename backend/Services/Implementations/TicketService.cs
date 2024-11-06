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
        /// <returns>The added ticket</returns>
        public async Task<Ticket> AddTicket(Ticket ticket)
        {
            try
            {
                // Set default availability to true if not specified
                if (ticket.Availability == default)
                {
                    ticket.Availability = true;
                }

                await _dbContext.Tickets.AddAsync(ticket);
                await _dbContext.SaveChangesAsync();
                return ticket;
            }
            catch (DbUpdateException)
            {
                return null; // Conflict - item already exists or validation error
            }
            catch (Exception)
            {
                throw new Exception(ErrorDictionary.ErrorLibrary[500]);
            }
        }

        /// <summary>
        /// Updates an existing ticket in the database
        /// </summary>
        /// <param name="ticketId">The ID of the ticket to update</param>
        /// <param name="updatedTicket">The updated ticket information</param>
        /// <returns>True if the ticket was updated, otherwise false</returns>
        public async Task<bool> UpdateTicket(int ticketId, Ticket updatedTicket)
        {
            try
            {
                var existingTicket = await _dbContext.Tickets.FindAsync(ticketId);
                if (existingTicket == null)
                {
                    return false;
                }

                existingTicket.ShowTimeId = updatedTicket.ShowTimeId;
                existingTicket.Price = updatedTicket.Price;
                existingTicket.Availability = updatedTicket.Availability;

                _dbContext.Tickets.Update(existingTicket);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false; // Unprocessable Entity - validation error
            }
            catch (Exception)
            {
                throw new Exception(ErrorDictionary.ErrorLibrary[500]);
            }
        }

        /// <summary>
        /// Deletes a ticket by its ID from the database
        /// </summary>
        /// <param name="ticketId">The ID of the ticket to delete</param>
        /// <returns>True if the ticket was deleted, otherwise false</returns>
        public async Task<bool> DeleteTicket(int ticketId)
        {
            try
            {
                var ticket = await _dbContext.Tickets.FindAsync(ticketId);
                if (ticket == null)
                {
                    return false;
                }

                _dbContext.Tickets.Remove(ticket);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw new Exception(ErrorDictionary.ErrorLibrary[500]);
            }
        }
    }
}
