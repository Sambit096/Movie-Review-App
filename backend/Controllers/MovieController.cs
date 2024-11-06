using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Models;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Tools;

namespace MovieReviewApp.Controllers;
[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase {
    private readonly IMovieService movieService;

    public MovieController(IMovieService movieService) {
        this.movieService = movieService;
    }
    /// <summary>
    /// Gets all Movies from Database
    /// </summary>
    /// <returns></returns>
    [HttpGet(nameof(GetMovies))]
    public async Task<IActionResult> GetMovies() {
         try {
            var movies = await this.movieService.GetMovies();
            if (movies == null || !movies.Any()) {
                return NotFound(ErrorDictionary.ErrorLibrary[404] + "No movies found.");
            }
            return Ok(movies);
        } catch (Exception error) {
            return StatusCode(500, ErrorDictionary.ErrorLibrary[500] + error.Message);
        }
    }
    /// <summary>
    /// Gets Movie from Database by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet(nameof(GetMovieById))]
    public async Task<IActionResult> GetMovieById(int id) {
        try {
            var movie = await this.movieService.GetMovieById(id);
            if (movie == null) 
                return NotFound(ErrorDictionary.ErrorLibrary[400]);
            return Ok(movie);
        } catch (Exception error) {
             return StatusCode(500, ErrorDictionary.ErrorLibrary[500] + error.Message);
        }
        
    }
    /// <summary>
    /// Add movie to database 
    /// </summary>
    /// <param name="movie"></param>
    /// <returns></returns>
    [HttpPost(nameof(AddMovie))]
    public async Task<IActionResult> AddMovie(Movie movie) {
        try {
            var result = await this.movieService.AddMovie(movie);
            if (result) return Ok();
            return NoContent();
        } catch (Exception error) {
            return StatusCode(500, ErrorDictionary.ErrorLibrary[501] + error.Message);
        }
    }
    /// <summary>
    /// Remove movie from database with specific Id
    /// </summary>
    /// <param name="movie"></param>
    /// <returns></returns>
    [HttpDelete(nameof(RemoveMovie))]
    public async Task<IActionResult> RemoveMovie(Movie movie) {
        try {
            var result = await this.movieService.RemoveMovie(movie);
            if (result) return Ok();
            return NoContent();
        } catch (Exception error) {
            return StatusCode(500, ErrorDictionary.ErrorLibrary[502] + error.Message);
        }
    }
    /// <summary>
    /// Edit existing movie in Database
    /// </summary>
    /// <param name="movie"></param>
    /// <returns></returns>
    [HttpPut(nameof(EditMovie))]
    public async Task<IActionResult> EditMovie(Movie movie) {
        try {
            var result = await this.movieService.EditMovie(movie);
            if (result) return Ok();
            return NoContent();
        } catch (Exception error) {
            return StatusCode(500, ErrorDictionary.ErrorLibrary[500] + error.Message);
        }
    }
}
