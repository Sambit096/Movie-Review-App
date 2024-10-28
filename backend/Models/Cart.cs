using System.Collections.Generic;

namespace MovieReviewApp.Models
{
    public class Cart
    {
        public int CartId { get; set; }                   // Unique identifier for each cart
        public List<Ticket> Tickets { get; set; }= new List<Ticket>();       // Collection of tickets in the cart
        public double Total { get; set; }                 // Total price of items in the cart
        public Cart(){}
    }
}
