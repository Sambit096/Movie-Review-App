using MovieReviewApp.Models;
namespace MovieReviewApp.Interfaces {
    public interface IShowTimeService {
        public Task<IList<ShowTime>> GetAllShowTimes(); //Get all showtimes for every movie
        public Task<IList<ShowTime>> GetShowTimes(int movieId); //Get all showtimes for a specific movie
        //public Task<int> GetTickets(int movieId); //Get all tickets for a specific movie
    }
}