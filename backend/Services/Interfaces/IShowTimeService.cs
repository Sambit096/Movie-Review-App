using MovieReviewApp.Models;
namespace MovieReviewApp.Interfaces {
    public interface IShowTimeService {
        IList<ShowTime> GetAllShowTimes(); //Get all showtimes for every movie
        IList<ShowTime> GetShowTimes(int movieId); //Get all showtimes for a specific movie
        int GetTickets(int movieId); //Get all tickets for a specific movie
    }
}