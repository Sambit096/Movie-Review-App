using MovieReviewApp.Models;
namespace MovieReviewApp.Interfaces {
    public interface ITicketService {                  // Retrieve a single ticket by ID
        Task<IEnumerable<Ticket>> GetTickets(int movieId); // Get all tickets for a specific movie
        Task<IEnumerable<Ticket>> GetAllTickets();                  // Retrieve all tickets
        Task<Ticket> AddTicket(Ticket ticket);                      // Create a new ticket
        Task<bool> UpdateTicket(int ticketId, Ticket updatedTicket); // Update a ticket by ID
        Task<bool> DeleteTicket(int ticketId);                      // Delete a ticket by ID

    }
}