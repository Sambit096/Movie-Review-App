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

        
    }
}
