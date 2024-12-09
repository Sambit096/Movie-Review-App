using System;
namespace MovieReviewApp.Models
{
    // Enum to represent the status of a movie showtime
    public enum MovieStatus{
            Available,
            SoldOut
    }
    public class ShowTime{

        // Unique identifier for each showtime
        public int ShowTimeId { get; set; }

        // Foreign key linking this showtime to a specific movie
        public required int MovieId { get; set; }

        // Date and time for when the showtime occurs
        public required DateTime ViewingTime { get; set; }

        public Movie? Movie {get; set; }
        public ShowTime(){}
        /*public ShowTime(int showTimeId,int movieId, DateTime viewingTime,int numOfTickets, MovieStatus status)
        {
            this.ShowTimeId=showTimeId;
            this.MovieId=movieId;
            this.ViewingTime=viewingTime;
            this.NumOfTickets=numOfTickets;
            this.Status=status;
        }*/
    }
}