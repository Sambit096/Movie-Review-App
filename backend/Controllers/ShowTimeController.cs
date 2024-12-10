using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Models;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Tools;

namespace MovieReviewApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShowTimeController : ControllerBase
{
    private readonly IShowTimeService showTimeService;

    public ShowTimeController(IShowTimeService showTimeService)
    {
        this.showTimeService = showTimeService;
    }

    /// <summary>
    /// Gets all ShowTimes
    /// </summary>
    /// <returns></returns>
    [HttpGet(nameof(GetAllShowTimes))]
    public async Task<IActionResult> GetAllShowTimes()
    {
        try
        {
            var showTimes = await this.showTimeService.GetAllShowTimes();
            if (showTimes == null || !showTimes.Any())
            {
                return StatusCode(404, ErrorDictionary.ErrorLibrary[404]);
            }
            return Ok(showTimes);
        }
        catch (Exception)
        {
            return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
        }
    }

    /// <summary>
    /// Gets all ShowTimes for a specific Movie based on Movie ID
    /// </summary>
    /// <param name="movieId"></param>
    /// <returns></returns>
    [HttpGet("GetShowTimes/{movieId}")]
    public async Task<IActionResult> GetShowTimes(int movieId)
    {
        try
        {
            var showTimes = await this.showTimeService.GetShowTimes(movieId);
            if (showTimes == null || !showTimes.Any())
            {
                return StatusCode(404, ErrorDictionary.ErrorLibrary[404]);
            }
            return Ok(showTimes);
        }
        catch (Exception)
        {
            return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
        }
    }

    /// <summary>
    /// Gets all Tickets for a specific Showtime
    /// </summary>
    /// <param name="showTimeId"></param>
    /// <returns></returns>
    [HttpGet("GetTicketsForShowTime/{showTimeId}")]
    public async Task<IActionResult> GetTicketsForShowTime(int showTimeId)
    {
        try
        {
            var tickets = await this.showTimeService.GetTicketsForShowTime(showTimeId);
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
