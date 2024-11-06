namespace MovieReviewApp.Models
{
    public class PaymentGateway
    {
        public int GatewayId { get; set; }   // Primary key for PaymentGateway
        public required string CardNumber { get; set; }
        public required string ExpirationDate { get; set; }
        public required string CardHolderName { get; set; }
        public required string CVC { get; set; }
        public PaymentGateway() {}
    }
}
