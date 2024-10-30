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

            var cartExists = await dbContext.Carts.AnyAsync(c => c.CartId == cartId);
            if (!cartExists) {
                throw new ArgumentException("Cart does not exist.");
            }
            
            if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length < 13 || cardNumber.Length > 19 || !cardNumber.All(char.IsDigit))  {
                throw new ArgumentException("Invalid card number");
            }

            if (string.IsNullOrWhiteSpace(cvc) || (cvc.Length != 3 && cvc.Length != 4) || !cvc.All(char.IsDigit)) {
                throw new ArgumentException("Invalid cvc");
            }

            if (!DateTime.TryParseExact(exp, "MM/yy", null, System.Globalization.DateTimeStyles.None, out DateTime expDate))
            {
                throw new ArgumentException("Invalid expiration date format. Should be MM/yy");
            }

            expDate = expDate.AddMonths(1).AddDays(-1);

            if(expDate < DateTime.Now) {
                throw new ArgumentException("Card is expired");
            }

            return true;
        }
    }
}