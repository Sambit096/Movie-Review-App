namespace MovieReviewApp.Models {

    public class PaymentGateway {
        //All fields are required or it would be an invalid card/purchase
        public required int GatewayId {get; set;}
        public required int CardNumber {get; set;}
        public required DateTime ExpirationDate {get; set; }
        public required string CardHolderName {get; set; }
        public required int CVC {get; set; }
    }

}


