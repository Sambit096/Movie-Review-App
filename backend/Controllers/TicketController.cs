using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Models;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Tools;
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
        public async Task<ActionResult<IEnumerable<Ticket>>> GetAllTickets()
        {
           try
            {
                var tickets = await _ticketService.GetAllTickets();
                if (tickets == null || !tickets.Any())
                    return NotFound(ErrorDictionary.ErrorLibrary[404] + "No tickets found.");
                
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500] + ex.Message);
            }
        }

        // GET: api/ticket/movie/{movieId}
        [HttpGet("GetTickets/{movieId}")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets(int movieId)
        {
            try
            {
                var tickets = await _ticketService.GetTickets(movieId);
                if (tickets == null || !tickets.Any())
                    return NotFound(ErrorDictionary.ErrorLibrary[400]);

                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500] + ex.Message);
            }
        }

        // POST: api/ticket
        [HttpPost(nameof(CreateTicket))]
        public async Task<ActionResult<Ticket>> CreateTicket([FromBody] Ticket ticket)
        {
            try
            {
                var createdTicket = await _ticketService.AddTicket(ticket);
                return CreatedAtAction(nameof(GetAllTickets), new { ticketId = createdTicket.TicketId }, createdTicket);
            }
            catch (Exception ex)
            {
                return StatusCode(501, ErrorDictionary.ErrorLibrary[501] + ex.Message);
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
                    return NotFound(ErrorDictionary.ErrorLibrary[400]);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500] + ex.Message);
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
                    return NotFound(ErrorDictionary.ErrorLibrary[400]);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(502, ErrorDictionary.ErrorLibrary[502] + ex.Message);
            }
        }
    }
}
