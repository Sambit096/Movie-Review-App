using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Models;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Tools; // Include the ErrorDictionary namespace
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieReviewApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // GET: api/ticket
        [HttpGet(nameof(GetAllTickets))]
        public async Task<IActionResult> GetAllTickets()
        {
            try
            {
                var tickets = await _ticketService.GetAllTickets();
                if (tickets == null || !tickets.Any())
                {
                    return StatusCode(404, ErrorDictionary.ErrorLibrary[404]);
                }
                return Ok(tickets);
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
            }
        }

        // GET: api/ticket/movie/{movieId}
        [HttpGet("GetTickets/{movieId}")]
        public async Task<IActionResult> GetTickets(int movieId)
        {
            try
            {
                var tickets = await _ticketService.GetTickets(movieId);
                if (tickets == null || !tickets.Any())
                {
                    return StatusCode(404, ErrorDictionary.ErrorLibrary[404]);
                }
                return Ok(tickets);
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
            }
        }

        // POST: api/ticket
        // Add a ticket for a specific movie
        [HttpPost(nameof(AddTicket))]
        public async Task<IActionResult> AddTicket(int movieId, [FromBody] Ticket ticket) {
            try {
                var addedTicket = await _ticketService.AddTicket(movieId, ticket);
                return CreatedAtAction(nameof(GetTickets), new { movieId = movieId }, addedTicket);
            } 
            
            catch (KeyNotFoundException) {
                return NotFound(ErrorDictionary.ErrorLibrary[404]);
            } 
            
            catch (Exception) {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
            }
        }

        // PUT: api/ticket/{id}
        [HttpPut("EditTicket/{movieId}")]
        public async Task<IActionResult> EditTicket(int movieId, [FromBody] Ticket ticket)
        {
            try
            {
                var updated = await _ticketService.EditTicket(movieId, ticket);
                if (!updated)
                {
                    return StatusCode(404, ErrorDictionary.ErrorLibrary[404]); // Not Found
                }
                return NoContent(); // Successful update, no content to return
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500]); // Internal Server Error
            }
        }

        // DELETE: api/ticket/{id}
        [HttpDelete("RemoveTickets/{movieId}/{numberOfTickets}")]
        public async Task<IActionResult> RemoveTickets(int movieId, int numberOfTickets)
        {
            try
            {
                var removed = await _ticketService.RemoveTickets(movieId, numberOfTickets);
                if (!removed)
                {
                    return StatusCode(404, ErrorDictionary.ErrorLibrary[404]); // Not Found
                }
                return NoContent(); // Successful deletion, no content to return
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500]); // Internal Server Error
            }
        }
    }
}
