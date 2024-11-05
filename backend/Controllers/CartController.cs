using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Models;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Data;

namespace MovieReviewApp.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase {
    private readonly ICartService cartService;

    public CartController(ICartService cartService) {
        this.cartService = cartService;
    }

        [HttpPost(nameof(AddTicketToCart))]
        public async Task<IActionResult> AddTicketToCart(int cartId, int ticketId, int quantity)
        {
            try {
                var result = await cartService.AddTicketToCart(cartId, ticketId, quantity);
                if (result != null)
                {
                    return Ok(result);
                }
                return StatusCode(500, $"Unable to add ticket to cart.");
            } catch (Exception error) {
                return StatusCode(500, $"Error adding ticket to cart: {error}");
            }
        }

        [HttpPost(nameof(RemoveTicketFromCart))]
        public async Task<IActionResult> RemoveTicketFromCart(int cartId, int ticketId)
        {
            try {
                var result = await cartService.RemoveTicketFromCart(cartId, ticketId);
                if (result != null)
                {
                    return Ok(result);
                }
                return StatusCode(500, $"Unable to remove ticket from cart.");
            } catch (Exception error) {
                return StatusCode(500, $"Error removing ticket from cart: {error}");
            }
        }

    [HttpGet(nameof(GetCart))]
    public async Task<IActionResult> GetCart(int cartId) {
        try {
            var cart = await this.cartService.GetCart(cartId);
            if(cart == null) {
                ///create cart code
                return NotFound();
            }
            return Ok(cart);
        } catch (Exception error) {
            return StatusCode(500, $"Error gathering cart data: {error}");
        }
    }

    [HttpPost(nameof(ProcessPayment))]
    public async Task<IActionResult> ProcessPayment(int cartId, string cardNumber, string exp, string cardHolderName, string cvc) {
        try {
            var success = await this.cartService.ProcessPayment(cartId, cardNumber, exp, cardHolderName, cvc);
            if(!success) {
                return StatusCode(500, $"Payment failed");
            }
            return Ok("Payment Processed!");
        } catch (Exception error) {
            return StatusCode(500, $"Error when processing payment: {error}");
        }
    }
}

