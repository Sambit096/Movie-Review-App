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
                    ? Ok("Movie added successfully.")
                    : StatusCode(500, ErrorDictionary.ErrorLibrary[501]);
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
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

        // Edit Movie
        [HttpPut("EditMovie/{movieId}")]
        public async Task<IActionResult> EditMovie(int movieId, [FromBody] Movie movie)
        {
            try
            {
                var result = await _movieService.EditMovie(movie);
                return result
                    ? NoContent()
                    : StatusCode(404, ErrorDictionary.ErrorLibrary[404]);
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
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
    public async Task<IActionResult> AddTicketsToMovie([FromBody] AddTicketsRequest request)
    {
        if (request == null)
        {
            return BadRequest("Request cannot be null.");
        }

        if (request.MovieId <= 0)
        {
            return BadRequest("Invalid MovieId.");
        }

        if (request.NumberOfTickets <= 0)
        {
            return BadRequest("Number of tickets must be greater than zero.");
        }

        try
        {
            // Attempt to retrieve the movie by its ID
            var movieExists = await _movieService.GetMovieById(request.MovieId);

            // If the movie does not exist, return a 404 Not Found response
            if (movieExists == null)
            {
                return NotFound($"Movie with ID {request.MovieId} not found.");
            }

            // Attempt to add tickets to the movie via the TicketService
            var success = await _ticketService.AddTicketsToMovie(request.MovieId, request.NumberOfTickets);

            if (success)
            {
                return Ok("Tickets added successfully to all showtimes of the selected movie.");
            }
            else
            {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
            }
        }
        catch (KeyNotFoundException knfEx)
        {
            // Handle cases where related entities might not be found
            return NotFound(knfEx.Message);
        }
        catch (DbUpdateException dbEx)
        {
            // Specifically catch database update exceptions to provide more context
            return StatusCode(500, $"Database Update Error: {dbEx.InnerException?.Message ?? dbEx.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected exceptions
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }


        // Remove Tickets from Movie
    [HttpPost("RemoveTicketsFromMovie")]
    public async Task<IActionResult> RemoveTicketsFromMovie([FromBody] RemoveTicketsRequest request)
    {
        if (request == null)
        {
            return BadRequest("Request cannot be null.");
        }

        if (request.MovieId <= 0)
        {
            return BadRequest("Invalid MovieId.");
        }

        if (request.NumberOfTickets <= 0)
        {
            return BadRequest("Number of tickets must be greater than zero.");
        }

        var movieExists = await _movieService.GetMovieById(request.MovieId);
        if (movieExists == null)
        {
            return NotFound($"Movie with ID {request.MovieId} not found.");
        }

        try
        {
            var success = await _ticketService.RemoveTicketsFromMovie(request.MovieId, request.NumberOfTickets);
            if (success)
            {
                return Ok($"Successfully removed {request.NumberOfTickets} tickets from all showtimes of movie ID {request.MovieId}.");
            }
            else
            {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
            }
        }
        catch (InvalidOperationException ioeEx)
        {
            return BadRequest(ioeEx.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
        }
    }

    [HttpPost("EditTickets")]
        public async Task<IActionResult> EditTickets([FromBody] EditTicketsRequest request)
        {
            // Validate the request object
            if (request == null)
            {
                return BadRequest("Request cannot be null.");
            }

            if (request.MovieId <= 0)
            {
                return BadRequest("Invalid MovieId.");
            }

            if (request.Price < 0)
            {
                return BadRequest("Price cannot be negative.");
            }

            if (request.ShowTimeId <= 0)
            {
                return BadRequest("Invalid ShowTimeId.");
            }

            try
            {
                // Attempt to retrieve the movie by its ID
                var movieExists = await _movieService.GetMovieById(request.MovieId);

                // If the movie does not exist, return a 404 Not Found response
                if (movieExists == null)
                {
                    return NotFound($"Movie with ID {request.MovieId} not found.");
                }

                // Attempt to retrieve the new showtime by its ID
                var newShowTime = await _showTimeService.GetShowTimeById(request.ShowTimeId);

                // Validate that the new showtime exists and is associated with the specified movie
                if (newShowTime == null || newShowTime.MovieId != request.MovieId)
                {
                    return BadRequest($"ShowTime with ID {request.ShowTimeId} does not exist for Movie ID {request.MovieId}.");
                }

                // Create a new Ticket object with the updated data
                var updatedTicketData = new Ticket
                {
                    ShowTimeId = request.ShowTimeId,
                    Price = (double)request.Price,
                    Availability = request.Availability
                    
                };

                // Attempt to edit tickets via the TicketService
                var success = await _ticketService.EditTickets(request.MovieId, updatedTicketData);

                if (success)
                {
                    return Ok("Tickets updated successfully.");
                }
                else
                {
                    return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
                }
            }
            catch (DbUpdateException dbEx)
            {
                // Handle database update exceptions, such as foreign key conflicts
                return StatusCode(500, $"Database Update Error: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (KeyNotFoundException knfEx)
            {
                // Handle cases where related entities might not be found
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                // Handle any other unexpected exceptions
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
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
        public class AddTicketsRequest
        {
            public int MovieId { get; set; }
            public int NumberOfTickets { get; set; }
        }

        /// <summary>
        /// Request model for removing tickets from a movie.
        /// </summary>
        public class RemoveTicketsRequest
        {
            public int MovieId { get; set; }
            public int NumberOfTickets { get; set; }
        }

        /// <summary>
        /// Request model for editing tickets of a movie.
        /// </summary>
        public class EditTicketsRequest
        {
            public int MovieId { get; set; }
            public decimal Price { get; set; }
            public bool Availability { get; set; }
            public int ShowTimeId { get; set; }    
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
