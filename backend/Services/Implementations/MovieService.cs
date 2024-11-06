using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using MovieReviewApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MovieReviewApp.Tools; // Include the ErrorDictionary namespace

namespace MovieReviewApp.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieReviewDbContext dbContext;

        public MovieService(MovieReviewDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IList<Movie>> GetMovies()
        {
            try
            {
                return await this.dbContext.Movies.ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception(ErrorDictionary.ErrorLibrary[500]);
            }
        }

        public async Task<Movie> GetMovieById(int movieId)
        {
            try
            {
                var movie = await this.dbContext.Movies.FirstOrDefaultAsync(m => m.MovieId == movieId);
                if (movie != null)
                    return movie;
                throw new KeyNotFoundException(ErrorDictionary.ErrorLibrary[404]);
            }
            catch (Exception ex) when (!(ex is KeyNotFoundException))
            {
                throw new Exception(ErrorDictionary.ErrorLibrary[500]);
            }
        }

        public async Task<bool> AddMovie(Movie newMovie)
        {
            try
            {
                await this.dbContext.Movies.AddAsync(newMovie);
                await this.dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                throw new Exception(ErrorDictionary.ErrorLibrary[409]); // Conflict - item already exists
            }
            catch (Exception)
            {
                throw new Exception(ErrorDictionary.ErrorLibrary[500]);
            }
        }

        public async Task<bool> RemoveMovie(Movie movieToRemove)
        {
            try
            {
                var existingMovie = await dbContext.Movies.FindAsync(movieToRemove.MovieId);
                if (existingMovie == null)
                {
                    throw new KeyNotFoundException(ErrorDictionary.ErrorLibrary[404]);
                }
                this.dbContext.Movies.Remove(existingMovie);
                await this.dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                throw new Exception(ErrorDictionary.ErrorLibrary[500]); // Could also use a specific code if applicable
            }
            catch (Exception ex) when (!(ex is KeyNotFoundException))
            {
                throw new Exception(ErrorDictionary.ErrorLibrary[500]);
            }
        }

        public async Task<bool> EditMovie(Movie editedMovie)
        {
            try
            {
                var existingMovie = await this.dbContext.Movies.FindAsync(editedMovie.MovieId);
                if (existingMovie == null)
                {
                    throw new KeyNotFoundException(ErrorDictionary.ErrorLibrary[404]);
                }

                existingMovie.Title = editedMovie.Title;
                existingMovie.Genre = editedMovie.Genre;
                existingMovie.Description = editedMovie.Description;
                existingMovie.Rating = editedMovie.Rating;

                this.dbContext.Movies.Update(existingMovie);
                await this.dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                throw new Exception(ErrorDictionary.ErrorLibrary[422]); // Unprocessable Entity - validation error
            }
            catch (Exception ex) when (!(ex is KeyNotFoundException))
            {
                throw new Exception(ErrorDictionary.ErrorLibrary[500]);
            }
        }
    }
}
