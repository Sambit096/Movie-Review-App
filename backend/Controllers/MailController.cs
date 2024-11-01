using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Models;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Data;

namespace MovieReviewApp.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class MailController : ControllerBase {
        private readonly IMailService mailService;

        public MailController(IMailService mailService) {
            this.mailService = mailService;
        }

        [HttpPost(nameof(SendEmail))]
        public async Task<IActionResult> SendEmail(int cartId)
        {
            try {
                var result = await mailService.SendEmail(cartId);
                if(!result) {
                    return StatusCode(500, $"Email send failed");
                }
                return Ok("Email Sent!");
            } catch (Exception error) {
                return StatusCode(500, $"Error sending email: {error}");
            }
        }
    }
}