using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Tools;

namespace MovieReviewApp.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class SMSController : ControllerBase {
        private readonly ISMSService sMSService;

        public MailController(ISMSService sMSService) {
            this.sMSService = sMSService;
        }

        [HttpPost(nameof(SendText))]
        public async Task<IActionResult> SendText(int cartId) {
            try {
                var result = await sMSService.SendText(cartId);
                if (!result) {
                    return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
                }
                return Ok("Text Sent!");
            } catch (KeyNotFoundException) {
                return NotFound(ErrorDictionary.ErrorLibrary[400]);
            } catch (Exception) {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
            }
        }
    }
}
