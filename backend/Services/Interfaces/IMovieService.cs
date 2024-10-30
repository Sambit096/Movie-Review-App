using MovieReviewApp.Models;
namespace MovieReviewApp.Interfaces {
    public interface IMovieService {
        IList<Movie> GetMovies();
        Movie GetMovieById(int id);
        bool AddMovie(Movie movie);
        bool RemoveMovie(Movie movie);
        bool EditMovie(Movie movie);
    }
}