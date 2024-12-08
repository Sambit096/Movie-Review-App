using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using System.Linq;
using MovieReviewApp.Data;
using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Tools;

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

        public async Task<Cart> AddTicketToCart(int cartId, int ticketId, int quantity) {
                try { 
                var cart = await dbContext.Carts.FirstOrDefaultAsync(c => c.CartId == cartId);
                if(cart == null) {
                    throw new ArgumentException("Cart does not exist.");
                }
                var ticket = await dbContext.Tickets.FirstOrDefaultAsync(t => t.TicketId == ticketId);
                if(ticket == null) {
                    throw new ArgumentException("Ticket does not exist.");
                } else {
                    ticket.CartId = cart.CartId;
                    //ticket availability updates when a ticket is in a cart. 
                    ticket.Availability = false;
                }
                //updated the cart total price
                cart.Total += ticket.Price * quantity;
                await dbContext.SaveChangesAsync();
                return cart;
            } catch (Exception error) {
                throw new Exception("Error when adding ticket to cart:", error);
            }

        }

        public async Task<Cart> RemoveTicketFromCart(int cartId, int ticketId) {
                try {
                var cart = await dbContext.Carts.FirstOrDefaultAsync(c => c.CartId == cartId);
                if(cart == null) {
                    throw new ArgumentException("Cart does not exist.");
                }
                var ticket = await dbContext.Tickets.FirstOrDefaultAsync(t => t.TicketId == ticketId && t.CartId == cartId);
                if(ticket == null) {
                    throw new ArgumentException("Ticket does not exist in cart.");
                }
                var updateTicket = await dbContext.Tickets
                    .Where(t => t.TicketId == ticketId)
                    .ExecuteUpdateAsync(u => u.SetProperty(t => t.Availability, true)
                    .SetProperty(t => t.CartId, (int?)null));
                var updateCart = await dbContext.Carts
                    .Where(c => c.CartId == cartId)
                    .ExecuteUpdateAsync(u => u.SetProperty(c => c.Total, c => c.Total - ticket.Price));
                await dbContext.SaveChangesAsync();
                //return a cart
                return cart;
            } catch (Exception error) {
                throw new Exception("Error when removing ticket from cart:", error);
            }

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
                var updateCart = await dbContext.Carts
                    .Where(c => c.CartId == cartId)
                    .ExecuteUpdateAsync(u => u.SetProperty(c => c.Purchased, true));
                dbContext.SaveChanges();
            } else {
                if(paymentGateway.CardHolderName != cardHolderName || paymentGateway.CardNumber != cardNumber || paymentGateway.CVC != cvc || paymentGateway.ExpirationDate != exp) {
                    throw new Exception("Invalid Credentials for card");
                }
                var updateCart = await dbContext.Carts
                    .Where(c => c.CartId == cartId)
                    .ExecuteUpdateAsync(u => u.SetProperty(c => c.Purchased, true));
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

        public async Task<int> GetCartIdByUser(int userId) {
            try {
                var userCheck = await dbContext.Users.SingleOrDefaultAsync(u => u.UserId == userId);
                if (userCheck == null) {
                    throw new Exception(ErrorDictionary.ErrorLibrary[404] + " User cannot be found.");
                }
                var currentCart = await dbContext.Carts.FirstOrDefaultAsync(c => c.UserId == userId && c.Purchased == false);
                if (currentCart == null) {
                    var newCart = await GetCart(userId);
                    var updateCart = await dbContext.Carts
                    .Where(c => c.CartId == newCart.Cart.CartId)
                    .ExecuteUpdateAsync(u => u.SetProperty(c => c.UserId, userId));
                    return newCart.Cart.CartId;
                } else {
                    return currentCart.CartId;
                }
            } catch (Exception error) {
                throw new Exception(ErrorDictionary.ErrorLibrary[500], error);
            }
        }

        public async Task<IList<CartWithTickets>> GetCompletedCartsByUser(int userId) {
            try {
                var userCheck = await dbContext.Users.SingleOrDefaultAsync(u => u.UserId == userId);
                if (userCheck == null) {
                    throw new Exception(ErrorDictionary.ErrorLibrary[404] + " User cannot be found.");
                }
                var carts = await dbContext.Carts.Where(c => c.UserId == userId && c.Purchased == true).ToListAsync();
                List<CartWithTickets> finalCarts = new List<CartWithTickets>();
                foreach (Cart cart in carts) {
                    List<Ticket> tickets = new List<Ticket>();
                    tickets = await dbContext.Tickets.Where(t => t.CartId == cart.CartId).ToListAsync();
                    var result = new CartWithTickets { Cart = cart, Tickets = tickets };
                    finalCarts.Add(result);
                }
                return finalCarts;
            } catch (Exception error) {
                throw new Exception(ErrorDictionary.ErrorLibrary[500], error);
            }
        }
    }
}