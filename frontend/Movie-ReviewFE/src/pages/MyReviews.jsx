import fetchData from "../utils/request-utils";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

const MyReviews = () => {
  const nav = useNavigate();
  const [reviews, setReviews] = useState([]);
  const [movies, setMovies] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [userData, setUserData] = useState(null);

  const [isReviewOpen, setIsReviewOpen] = useState(false);
  const [formData, setFormData] = useState({
    isAnonymous: false,
    selectedMovie: "",
    rating: "",
    reviewText: "",
  });

  const [editReviewData, setEditReviewData] = useState(null);

  useEffect(() => {
    const storedValue = localStorage.getItem("user");
    if (storedValue) {
      const parse = JSON.parse(storedValue);
      setUserData(parse);

      const fetchMovies = async () => {
        try {
          const response = await fetch(
            "http://localhost:5190/api/Movie/GetMovies"
          );
          if (!response.ok) {
            throw new Error("Network response was not ok");
          }
          const data = await response.json();
          setMovies(data);
        } catch (error) {
          setError("Failed to fetch movies");
        } finally {
          setLoading(false);
        }
      };

      fetchMovies();
      fetchReviews(parse.userId);
    }
  }, []);

  const fetchReviews = async (id) => {
    try {
      const response = await fetchData(
        `http://localhost:5190/api/Review/GetReviewsByUser?userId=${id}`
      );
      setReviews(response);
      setLoading(false);
    } catch (err) {
      setError("Failed to fetch reviews");
      setLoading(false);
    }
  };

  if (loading) return <p>Loading reviews...</p>;
  if (error) return <p>{error}</p>;

  const reviewedMovieIds = reviews.map((review) => review.movieId);
  const availableMovies = movies.filter(
    (movie) => !reviewedMovieIds.includes(movie.movieId)
  );

  const handleInputChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormData({
      ...formData,
      [name]: type === "checkbox" ? checked : value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    let name = userData.username;
    if (formData.isAnonymous) {
      name = "Anonymous";
    }

    const reviewPayload = {
      userId: userData.userId,
      content: formData.reviewText,
      reviewerName: name,
      rating: formData.rating,
      movieId: formData.selectedMovie,
    };

    if (editReviewData) {
      try {
        const res = await fetch(
          `http://localhost:5190/api/Review/EditReview?reviewId=${editReviewData.reviewId}`,
          {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(reviewPayload),
          }
        );
        if (!res.ok) throw new Error("Review update failed");
        fetchReviews(userData.userId);
      } catch (e) {
        console.log(e);
      }
    } else {
      // Create new review
      const queryParams = new URLSearchParams({
        movieId: formData.selectedMovie,
        title: movies.find((movie) => movie.movieId === formData.selectedMovie)
          ?.title,
      });

      try {
        const res = await fetch(
          `http://localhost:5190/api/Review/AddReview?${queryParams.toString()}`,
          {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(reviewPayload),
          }
        );
        if (!res.ok) throw new Error("Review submission failed");
        fetchReviews(userData.userId);
      } catch (e) {
        console.log(e);
      }
    }

    setIsReviewOpen(false);
    setEditReviewData(null);
  };

  const handleDelete = async (review) => {
    console.log(review);
    const reviewPayload = {
      userId: userData.userId,
      content: review.content,
      reviewerName: review.reviewerName,
      rating: review.rating,
      movieId: review.movieId,
      reviewId: review.reviewId,
    };

    if (review) {
      try {
        const res = await fetch(
          `http://localhost:5190/api/Review/RemoveReview`,
          {
            method: "DELETE",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(reviewPayload),
          }
        );
        if (!res.ok) throw new Error("Review delete failed");
        fetchReviews(userData.userId);
      } catch (e) {
        console.log(e);
      }
    }
    setEditReviewData(null);
  };

  return (
    <div>
      <button
        className="review-buttons"
        onClick={() => nav("/Movies")}
      >
        Back to Movies
      </button>
      <br></br>
      <button
        className="review-buttons"
        onClick={() => {
          setFormData({
            isAnonymous: false,
            selectedMovie: "",
            rating: "",
            reviewText: "",
          });
          setIsReviewOpen(true);
        }}
      >
        Create New Review
      </button>

      {isReviewOpen && (
        <div className="modal">
          <div className="modal-content">
            <span
              className="close"
              onClick={() => {
                setIsReviewOpen(false);
                setEditReviewData(null);
              }}
            >
              &times;
            </span>
            {loading ? (
              <p>Loading movies...</p>
            ) : error ? (
              <p>{error}</p>
            ) : (
              <form onSubmit={handleSubmit}>
                <label>
                  <input
                    type="checkbox"
                    name="isAnonymous"
                    checked={formData.isAnonymous}
                    onChange={handleInputChange}
                  />
                  Post as Anonymous
                </label>
                <br />
                {!editReviewData && (
                  <>
                    <label>
                      Select Movie:
                      <select
                        name="selectedMovie"
                        value={formData.selectedMovie}
                        onChange={handleInputChange}
                        required
                      >
                        <option value="">Select a movie</option>
                        {availableMovies.map((movie) => (
                          <option key={movie.movieId} value={movie.movieId}>
                            {movie.title}
                          </option>
                        ))}
                      </select>
                    </label>
                    <br />
                  </>
                )}
                <label>
                  Rating:
                  <select
                    name="rating"
                    value={formData.rating}
                    onChange={handleInputChange}
                    required
                  >
                    <option value="">Select Rating</option>
                    <option value="1">1</option>
                    <option value="2">2</option>
                    <option value="3">3</option>
                    <option value="4">4</option>
                    <option value="5">5</option>
                  </select>
                </label>
                <br />
                <label>
                  Review:
                  <textarea
                    name="reviewText"
                    value={formData.reviewText}
                    onChange={handleInputChange}
                    required
                  />
                </label>
                <br />
                <button type="submit">
                  {editReviewData ? "Update Review" : "Submit Review"}
                </button>
              </form>
            )}
          </div>
        </div>
      )}

      <h2>Reviews for {userData.username}</h2>
      <div className="reviews-container">
        {" "}
        {reviews.length === 0 ? (
          <p>No reviews available.</p>
        ) : (
          reviews.map((review) => (
            <div key={review.reviewId} className="review-card">
              <h4>
                {
                  movies.find((movie) => movie.movieId === review.movieId)
                    ?.title
                }
              </h4>
              <h4>Reviewer: {review.reviewerName || "Anonymous"}</h4>
              <p>
                <strong>Rating:</strong> {review.rating} / 5
              </p>
              <p>{review.content}</p>
              <div>
                <button style={{ marginRight: "10px" }}>
                  👍 {review.likes}
                </button>
                <button>👎</button>
              </div>
              <div style={{ marginTop: "10px" }}>
                <button
                  style={{ marginRight: "10px" }}
                  onClick={() => {
                    setEditReviewData(review);
                    setFormData({
                      isAnonymous: review.reviewerName === "Anonymous",
                      selectedMovie: review.movieId,
                      rating: review.rating,
                      reviewText: review.content,
                    });
                    setIsReviewOpen(true);
                  }}
                >
                  Edit
                </button>
                <button
                  onClick={() => {
                    handleDelete(review);
                  }}
                >
                  Delete
                </button>
              </div>
              <p>
                <em>
                  Reviewed on: {new Date(review.createdAt).toLocaleDateString()}
                </em>
              </p>
            </div>
          ))
        )}
      </div>
    </div>
  );
};

export default MyReviews;
