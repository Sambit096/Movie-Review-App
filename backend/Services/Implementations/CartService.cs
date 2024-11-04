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

        public async Task<CartWithTickets> GetCart(int cartId) {
            try {
                var cart = await dbContext.Carts.FirstOrDefaultAsync(c => c.CartId == cartId);
                if(cart == null) {
                    cart = new Cart { Total = 0.0 };
                    await dbContext.Carts.AddAsync(cart);
                    await dbContext.SaveChangesAsync();
                }
                List<Ticket> tickets = new List<Ticket>();
                tickets = await dbContext.Tickets.Where(t => t.CartId == cart.CartId).ToListAsync();
                var result = new CartWithTickets { Cart = cart, Tickets = tickets };
                return result;
            } catch (Exception error) {
                throw new Exception("Error when getting cart:", error);
            }
        }

        public async Task<bool> AddTicketToCart(int cartId, int ticketId, int quantity) {
            //return true;
            try { 
                var cart = await dbContext.Carts.FirstOrDefaultAsync(c => c.CartId == cartId);
                if(cart == null) {
                    throw new ArgumentException("Cart does not exist.");
                }
                var ticket = await dbContext.Tickets.FirstOrDefaultAsync(t => t.TicketId == ticketId);
                if(ticket == null) {
                    ticket = new Ticket { TicketId = ticketId, CartId = cart.CartId, Quantity = 0 };
                    await dbContext.Tickets.AddAsync(ticket);
                } else {
                    ticket.CartId = cart.CartId;
                    ticket.Quantity = quantity;
                }
                await dbContext.SaveChangesAsync();
                return true;
            } catch (Exception error) {
                throw new Exception("Error when adding ticket to cart:", error);
            }
        }

        public async Task<bool> RemoveTicketFromCart(int cartId, int ticketId) {
            //return true;
            try {
                var cart = await dbContext.Carts.FirstOrDefaultAsync(c => c.CartId == cartId);
                if(cart == null) {
                    throw new ArgumentException("Cart does not exist.");
                }
                var ticket = await dbContext.Tickets.FirstOrDefaultAsync(t => t.TicketId == ticketId);
                if(ticket == null) {
                    throw new ArgumentException("Ticket does not exist.");
                }
                /*ticket.CartId = 0;
                ticket.Quantity = 0;*/
                dbContext.Tickets.Remove(ticket);
                await dbContext.SaveChangesAsync();
                return true;
            } catch (Exception error) {
                throw new Exception("Error when removing ticket from cart:", error);
            }
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