using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using MovieReviewApp.Tools;

namespace MovieReviewApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagementController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IShowTimeService _showTimeService;
        private readonly ITicketService _ticketService;

        public ManagementController(IMovieService movieService,IShowTimeService showTimeService,ITicketService ticketService)
        {
            _movieService = movieService;
            _showTimeService = showTimeService;
            _ticketService = ticketService;
        }

        // Add Movie
        [HttpPost(nameof(AddMovie))]
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
        [HttpPost("AddTicketsToMovie/{movieId}")]
        public async Task<IActionResult> AddTicketsToMovie(int movieId, [FromBody] Ticket ticket)
        {
            try
            {
                var createdTicket = await _ticketService.AddTicket(movieId, ticket);
                return createdTicket != null
                    ? Ok("Tickets added successfully.")
                    : StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
            }
            catch (KeyNotFoundException)
            {
                return StatusCode(404, ErrorDictionary.ErrorLibrary[404]);
            }
        }

        // Remove Tickets from Movie
        [HttpDelete("RemoveTicketsFromMovie/{movieId}/{numberOfTickets}")]
        public async Task<IActionResult> RemoveTicketsFromMovie(int movieId, int numberOfTickets)
        {
            try
            {
                var result = await _ticketService.RemoveTickets(movieId, numberOfTickets);
                return result
                    ? NoContent()
                    : StatusCode(404, ErrorDictionary.ErrorLibrary[404]);
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
            }
        }

        // Edit Ticket Price
        [HttpPut("EditTickets/{movieId}")]
        public async Task<IActionResult> EditTickets(int movieId, [FromBody] Ticket ticket)
        {
            try
            {
                var result = await _ticketService.EditTicket(movieId, ticket);
                return result
                    ? NoContent()
                    : StatusCode(404, ErrorDictionary.ErrorLibrary[404]);
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
            }
        }
    }
}
