using MovieReviewApp.Interfaces;
using MovieReviewApp.backend.Models;
using System.Linq;

namespace MovieReviewApp.Services {
    public class MovieService : IMovieService {
        private static List<Movie> movies = new List<Movie>();

        public IList<Movie> GetMovies() {
            return movies.AsReadOnly();
        }

        public bool AddMovie(Movie newMovie) {
            movies.Add(newMovie);
            return true;
        }

        public bool RemoveMovie(Movie removingMovie) {
            return true;
        }

        public bool EditMovie (Movie editedMovie) {
            return true;
        }
    }
}