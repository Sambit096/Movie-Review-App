using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.backend.Models;
using MovieReviewApp.Interfaces;
using System.Web.Http.Cors;

namespace MovieReviewApp.Controllers;
[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase {
    private readonly IMovieService movieService;

    public MovieController(IMovieService movieService) {
        this.movieService = movieService;
    }

    [HttpGet(nameof(GetMovies))]
    public IList<Movie> GetMovies() {
        return movieService.GetMovies();
    }

    [HttpPost(nameof(AddMovie))]
    public bool AddMovie(Movie movie) {
        return movieService.AddMovie(movie);
    }

    [HttpDelete(nameof(RemoveMovie))]
    public bool RemoveMovie(Movie movie) {
        return movieService.RemoveMovie(movie);
    }
}
