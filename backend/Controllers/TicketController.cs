using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Models;
using MovieReviewApp.Interfaces;
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllTickets();
            return Ok(tickets);
        }

        // GET: api/ticket/movie/{movieId}
        [HttpGet("movie/{movieId}")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets(int movieId)
        {
            var tickets = await _ticketService.GetTickets(movieId);
            if (tickets == null || !tickets.Any()) return NotFound("No tickets found for the specified movie.");

            return Ok(tickets);
        }

        // POST: api/ticket
        [HttpPost]
        public async Task<ActionResult<Ticket>> CreateTicket([FromBody] Ticket ticket)
        {
            var createdTicket = await _ticketService.AddTicket(ticket);
            return CreatedAtAction(nameof(GetAllTickets), new { ticketId = createdTicket.TicketId }, createdTicket);
        }

        // PUT: api/ticket/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(int id, [FromBody] Ticket ticket)
        {
            var updated = await _ticketService.UpdateTicket(id, ticket);
            if (!updated) return NotFound("Ticket not found.");

            return NoContent();
        }

        // DELETE: api/ticket/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var deleted = await _ticketService.DeleteTicket(id);
            if (!deleted) return NotFound("Ticket not found.");

            return NoContent();
        }
    }
}
