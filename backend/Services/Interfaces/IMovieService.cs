using MovieReviewApp.Models;
namespace MovieReviewApp.Interfaces {
    public interface IMovieService {
        public Task<IList<Movie>> GetMovies();
        public Task<Movie> GetMovieById(int id);
        public Task<bool> AddMovie(Movie movie);
        public Task<bool> RemoveMovie(Movie movie);
        Task<bool> EditMovie(Movie oldMovie, Movie newMovie);
    }
}