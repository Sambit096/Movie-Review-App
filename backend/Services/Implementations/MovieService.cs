using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using System.Linq;

namespace MovieReviewApp.Services {
    public class MovieService : IMovieService {
        private static List<Movie> movies = new List<Movie>();

        public IList<Movie> GetMovies() {
            return movies.AsReadOnly();
        }

        public Movie GetMovieById(int id) {
            foreach(Movie movie in movies) {
                if (movie.MovieId == id) return movie;
            }
            return null;
        }

        public bool AddMovie(Movie newMovie) {
            movies.Add(newMovie);
            return true;
        }

        public bool RemoveMovie(Movie removingMovie) {
            movies.Remove(removingMovie);
            return true;
        }

        public bool EditMovie (Movie editedMovie) {
            if(movies.Contains(editedMovie)) {
                int index = movies.IndexOf(editedMovie);
                movies[index] = editedMovie;
                return true;
            }
            return false;
        }
    }
}