using MovieReviewApp.models;
namespace MovieReviewApp.interfaces
{
    public interface ICart
    {
        int CartId { get; set; }
        List<Ticket> Tickets { get; set; }
        double Total { get; set; }

        bool AddTicketToCart(int cartId, int ticketId, int quantity);
        bool RemoveTicketFromCart(int cartId, int ticketId);
        Cart GetCart(int cartId);
        bool ProcessPayment(int cartId, string cardNumber, string exp, string cardHolderName, string cvc);
    }
}
