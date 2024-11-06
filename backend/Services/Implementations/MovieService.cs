using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using System.Linq;
using MovieReviewApp.Data;
using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Tools;

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
                throw new Exception(ErrorDictionary.ErrorLibrary[500] + "Error when retrieving Movies from Database.", error);
            }
        }

        public async Task<Movie> GetMovieById(int movieId) {
            try {
                var movie = await dbContext.Movies.FirstOrDefaultAsync(m => m.MovieId == movieId);
                if (movie == null)
                    throw new KeyNotFoundException(ErrorDictionary.ErrorLibrary[404] + $"Movie with Id {movieId} not found.");
                
                return movie;
            } catch (KeyNotFoundException keyError) {
                throw keyError; // Retain specific KeyNotFoundException for higher-level handling if needed
            } catch (Exception error) {
                throw new Exception(ErrorDictionary.ErrorLibrary[500] + $"Error when retrieving Movie with Id {movieId} from Database.", error);
            }
        }

        public async Task<bool> AddMovie(Movie newMovie) {
            try {
                await this.dbContext.Movies.AddAsync(newMovie);
                await this.dbContext.SaveChangesAsync();
                return true;
            } catch (Exception error) {
                throw new Exception(ErrorDictionary.ErrorLibrary[501] + "Error when adding Movie to Database.", error);
            }
        }

        public async Task<bool> RemoveMovie(Movie movieToRemove) {
            try {
                this.dbContext.Movies.Remove(movieToRemove);
                await this.dbContext.SaveChangesAsync();
                return true;
            } catch (Exception error) {
                throw new Exception(ErrorDictionary.ErrorLibrary[502] + "Error when removing Movie from Database.", error);
            }
        }

        public async Task<bool> EditMovie(Movie editedMovie) {
            try {
                var existingMovie = await this.dbContext.Movies.FindAsync(editedMovie.MovieId);
                if (existingMovie == null) 
                    throw new KeyNotFoundException(ErrorDictionary.ErrorLibrary[404] + $"Movie with Id {editedMovie.MovieId} not found.");

                existingMovie.Title = editedMovie.Title;
                existingMovie.Genre = editedMovie.Genre;
                existingMovie.Description = editedMovie.Description;
                existingMovie.Rating = editedMovie.Rating;

                this.dbContext.Movies.Update(existingMovie);
                await this.dbContext.SaveChangesAsync();
                return true;
            } catch (KeyNotFoundException keyError) {
                throw keyError; // Handle specific not-found error gracefully
            } catch (Exception error) {
                throw new Exception(ErrorDictionary.ErrorLibrary[500] + "Error when editing Movie.", error);
            }
        }
    }
}
