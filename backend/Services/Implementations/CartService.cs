using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using System.Linq;
using MovieReviewApp.Data;
using Microsoft.EntityFrameworkCore;

namespace MovieReviewApp.Services {

    public class CartService : ICartService {
        private readonly MovieReviewDbContext dbContext;

        public CartService(MovieReviewDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<Cart> GetCart(int cartId) {
            try {
                var cart = await dbContext.Carts.FirstOrDefaultAsync(c => c.CartId == cartId);

                if(cart == null) {
                    cart = new Cart { Total = 0.0 };
                    await dbContext.Carts.AddAsync(cart);
                    await dbContext.SaveChangesAsync();
                }

                return cart;
            } catch (Exception error) {
                throw new Exception("Error when getting cart:", error);
            }
        }

        public async Task<bool> AddTicketToCart(int cartId, int ticketId, int quantity) {
            return true;
        }

        public async Task<bool> RemoveTicketFromCart(int cartId, int ticketId) {
            return true;
        }

        public async Task<bool> ProcessPayment(int cartId, string cardNumber, string exp, string cardHolderName, string cvc){
            return true;
        }
    }
}