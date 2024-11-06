using MovieReviewApp.Models;
namespace MovieReviewApp.Interfaces
{
    public interface ICartService
    {
        public Task<Cart> AddTicketToCart(int cartId, int ticketId, int quantity);
        public Task<Cart> RemoveTicketFromCart(int cartId, int ticketId);
        public Task<CartWithTickets> GetCart(int? cartId = null);
        public Task<bool> AddTicketsByShowtime(int cartId, int showtimeId, int quantity);
        public Task<bool> ProcessPayment(int cartId, string cardNumber, string exp, string cardHolderName, string cvc);
    }
}
