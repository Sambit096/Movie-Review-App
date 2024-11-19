using MovieReviewApp.Models;
namespace MovieReviewApp.Interfaces {

    public interface IReviewService {
        
        public Task<bool> AddReview(Movie movie, Review review);

        public Task<bool> RemoveReview(Review review);

        public Task<bool> EditReview(int reviewId, Review newReview);

        public Task<IList<Review>> GetReviews(int movieId);
    }
}