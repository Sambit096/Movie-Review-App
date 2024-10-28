namespace MovieReviewApp.Models
{
    public class PaymentGateway
    {
        public int GatewayId { get; set; }   // Primary key for PaymentGateway
        public int CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CardHolderName { get; set; }
        public int CVC { get; set; }
        public PaymentGateway(){}
    }
}
