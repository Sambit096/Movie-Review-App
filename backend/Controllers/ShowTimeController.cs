using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Models;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Data;

namespace MovieReviewApp.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ShowTimeController : ControllerBase {
    private readonly IShowTimeService showTimeService;

    public ShowTimeController(IShowTimeService showTimeService) {
        this.showTimeService = showTimeService;
    }

    /// <summary>
    /// Gets all ShowTimes
    /// </summary>
    /// <returns></returns>
    [HttpGet(nameof(GetAllShowTimes))]
    public async Task<IActionResult> GetAllShowTimes() {
        try {
            var showTimes = await this.showTimeService.GetAllShowTimes();
            if (showTimes == null || !showTimes.Any()) {
                return NotFound();
            }
            return Ok(showTimes);
        } catch (Exception error) {
            return StatusCode(500, $"Error when receiving ShowTime Data: {error}");
        }
    }
    /// <summary>
    /// Gets all ShowTimes for a specific Movie based on Movie ID
    /// </summary>
    /// <param name="movieId"></param>
    /// <returns></returns>
    [HttpGet("GetShowTimes/{movieId}")]
    public async Task<IActionResult> GetShowTimes(int movieId) {
        try {
            var showTimes = await this.showTimeService.GetShowTimes(movieId);
            if (showTimes == null || !showTimes.Any()) {
                return NotFound();
            }
            return Ok(showTimes);
        } catch (Exception error) {
            return StatusCode(500, $"Error when receiving ShowTime Data for movieId: {error}");
        }
        
    }
    /// <summary>
    /// Gets all Tickets for a specific movie (adds all from ShowTimes)
    /// </summary>
    /// <param name="showTimeId"></param>
    /// <returns></returns>
    [HttpGet("GetTicketsForShowTime/{showTimeId}")]
    public async Task<IActionResult> GetTicketsForShowTime(int showTimeId) {
        try {
            var tickets = await this.showTimeService.GetTicketsForShowTime(showTimeId);
            if (tickets == null) {
                return NotFound("No Tickets with this showTimeId were found.");
            } 
            return Ok(tickets);
        } catch (Exception error) {
            return StatusCode(500, $"Error when receiving Ticket Data: {error}");
        }
    }*/
}
