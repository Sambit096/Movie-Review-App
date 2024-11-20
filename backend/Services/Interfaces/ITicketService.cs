using MovieReviewApp.Models;
namespace MovieReviewApp.Interfaces {
    public interface ITicketService {                  // Retrieve a single ticket by ID
        Task<IEnumerable<Ticket>> GetTickets(int movieId); // Get all tickets for a specific movie
        Task<IEnumerable<Ticket>> GetAllTickets();                  // Retrieve all tickets
        Task<Ticket> AddTicket(int movieId, Ticket ticket);                      // Create a new ticket
        Task<bool> RemoveTickets(int movieId, int numberOfTickets);
        Task<bool> EditTicket(int movieId, Ticket ticket);

    }
}