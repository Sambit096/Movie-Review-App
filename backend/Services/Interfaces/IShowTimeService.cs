using MovieReviewApp.Models;
namespace MovieReviewApp.Interfaces {
    public interface IShowTimeService {
        public Task<IList<ShowTime>> GetAllShowTimes(); //Get all showtimes for every movie
        public Task<IList<ShowTime>> GetShowTimes(int movieId); //Get all showtimes for a specific movie
        public Task<IList<Ticket>> GetTicketsForShowTime(int showTimeId); //Get all tickets for a specific showtime
        public Task<bool> AddShowTime(ShowTime showTime); //Add new Showtime

         Task<ShowTime> GetShowTimeById(int showTimeId); // Returns The ShowTime object if found; otherwise, null.
    } 
}