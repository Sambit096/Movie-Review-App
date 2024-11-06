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

        public async Task<CartWithTickets> GetCart(int? cartId = null) {

            var idTemp = cartId;
            if(cartId == null) {
                idTemp = -1;
            }
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
            return true;
        }

        public async Task<bool> RemoveTicketFromCart(int cartId, int ticketId) {
            return true;
        }

        public async Task<bool> ProcessPayment(int cartId, string cardNumber, string exp, string cardHolderName, string cvc){

            //checking to see if a cart exists
            var cartExists = await dbContext.Carts.AnyAsync(c => c.CartId == cartId);
            if (!cartExists) {
                throw new Exception("Cart does not exist.");
            }

            //checking if card exists in database
            var paymentGateway = await dbContext.PaymentGateways.FirstOrDefaultAsync(p => p.CardNumber == cardNumber);

            if(paymentGateway == null) {
                //checking validity of new card and adding to database
                if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length < 13 || cardNumber.Length > 19 || !cardNumber.All(char.IsDigit))  {
                    throw new Exception("Invalid card number");
                }

                if (string.IsNullOrWhiteSpace(cvc) || (cvc.Length != 3 && cvc.Length != 4) || !cvc.All(char.IsDigit)) {
                    throw new Exception("Invalid cvc");
                }

                if (!DateTime.TryParseExact(exp, "MM/yy", null, System.Globalization.DateTimeStyles.None, out DateTime expDate))
                {
                    throw new Exception("Invalid expiration date format. Should be MM/yy");
                }

                expDate = expDate.AddMonths(1).AddDays(-1);

                if(expDate < DateTime.Now) {
                    throw new Exception("Card is expired");
                }

                var newPayment = new PaymentGateway() { CardNumber = cardNumber, CardHolderName = cardHolderName, ExpirationDate = exp, CVC = cvc };
                await dbContext.PaymentGateways.AddAsync(newPayment);
                dbContext.SaveChanges();
            } else {
                if(paymentGateway.CardHolderName != cardHolderName || paymentGateway.CardNumber != cardNumber || paymentGateway.CVC != cvc || paymentGateway.ExpirationDate != exp) {
                    throw new Exception("Invalid Credentials for card");
                }
            }

            return true;
        }

        /*
        * Method to add a ticket to a cart by referencing showtime. This allows a single db fetch to be made instead of multiple
        */
        public async Task<bool> AddTicketsByShowtime(int cartId, int showtimeId, int quantity) {
            var tickets = await dbContext.Tickets.Where(t => t.ShowTimeId == showtimeId && t.Availability == true)
                            .Take(quantity)
                            .ToListAsync();

            if(tickets.Count < quantity)
            {
                throw new Exception("Not enough tickets available");
            }

            foreach(var ticket in tickets)
            {
                ticket.CartId = cartId;
                ticket.Availability = false;
            }

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}