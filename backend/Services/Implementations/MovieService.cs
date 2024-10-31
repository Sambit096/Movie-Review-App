using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using System.Linq;
using MovieReviewApp.Data;
using Microsoft.EntityFrameworkCore;

namespace MovieReviewApp.Services {
    public class MovieService : IMovieService {

        private readonly MovieReviewDbContext dbContext;

        public MovieService(MovieReviewDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<IList<Movie>> GetMovies() {
            try {
                var movies = await this.dbContext.Movies.ToListAsync();
                return movies;
            } catch (Exception error) {
                throw new Exception("Error when retreiving Movies from Database: ", error);
            }
        }

        public async Task<Movie> GetMovieById(int movieId) {
            try {
                var movie = await (from m in dbContext.Movies
                where m.MovieId == movieId
                select m).FirstOrDefaultAsync();
                if (movie != null)
                    return movie;
                return null;
            } catch (Exception error) {
                throw new Exception($"Error when retreiving Movie with Id {movieId} from Database: ", error);
            }
        }

        public async Task<bool> AddMovie(Movie newMovie) {
            try {
                await this.dbContext.Movies.AddAsync(newMovie);
                await this.dbContext.SaveChangesAsync();
                return true;
            } catch (Exception error) {
                throw new Exception($"Error when adding Movie to Database: ", error);
            }
        }

        public async Task<bool> RemoveMovie(Movie movieToRemove) {
            try {
                this.dbContext.Movies.Remove(movieToRemove);
                await this.dbContext.SaveChangesAsync();
                return true;
            } catch (Exception error) {
                throw new Exception($"Error when removing Movie from Database: ", error);
            }
        }

        public async Task<bool> EditMovie (Movie editedMovie) {
            try {
                var existingMovie = await this.dbContext.Movies.FindAsync(editedMovie.MovieId);
                if (existingMovie == null) return false;

                existingMovie.Title = editedMovie.Title;
                existingMovie.Genre = editedMovie.Genre;
                existingMovie.Description = editedMovie.Description;
                existingMovie.Rating = editedMovie.Rating;

                this.dbContext.Movies.Update(existingMovie);
                await this.dbContext.SaveChangesAsync();
                return true;
            } catch (Exception error) {
                throw new Exception($"Error when editing Movie: ", error);
            }
        }
    }
}