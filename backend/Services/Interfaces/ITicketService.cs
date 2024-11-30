using MovieReviewApp.Models;
namespace MovieReviewApp.Interfaces {
    public interface ITicketService {                  // Retrieve a single ticket by ID
        Task<IEnumerable<Ticket>> GetTickets(int movieId); // Get all tickets for a specific movie
        Task<IEnumerable<Ticket>> GetAllTickets();                  // Retrieve all tickets
        
        Task<bool> AddTicketsToMovie(int movieId, int numberOfTickets);
        Task<bool> RemoveTicketsFromMovie(int movieId, int numberOfTickets);
        Task<bool> EditTickets(int movieId, Ticket newTicketData);
        Task<int> GetAvailableTicketsByMovie(int movieId);
        

    }
}