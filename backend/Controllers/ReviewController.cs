using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Models;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Tools;
using MovieReviewApp.Migrations;

namespace MovieReviewApp.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ReviewController: ControllerBase
{
    private readonly IReviewService reviewService;

    public ReviewController(IReviewService reviewService)
    {
        this.reviewService = reviewService;
    }

    [HttpPost(nameof(AddReview))]
    public async Task<IActionResult> AddReview([FromQuery] Movie movie, [FromBody] Review review)
    {
        try 
        {
            var success = await this.reviewService.AddReview(movie, review);
            if(!success)
            {
                return StatusCode(400, ErrorDictionary.ErrorLibrary[400]);
            }
            return Ok("success");
        } catch (Exception)
        {
            return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
        }
    }

    [HttpDelete(nameof(RemoveReview))]
    public async Task<IActionResult> RemoveReview(Review review)
    {
        try
        {
            var success = await this.reviewService.RemoveReview(review);
            if(!success)
            {
                return StatusCode(400, ErrorDictionary.ErrorLibrary[400]);
            }
            return Ok("success");
        } catch (Exception)
        {
            return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
        }
    }

    [HttpGet(nameof(GetReviews))]
    public async Task<IActionResult> GetReviews(int movieId)
    {
        try
        {
            var reviews = await this.reviewService.GetReviews(movieId);
            if(reviews == null || !reviews.Any())
            {
                return Ok(reviews); 
            }
            return Ok(reviews);
        }
        catch (Exception)
        {
            return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
        }
    }

        [HttpGet(nameof(GetReviewsByUser))]
    public async Task<IActionResult> GetReviewsByUser(int userId)
    {
        try
        {
            var reviews = await this.reviewService.GetReviewsByUser(userId);
            if(reviews == null || !reviews.Any())
            {
                return Ok(reviews); 
            }
            return Ok(reviews);
        }
        catch (Exception)
        {
            return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
        }
    }

    [HttpPut(nameof(EditReview))]
    public async Task<IActionResult> EditReview(int reviewId, Review newReview)
    {
        try
        {
            var success = await this.reviewService.EditReview(reviewId, newReview);
            if(!success)
            {
                return StatusCode(404, ErrorDictionary.ErrorLibrary[404]);
            }
            return Ok("success");
        } catch (Exception)
        {
            return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
        }
    }

    [HttpPut(nameof(AddLike))]
    public async Task<IActionResult> AddLike(int reviewId)
    {
        try
        {
            var success = await this.reviewService.AddLike(reviewId);
            if(!success)
            {
                return StatusCode(404, ErrorDictionary.ErrorLibrary[404]);
            }
            return Ok("success");
        } catch (Exception)
        {
            return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
        }
    }

    
    [HttpPut(nameof(RemoveLike))]
    public async Task<IActionResult> RemoveLike(int reviewId)
    {
        try
        {
            var success = await this.reviewService.RemoveLike(reviewId);
            if(!success)
            {
                return StatusCode(404, ErrorDictionary.ErrorLibrary[404]);
            }
            return Ok("success");
        } catch (Exception)
        {
            return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
        }
    }
}