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
        public int MovieId { get; set; }

        // Date and time for when the showtime occurs
        public DateTime ViewingTime { get; set; }

        // Number of tickets available for this showtime
        public int NumOfTickets { get; set; }

        // Current status of the showtime, indicating if it is available or sold out
        public MovieStatus Status { get; set; }
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