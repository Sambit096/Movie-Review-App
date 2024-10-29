namespace MovieReviewApp.Models
{
    public class PaymentGateway
    {
        public int GatewayId { get; set; }   // Primary key for PaymentGateway
        public required int CardNumber { get; set; }
        public required DateTime ExpirationDate { get; set; }
        public required string CardHolderName { get; set; }
        public required int CVC { get; set; }
        public PaymentGateway(){}
    }
}
