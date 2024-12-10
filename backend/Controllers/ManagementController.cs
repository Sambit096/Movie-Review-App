using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using MovieReviewApp.Tools;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;

namespace MovieReviewApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagementController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IShowTimeService _showTimeService;
        private readonly ITicketService _ticketService;
        private readonly ILogger<ManagementController> _logger;

        public ManagementController(IMovieService movieService, IShowTimeService showTimeService, ITicketService ticketService,ILogger<ManagementController> logger)
        {
            _movieService = movieService;
            _showTimeService = showTimeService;
            _ticketService = ticketService;
            _logger = logger;
        }

        [HttpGet("Management")]
        public IActionResult GetManagement()
        {
            return Ok("Management endpoint is live");
        }

        // Get all movies
        [HttpGet("GetAllMovies")]
        public async Task<IActionResult> GetAllMovies()
        {
            try
            {
                var movies = await _movieService.GetMovies();
                return Ok(movies);
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
            }
        }

        // Get showtimes by movie ID
        [HttpGet("GetShowTimesByMovie/{movieId}")]
        public async Task<IActionResult> GetShowTimesByMovie(int movieId)
        {
            try
            {
                var showTimes = await _showTimeService.GetShowTimes(movieId);
                return Ok(showTimes);
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
            }
        }

        
       // Add Movie
        [HttpPost("AddMovie")]
        public async Task<IActionResult> AddMovie([FromBody] Movie movie)
        {
            try
            {
                var result = await _movieService.AddMovie(movie);
                return result
                    ? Ok(new { message = "Movie added successfully." })
                    : StatusCode(500, new { message = ErrorDictionary.ErrorLibrary[501] });
            }
            catch (Exception ex)
            {
                if (ex.Message == ErrorDictionary.ErrorLibrary[409]) // Conflict - Duplicate
                {
                    return Conflict(new { message = ex.Message });
                }
                else
                {
                    return StatusCode(500, new { message = ErrorDictionary.ErrorLibrary[500] });
                }
            }
        }

        // Remove Movie
        [HttpDelete("RemoveMovie/{movieId}")]
        public async Task<IActionResult> RemoveMovie(int movieId)
        {
            try
            {
                // Initialize the Movie object with a placeholder Title
                var movie = new Movie { MovieId = movieId, Title = "To Be Removed" };

                var result = await _movieService.RemoveMovie(movie);
                return result
                    ? NoContent()
                    : StatusCode(404, ErrorDictionary.ErrorLibrary[404]);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(ErrorDictionary.ErrorLibrary[404]);
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
            }
        }

    [HttpPut("EditMovie")]
    public async Task<IActionResult> EditMovie([FromBody] EditMovieRequest request)
    {
        if (!ModelState.IsValid)
        {
            // Returns JSON with `errors`
            return BadRequest(ModelState);
        }

        try
        {
            var oldMovieExists = await _movieService.GetMovieById(request.OldMovie.MovieId);
            if (oldMovieExists == null)
            {
                // JSON NotFound
                return NotFound(new { Message = $"Movie with ID {request.OldMovie.MovieId} not found." });
            }

            var success = await _movieService.EditMovie(request.OldMovie, request.NewMovie);
            if (success)
            {
                // JSON success response
                return Ok(new { Message = "Movie edited successfully." });
            }
            else
            {
                // JSON error response
                return StatusCode(500, new { Message = "Failed to edit movie." });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in EditMovie.");
            // JSON error response for exceptions
            return StatusCode(500, new { Message = $"Internal Server Error: {ex.Message}" });
        }
    }



        // Add Showtime
        [HttpPost(nameof(AddShowTime))]
        public async Task<IActionResult> AddShowTime([FromBody] ShowTime showTime)
        {
            try
            {
                var result = await _showTimeService.AddShowTime(showTime);
                return result
                    ? Ok("Showtime added successfully.")
                    : StatusCode(500, ErrorDictionary.ErrorLibrary[501]);
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
            }
        }

        // Add Tickets to Movie
        // Add Tickets to Movie
    // Add Tickets to Movie
    [HttpPost("AddTicketsToMovie")]
    public async Task<IActionResult> AddTicketsToMovie([FromBody] AddTicketsToMovieRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            // Validate that the movie exists
            var movieExists = await _movieService.GetMovieById(request.Movie.MovieId);
            if (movieExists == null)
            {
                return NotFound($"Movie with ID {request.Movie.MovieId} not found.");
            }

            // Add tickets
            var success = await _ticketService.AddTicketsToMovie(request.Movie.MovieId, request.NumberOfTickets);
            if (success)
            {
                return Ok(new { Message = "Tickets added successfully!" });
            }
            else
            {
                return StatusCode(500, "Failed to add tickets.");
            }
        }
        catch (KeyNotFoundException knfEx)
        {
            _logger.LogError(knfEx, "KeyNotFoundException in AddTicketsToMovie.");
            return NotFound(knfEx.Message);
        }
        catch (DbUpdateException dbEx)
        {
            _logger.LogError(dbEx, "DbUpdateException in AddTicketsToMovie.");
            return StatusCode(500, $"Database Update Error: {dbEx.InnerException?.Message ?? dbEx.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in AddTicketsToMovie.");
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }


        // Remove Tickets from Movie
    [HttpPost("RemoveTicketsFromMovie")]
    public async Task<IActionResult> RemoveTicketsFromMovie([FromBody] RemoveTicketsRequest request)
    {
        if (!ModelState.IsValid)
        {
            // Returns JSON containing 'errors' by default
            return BadRequest(ModelState);
        }

        try
        {
            // Validate that the movie exists
            var movieExists = await _movieService.GetMovieById(request.Movie.MovieId);
            if (movieExists == null)
            {
                // Return JSON instead of plain text
                return NotFound(new { Message = $"Movie with ID {request.Movie.MovieId} not found." });
            }

            // Remove tickets
            var success = await _ticketService.RemoveTicketsFromMovie(request.Movie.MovieId, request.NumberOfTickets);
            if (success)
            {
                // Return JSON on success
                return Ok(new { Message = $"Successfully removed {request.NumberOfTickets} tickets." });
            }
            else
            {
                // Return JSON on error
                return StatusCode(500, new { Message = "Failed to remove tickets." });
            }
        }
        catch (InvalidOperationException ioeEx)
        {
            // Return JSON
            return BadRequest(new { Message = ioeEx.Message });
        }
        catch (Exception ex)
        {
            // Return JSON
            return StatusCode(500, new { Message = $"Internal Server Error: {ex.Message}" });
        }
    }

    [HttpPost("EditTickets")]
    public async Task<IActionResult> EditTickets([FromBody] EditTicketsRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Returns JSON with 'errors'
        }

        try
        {
            // Validate that the movie exists
            var movieExists = await _movieService.GetMovieById(request.Movie.MovieId);
            if (movieExists == null)
            {
                return NotFound(new { Message = $"Movie with ID {request.Movie.MovieId} not found." });
            }

            // Validate that the new showtime exists and belongs to the movie
            var newShowTime = await _showTimeService.GetShowTimeById(request.NewTicket.ShowTimeId);
            if (newShowTime == null || newShowTime.MovieId != request.Movie.MovieId)
            {
                return BadRequest(new { Message = $"ShowTime with ID {request.NewTicket.ShowTimeId} does not exist for Movie ID {request.Movie.MovieId}." });
            }

            // Edit tickets
            var success = await _ticketService.EditTickets(request.Movie.MovieId, request.NewTicket);
            if (success)
            {
                return Ok(new { Message = "Tickets updated successfully." });
            }
            else
            {
                return StatusCode(500, new { Message = "Failed to edit tickets." });
            }
        }
        catch (DbUpdateException dbEx)
        {
            _logger.LogError(dbEx, "DbUpdateException in EditTickets.");
            return StatusCode(500, new { Message = $"Database Update Error: {dbEx.InnerException?.Message ?? dbEx.Message}" });
        }
        catch (KeyNotFoundException knfEx)
        {
            _logger.LogError(knfEx, "KeyNotFoundException in EditTickets.");
            return NotFound(new { Message = knfEx.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in EditTickets.");
            return StatusCode(500, new { Message = $"Internal Server Error: {ex.Message}" });
        }
    }


        [HttpGet("GetAvailableTicketsByMovie/{movieId}")]
        public async Task<IActionResult> GetAvailableTicketsByMovie(int movieId)
        {
            _logger.LogInformation("Received request to get available tickets for Movie ID: {MovieId}", movieId);

            // Validate the movieId
            if (movieId <= 0)
            {
                _logger.LogWarning("Invalid Movie ID received: {MovieId}", movieId);
                return BadRequest("Invalid Movie ID.");
            }

            try
            {
                // Fetch the number of available tickets from the ticket service
                var availableTickets = await _ticketService.GetAvailableTicketsByMovie(movieId);
                _logger.LogInformation("Movie ID: {MovieId} has {AvailableTickets} available tickets.", movieId, availableTickets);

                // Return the result as a JSON object with PascalCase property names
                return Ok(new AvailableTicketsResponse
                {
                    MovieId = movieId,
                    AvailableTickets = availableTickets
                });
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("Movie not found: {MovieId}. Exception: {ExceptionMessage}", movieId, ex.Message);
                return NotFound(ex.Message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching available tickets for Movie ID: {MovieId}", movieId);
                return StatusCode(500, "An error occurred while fetching available tickets.");
            }
        }

        // Request Models

        /// <summary>
        /// Request model for adding tickets to a movie.
        /// </summary>
        public class AddTicketsToMovieRequest
        {
            
            public Movie Movie { get; set; }
            public int NumberOfTickets { get; set; }
        }

        /// <summary>
        /// Request model for removing tickets from a movie.
        /// </summary>
        public class RemoveTicketsRequest
        {
                public Movie Movie { get; set; }

                public int NumberOfTickets { get; set; }
        }

        /// <summary>
        /// Request model for editing tickets of a movie.
        /// </summary>
        public class EditTicketsRequest
        {
            public Movie Movie { get; set; }
            public Ticket NewTicket { get; set; }
        }

        public class EditMovieRequest
        {
            
            public Movie OldMovie { get; set; }
            public Movie NewMovie { get; set; }
        }

        // DTO class for the response
        public class AvailableTicketsResponse
        {
            [JsonPropertyName("MovieId")]
            public int MovieId { get; set; }

            [JsonPropertyName("AvailableTickets")]
            public int AvailableTickets { get; set; }
        }
    }
}
