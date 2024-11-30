using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using MovieReviewApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MovieReviewApp.Tools;
using Microsoft.IdentityModel.Tokens;

namespace MovieReviewApp.Services
{
    public class ReviewService : IReviewService
    {
        private readonly MovieReviewDbContext dbContext;

        public ReviewService(MovieReviewDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IList<Review>> GetReviews(int movieId)
        {
            try
            {
                var reviews = await this.dbContext.Reviews.Where(r => r.MovieId == movieId).ToListAsync();
                if (reviews.IsNullOrEmpty()) {
                    return new List<Review>();
                }
                return reviews;
            } catch (Exception e) 
            {
                throw new Exception("Error when getting reviews:", e);
            }
        }

        public async Task<IList<Review>> GetReviewsByUser(int userId)
        {
            try
            {
                var reviews = await this.dbContext.Reviews.Where(r => r.UserId == userId).ToListAsync();
                if (reviews.IsNullOrEmpty()) {
                    return new List<Review>();
                }
                return reviews;
            } catch (Exception e) 
            {
                throw new Exception("Error when getting reviews:", e);
            }
        }

        public async Task<bool> AddReview(Movie movie, Review review)
        {
            try
            {
                review.MovieId = movie.MovieId;
                await this.dbContext.Reviews.AddAsync(review);
                await this.dbContext.SaveChangesAsync();
                return true;
            } catch (Exception e)
            {
                throw new Exception("Error adding review:", e);
            }
        }

        public async Task<bool> EditReview(int id, Review newReview)
        {
            try 
            {
                var existingReview = await this.dbContext.Reviews.FirstOrDefaultAsync(r => r.ReviewId == id);
                if(existingReview == null)
                {
                    throw new Exception("Invalid id");
                }
                existingReview.Content = newReview.Content;
                existingReview.MovieId = newReview.MovieId;
                existingReview.ReviewerName = newReview.ReviewerName;
                existingReview.CreatedAt = newReview.CreatedAt;
                existingReview.Rating = newReview.Rating;
                existingReview.UserId = newReview.UserId;
                existingReview.Likes = newReview.Likes;

                await this.dbContext.SaveChangesAsync();
                return true;
            } catch (Exception e)
            {
                throw new Exception("Error updating review:", e);
            }
        }

        public async Task<bool> RemoveReview(Review review)
        {
            try
            {
                var existingReview = await this.dbContext.Reviews.FindAsync(review.ReviewId);
                if(existingReview == null)
                {
                    throw new Exception("Review does not exist");
                }
                this.dbContext.Reviews.Remove(existingReview);
                await this.dbContext.SaveChangesAsync();
                return true; 
            } catch (Exception e) {
                throw new Exception("Error deleting review", e);
            }
        }

        public async Task<bool> AddLike(int reviewId) {
            try {
                var result = await dbContext.Reviews
                    .Where(r => r.ReviewId == reviewId)
                    .ExecuteUpdateAsync(s =>
                    s.SetProperty(r => r.Likes, r => r.Likes + 1));
                return true;
            } catch (Exception e) {
                throw new Exception("Error adding Like", e);
            }
        }

        public async Task<bool> RemoveLike(int reviewId) {
            try {
                var result = await dbContext.Reviews
                    .Where(r => r.ReviewId == reviewId)
                    .ExecuteUpdateAsync(s =>
                    s.SetProperty(r => r.Likes, r => r.Likes > 0 ? r.Likes - 1 : 0));
                return true;
            } catch (Exception e) {
                throw new Exception("Error adding Like", e);
            }
        }
    }
}