namespace MovieReviewApp.Models {
    public class User {
        public int UserId { get; set; }
        public required string Email {  get; set; }
        public required string Username {  get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public UserGender Gender {get; set; } = UserGender.None; 
        public AgeGroup AgeGroup {get; set; } = AgeGroup.YoungAdult;
        public required string Password { get; set; }
        public UserPreference NotiPreference { get; set; } = UserPreference.None;
        public UserType UserType {get; set; } = UserType.User;
    }

    public enum UserGender {
        None,
        F,
        M,
        Other
    }
    public enum UserPreference{
        SMS,
        Email,
        Both,
        None
    }

    public enum UserType {
        User,
        Admin
    }

    public enum AgeGroup {
        Teen,
        YoungAdult,
        Adult,
        Retired

    }
}
