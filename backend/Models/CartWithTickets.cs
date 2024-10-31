namespace MovieReviewApp.Models
{
    public class CartWithTickets
    {
        public Cart Cart { get; set; }
        public List<Ticket> Tickets { get; set; }
        public CartWithTickets(){}
    }
}
