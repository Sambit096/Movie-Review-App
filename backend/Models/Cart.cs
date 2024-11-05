using System.Collections.Generic;

namespace MovieReviewApp.Models
{
    public class Cart
    {
        public int CartId { get; set; }                   // Unique identifier for each cart
        public double Total { get; set; }                 // Total price of items in the cart
        public int? UserId { get; set; }


        public User? User { get; set; }
        public Cart(){}
    }
}
