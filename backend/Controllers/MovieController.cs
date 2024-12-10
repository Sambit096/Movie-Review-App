using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Models;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Tools; // Include the ErrorDictionary namespace

namespace MovieReviewApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase
{
    private readonly IMovieService movieService;

    public MovieController(IMovieService movieService)
    {
        this.movieService = movieService;
    }

    /// <summary>
    /// Gets all Movies from Database
    /// </summary>
    /// <returns></returns>
    [HttpGet(nameof(GetMovies))]
    public async Task<IActionResult> GetMovies()
    {
        try
        {
            var movies = await this.movieService.GetMovies();
            if (movies == null || !movies.Any())
            {
                return StatusCode(404, ErrorDictionary.ErrorLibrary[404]);
            }
            return Ok(movies);
        }
        catch (Exception)
        {
            return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
        }
    }

    /// <summary>
    /// Gets Movie from Database by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet(nameof(GetMovieById))]
    public async Task<IActionResult> GetMovieById(int id)
    {
        try
        {
            var movie = await this.movieService.GetMovieById(id);
            return Ok(movie);
        }
        catch (KeyNotFoundException)
        {
            return StatusCode(404, ErrorDictionary.ErrorLibrary[404]);
        }
        catch (Exception)
        {
            return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
        }
    }

}
