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
        [HttpPost(nameof(CreateTicket))]
        public async Task<IActionResult> CreateTicket([FromBody] Ticket ticket)
        {
            try
            {
                var createdTicket = await _ticketService.AddTicket(ticket);
                if (createdTicket == null)
                {
                    return StatusCode(409, ErrorDictionary.ErrorLibrary[409]);
                }
                return CreatedAtAction(nameof(GetAllTickets), new { ticketId = createdTicket.TicketId }, createdTicket);
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
            }
        }

        // PUT: api/ticket/{id}
        [HttpPut("UpdateTicket/{id}")]
        public async Task<IActionResult> UpdateTicket(int id, [FromBody] Ticket ticket)
        {
            try
            {
                var updated = await _ticketService.UpdateTicket(id, ticket);
                if (!updated)
                {
                    return StatusCode(404, ErrorDictionary.ErrorLibrary[404]);
                }
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
            }
        }

        // DELETE: api/ticket/{id}
        [HttpDelete("DeleteTicket/{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            try
            {
                var deleted = await _ticketService.DeleteTicket(id);
                if (!deleted)
                {
                    return StatusCode(404, ErrorDictionary.ErrorLibrary[404]);
                }
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
            }
        }
    }
}
