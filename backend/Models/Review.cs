using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieReviewApp.Models{
    public class Review{

        public int ReviewId{get; set;} // Primary Key for each review

        public int MovieId { get; set; } // Foreign Key referencing the Movie

         public int UserId { get; set; } // Foreign Key referencing the User
        
        [StringLength(1000)]
        public string? Content { get; set; }  // Content of the review, optional

        [StringLength(100)]
        public string? ReviewerName { get; set; }  // Name of the reviewer(Optional) 

        public DateTime? CreatedAt { get; set; } = DateTime.Now;  // Date the review was created

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }  // Rating (1-5), ensures a positive rating

        public int Likes {get; set; } = 0; // Likes for the review
        public Movie? Movie { get; set; }   // Navigation property to the Movie for which the review was posted

       
        public User? User { get; set; }   // Navigation property to the User who posted the review

        public Review() {}
    }
}