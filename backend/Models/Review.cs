using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieReviewApp.Models{
    public class Review{
        
        [Key]
        public int ReviewId{get; set;} // Primary Key for each review

        [Required]
        [ForeignKey("Movie")] 
        public int MovieId { get; set; } // Foreign Key referencing the Movie

        [Required]
        [StringLength(1000)]
        public string? Content { get; set; }  // Content of the review, optional

        
        [StringLength(100)]
        public string? ReviewerName { get; set; }  // Name of the reviewer(Optional) 

        public DateTime? CreatedAt { get; set; } = DateTime.Now;  // Date the review was created

        public Movie Movie { get; set; }   // Navigation property for the related Movie
    }
}