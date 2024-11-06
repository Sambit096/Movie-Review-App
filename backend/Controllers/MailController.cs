using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Tools;

namespace MovieReviewApp.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class MailController : ControllerBase {
        private readonly IMailService mailService;

        public MailController(IMailService mailService) {
            this.mailService = mailService;
        }

        [HttpPost(nameof(SendEmail))]
        public async Task<IActionResult> SendEmail(int cartId) {
            try {
                var result = await mailService.SendEmail(cartId);
                if (!result) {
                    return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
                }
                return Ok("Email Sent!");
            } catch (KeyNotFoundException) {
                return NotFound(ErrorDictionary.ErrorLibrary[400]);
            } catch (Exception) {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
            }
        }
    }
}
