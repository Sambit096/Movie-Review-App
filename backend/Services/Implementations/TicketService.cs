using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using System.Linq;
using MovieReviewApp.Data;
using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Tools;

namespace MovieReviewApp.Services {
    public class TicketService : ITicketService {
        private readonly MovieReviewDbContext _dbContext;
        
        public TicketService(MovieReviewDbContext dbContext) {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets tickets by movie ID from the Database
        /// </summary>
        public async Task<IEnumerable<Ticket>> GetTickets(int movieId) {
            try {
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
            } catch (Exception error) {
                throw new Exception(ErrorDictionary.ErrorLibrary.GetValueOrDefault(404, "Error retrieving tickets for the movie"), error);
            }
        }

        /// <summary>
        /// Gets all tickets from the Database
        /// </summary>
        public async Task<IEnumerable<Ticket>> GetAllTickets() {
            try {
                return await _dbContext.Tickets.ToListAsync();
            } catch (Exception error) {
                throw new Exception(ErrorDictionary.ErrorLibrary.GetValueOrDefault(404, "Error retrieving all tickets"), error);
            }
        }

        /// <summary>
        /// Adds a new ticket to the Database
        /// </summary>
        public async Task<Ticket> AddTicket(Ticket ticket) {
            try {
                // Set default availability to true if not specified
                if (ticket.Availability == default) {
                    ticket.Availability = true;
                }

                await _dbContext.Tickets.AddAsync(ticket);
                await _dbContext.SaveChangesAsync();
                return ticket;
            } catch (Exception error) {
                throw new Exception(ErrorDictionary.ErrorLibrary.GetValueOrDefault(501, "Error adding ticket to database"), error);
            }
        }

        /// <summary>
        /// Updates an existing ticket in the Database
        /// </summary>
        public async Task<bool> UpdateTicket(int ticketId, Ticket updatedTicket) {
            try {
                var existingTicket = await _dbContext.Tickets.FindAsync(ticketId);
                if (existingTicket == null) {
                    throw new KeyNotFoundException(ErrorDictionary.ErrorLibrary.GetValueOrDefault(400, "Ticket not found"));
                }

                existingTicket.ShowTimeId = updatedTicket.ShowTimeId;
                existingTicket.Price = updatedTicket.Price;
                existingTicket.Availability = updatedTicket.Availability;

                _dbContext.Tickets.Update(existingTicket);
                await _dbContext.SaveChangesAsync();
                return true;
            } catch (KeyNotFoundException knfError) {
                throw knfError;
            } catch (Exception error) {
                throw new Exception(ErrorDictionary.ErrorLibrary.GetValueOrDefault(500, "Error updating ticket"), error);
            }
        }

        /// <summary>
        /// Deletes a ticket by its ID from the Database
        /// </summary>
        public async Task<bool> DeleteTicket(int ticketId) {
            try {
                var ticket = await _dbContext.Tickets.FindAsync(ticketId);
                if (ticket == null) {
                    throw new KeyNotFoundException(ErrorDictionary.ErrorLibrary.GetValueOrDefault(400, "Ticket not found"));
                }

                _dbContext.Tickets.Remove(ticket);
                await _dbContext.SaveChangesAsync();
                return true;
            } catch (KeyNotFoundException knfError) {
                throw knfError;
            } catch (Exception error) {
                throw new Exception(ErrorDictionary.ErrorLibrary.GetValueOrDefault(502, "Error deleting ticket"), error);
            }
        }
    }
}
