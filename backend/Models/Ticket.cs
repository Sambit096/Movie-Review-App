namespace MovieReviewApp.Models{
    public class Ticket{
        // Unique identifier for each ticket
        public int TicketId { get; set; }

        // Foreign key linking this ticket to a specific showtime
        public required int ShowTimeId { get; set; }

        // Price per ticket for the specified showtime
        public required double Price { get; set; }

        // Number of tickets purchased in this transaction
        public int Quantity { get; set; }

        // If ticket is purchased or not
        public required bool Availability {get; set; }

        public int? CartId { get; set; }


        public ShowTime ShowTime { get; set; }
        public Cart Cart { get; set; }
        public Ticket(){}
        
    }
}