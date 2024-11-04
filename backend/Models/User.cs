namespace MovieReviewApp.Models {
    public class User {
        public int UserId { get; set; }
        public string email {  get; set; }
        public string username {  get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        //public NotificationPreference NotificationPref { get; set; }
    }

    public enum NotificationPreference {
        Email,
        SMS
    }
}
