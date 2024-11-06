using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using System.Linq;
using MovieReviewApp.Data;
using MovieReviewApp.Tools;
using Microsoft.EntityFrameworkCore;

namespace MovieReviewApp.Services {

    public class CartService : ICartService {
        private readonly MovieReviewDbContext dbContext;

        public CartService(MovieReviewDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<CartWithTickets> GetCart(int? cartId = null) {
            var idTemp = cartId ?? -1;
            try {
                var cart = await dbContext.Carts.FirstOrDefaultAsync(c => c.CartId == cartId);
                if (cart == null) {
                    cart = new Cart { Total = 0.0 };
                    await dbContext.Carts.AddAsync(cart);
                    await dbContext.SaveChangesAsync();
                }
                List<Ticket> tickets = await dbContext.Tickets.Where(t => t.CartId == cart.CartId).ToListAsync();
                var result = new CartWithTickets { Cart = cart, Tickets = tickets };
                return result;
            } catch (Exception error) {
                throw new Exception(ErrorDictionary.ErrorLibrary[500] + " " + error.Message, error);
            }
        }

        public async Task<Cart> AddTicketToCart(int cartId, int ticketId, int quantity) {
            try { 
                var cart = await dbContext.Carts.FirstOrDefaultAsync(c => c.CartId == cartId);
                if (cart == null) {
                    throw new ArgumentException(ErrorDictionary.ErrorLibrary[400] + " Cart does not exist.");
                }
                var ticket = await dbContext.Tickets.FirstOrDefaultAsync(t => t.TicketId == ticketId);
                if (ticket == null) {
                    // Creating a new ticket if it does not exist
                    ticket = new Ticket { CartId = cart.CartId, TicketId = ticketId, ShowTimeId = 0, Price = 0.0, Availability = true };
                    await dbContext.Tickets.AddAsync(ticket);
                } else {
                    ticket.CartId = cart.CartId;
                    ticket.Availability = false;
                }
                cart.Total += ticket.Price * quantity;
                await dbContext.SaveChangesAsync();
                return cart;
            } catch (Exception error) {
                throw new Exception(ErrorDictionary.ErrorLibrary[501] + " " + error.Message, error);
            }
        }

        public async Task<Cart> RemoveTicketFromCart(int cartId, int ticketId) {
            try {
                var cart = await dbContext.Carts.FirstOrDefaultAsync(c => c.CartId == cartId);
                if (cart == null) {
                    throw new ArgumentException(ErrorDictionary.ErrorLibrary[400] + " Cart does not exist.");
                }
                var ticket = await dbContext.Tickets.FirstOrDefaultAsync(t => t.TicketId == ticketId);
                if (ticket == null) {
                    throw new ArgumentException(ErrorDictionary.ErrorLibrary[400] + " Ticket does not exist.");
                }
                dbContext.Tickets.Remove(ticket);
                await dbContext.SaveChangesAsync();
                return cart;
            } catch (Exception error) {
                throw new Exception(ErrorDictionary.ErrorLibrary[502] + " " + error.Message, error);
            }
        }

        public async Task<bool> ProcessPayment(int cartId, string cardNumber, string exp, string cardHolderName, string cvc) {
            try {
                if (!await dbContext.Carts.AnyAsync(c => c.CartId == cartId)) {
                    throw new Exception(ErrorDictionary.ErrorLibrary[404] + " Cart does not exist.");
                }

                var paymentGateway = await dbContext.PaymentGateways.FirstOrDefaultAsync(p => p.CardNumber == cardNumber);
                if (paymentGateway == null) {
                    ValidateNewCard(cardNumber, exp, cvc);

                    var newPayment = new PaymentGateway {
                        CardNumber = cardNumber, 
                        CardHolderName = cardHolderName, 
                        ExpirationDate = exp, 
                        CVC = cvc
                    };
                    await dbContext.PaymentGateways.AddAsync(newPayment);
                    await dbContext.SaveChangesAsync();
                } else {
                    ValidateExistingCard(paymentGateway, cardNumber, exp, cvc, cardHolderName);
                }

                return true;
            } catch (Exception error) {
                throw new Exception(ErrorDictionary.ErrorLibrary[501] + " " + error.Message, error);
            }
        }

        public async Task<bool> AddTicketsByShowtime(int cartId, int showtimeId, int quantity) {
            try {
                var tickets = await dbContext.Tickets
                    .Where(t => t.ShowTimeId == showtimeId && t.Availability == true)
                    .Take(quantity)
                    .ToListAsync();

                if (tickets.Count < quantity) {
                    throw new Exception(ErrorDictionary.ErrorLibrary[400] + " Not enough tickets available.");
                }

                foreach (var ticket in tickets) {
                    ticket.CartId = cartId;
                    ticket.Availability = false;
                }

                await dbContext.SaveChangesAsync();
                return true;
            } catch (Exception error) {
                throw new Exception(ErrorDictionary.ErrorLibrary[501] + " " + error.Message, error);
            }
        }

        private void ValidateNewCard(string cardNumber, string exp, string cvc) {
            if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length < 13 || cardNumber.Length > 19 || !cardNumber.All(char.IsDigit)) {
                throw new Exception(ErrorDictionary.ErrorLibrary[400] + " Invalid card number.");
            }
            if (string.IsNullOrWhiteSpace(cvc) || (cvc.Length != 3 && cvc.Length != 4) || !cvc.All(char.IsDigit)) {
                throw new Exception(ErrorDictionary.ErrorLibrary[400] + " Invalid CVC.");
            }
            if (!DateTime.TryParseExact(exp, "MM/yy", null, System.Globalization.DateTimeStyles.None, out DateTime expDate)) {
                throw new Exception(ErrorDictionary.ErrorLibrary[400] + " Invalid expiration date format. Should be MM/yy.");
            }
            expDate = expDate.AddMonths(1).AddDays(-1);
            if (expDate < DateTime.Now) {
                throw new Exception(ErrorDictionary.ErrorLibrary[400] + " Card is expired.");
            }
        }

        private void ValidateExistingCard(PaymentGateway paymentGateway, string cardNumber, string exp, string cvc, string cardHolderName) {
            if (paymentGateway.CardHolderName != cardHolderName || paymentGateway.CardNumber != cardNumber || paymentGateway.CVC != cvc || paymentGateway.ExpirationDate != exp) {
                throw new Exception(ErrorDictionary.ErrorLibrary[400] + " Invalid credentials for card.");
            }
        }
    }
}
