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

    [HttpGet(nameof(GetAllShowTimes))]
    public IActionResult GetAllShowTimes() {
        return Ok(this.showTimeService.GetAllShowTimes());
    }
    [HttpGet("GetShowTimes/{movieId}")]
    public IActionResult GetShowTimes(int movieId) {
        return Ok(this.showTimeService.GetShowTimes(movieId));
    }
    [HttpGet("GetTickets/{movieId}")]
    public IActionResult GetTickets(int movieId) {
        return Ok(this.showTimeService.GetTickets(movieId));
    }
}
