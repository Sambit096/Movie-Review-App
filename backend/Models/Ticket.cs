namespace MovieReviewApp.Models{
    public class Ticket{
        // Unique identifier for each ticket
        public int TicketId { get; set; }

        // Foreign key linking this ticket to a specific showtime
        public int ShowTimeId { get; set; }

        // Price per ticket for the specified showtime
        public double Price { get; set; }

        // Number of tickets purchased or available in this transaction
        public int Quantity { get; set; }

        public Ticket(){}
        /*public Ticket(int ticketId, int showTimeId, double price, int quantity = 1)
        {
            this.TicketId = ticketId;
            this.ShowTimeId = showTimeId;
            this.Price = price;
            this.Quantity = quantity; 
        }*/
    }
}