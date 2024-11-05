using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using System.Linq;
using MovieReviewApp.Data;
using Microsoft.EntityFrameworkCore;

namespace MovieReviewApp.Services {
    public class TicketService : ITicketService {
        private readonly MovieReviewDbContext _dbContext;
        public TicketService(MovieReviewDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets a ticket by movie ID from Database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<Ticket>> GetTickets(int movieId)
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

        /// <summary>
        /// Gets all tickets from the Database
        /// </summary>
        /// <returns>A collection of all tickets</returns>
        public async Task<IEnumerable<Ticket>> GetAllTickets()
        {
            return await _dbContext.Tickets.ToListAsync();
        }

        /// <summary>
        /// Adds a new ticket to the Database
        /// </summary>
        /// <param name="ticket">The ticket to add</param>
        /// <returns>The added ticket</returns>
        public async Task<Ticket> AddTicket(Ticket ticket)
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

        /// <summary>
        /// Updates an existing ticket in the Database
        /// </summary>
        /// <param name="ticketId">The ID of the ticket to update</param>
        /// <param name="updatedTicket">The updated ticket information</param>
        /// <returns>True if the ticket was updated, otherwise false</returns>
        public async Task<bool> UpdateTicket(int ticketId, Ticket updatedTicket)
        {
            var existingTicket = await _dbContext.Tickets.FindAsync(ticketId);
            if (existingTicket == null) return false;

            existingTicket.ShowTimeId = updatedTicket.ShowTimeId;
            existingTicket.Price = updatedTicket.Price;
            // existingTicket.Quantity = updatedTicket.Quantity;
            existingTicket.Availability = updatedTicket.Availability;
            
            _dbContext.Tickets.Update(existingTicket);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    
        /// <summary>
        /// Deletes a ticket by its ID from the Database
        /// </summary>
        /// <param name="ticketId">The ID of the ticket to delete</param>
        /// <returns>True if the ticket was deleted, otherwise false</returns>
        public async Task<bool> DeleteTicket(int ticketId)
        {
            var ticket = await _dbContext.Tickets.FindAsync(ticketId);
            if (ticket == null) return false;

            _dbContext.Tickets.Remove(ticket);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        
    }
}