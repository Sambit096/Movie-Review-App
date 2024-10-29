using Microsoft.AspNetCore.Authentication;

namespace MovieReviewApp.models {
    public class Cart {
        public int CartId {get; set;}
        public List<Ticket> Tickets {get; set;} = new List<Ticket>();
        public double Total {get; set;}
        public Cart(){}
        }

    }
    
