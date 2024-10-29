using MovieReviewApp.backend.Models;
namespace MovieReviewApp.Interfaces {
    public interface IMovieService {
        IList<Movie> GetMovies();
        bool AddMovie(Movie movie);
        bool RemoveMovie(Movie movie);
        bool EditMovie(Movie movie);
    }
}