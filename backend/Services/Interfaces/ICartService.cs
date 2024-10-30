using MovieReviewApp.Models;
namespace MovieReviewApp.Interfaces
{
    public interface ICartService
    {
        public Task<bool> AddTicketToCart(int cartId, int ticketId, int quantity);
        public Task<bool> RemoveTicketFromCart(int cartId, int ticketId);
        public Task<Cart> GetCart(int cartId);
        public Task<bool> ProcessPayment(int cartId, string cardNumber, string exp, string cardHolderName, string cvc);
    }
}
