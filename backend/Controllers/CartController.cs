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