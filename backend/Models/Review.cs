using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieReviewApp.Models{
    public class Review{

        public int ReviewId{get; set;} // Primary Key for each review

        public int MovieId { get; set; } // Foreign Key referencing the Movie
        
        [StringLength(1000)]
        public string? Content { get; set; }  // Content of the review, optional

        
        [StringLength(100)]
        public string? ReviewerName { get; set; }  // Name of the reviewer(Optional) 

        public DateTime? CreatedAt { get; set; } = DateTime.Now;  // Date the review was created

        public Movie Movie { get; set; } = null!;  // Ensures Movie is required for Review

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }  // Rating (1-5), ensures a positive rating

        public Review() {}
    }
}