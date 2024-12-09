import fetchData from "../utils/request-utils";
import { useState, useEffect } from "react";
import { useParams, useNavigate, useLocation } from "react-router-dom";

const Reviews = () => {
  const { movieId } = useParams();
  const nav = useNavigate();
  const location = useLocation();
  const { title } = location.state || {};
  const [reviews, setReviews] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchReviews = async () => {
      try {
        const response = await fetchData(
          `http://localhost:5190/api/Review/GetReviews?MovieId=${movieId}&Title=${title}&Genre=&Description=&Rating=0`
        );
        setReviews(response);
        setLoading(false);
      } catch (err) {
        setError("Failed to fetch reviews");
        setLoading(false);
      }
    };

    fetchReviews();
  }, [movieId]);

  if (loading) return <p>Loading reviews...</p>;
  if (error) return <p>{error}</p>;
  
  const handleLike = async (reviewId) => {
    try {
      const response = await fetch(
        `http://localhost:5190/api/Review/AddLike?reviewId=${reviewId}`,
        {
          method: 'PUT',
        }
      );
      if (response.ok) {
        console.log('Like added successfully:', response);
        const response2 = await fetchData(
          `http://localhost:5190/api/Review/GetReviews?MovieId=${movieId}&Title=${title}&Genre=&Description=&Rating=0`
        );
        setReviews(response2);
      } else {
        console.error('Failed to add like:', response);
      }
    } catch (error) {
      console.error('Error while adding like:', error);
    }
  };
  
  const handleDislike = async (reviewId) => {
    try {
      const response = await fetch(
        `http://localhost:5190/api/Review/RemoveLike?reviewId=${reviewId}`,
        {
          method: 'PUT',
        }
      );
      if (response.ok) {
        console.log('Like added successfully:', response);
        const response2 = await fetchData(
          `http://localhost:5190/api/Review/GetReviews?MovieId=${movieId}&Title=${title}&Genre=&Description=&Rating=0`
        );
        setReviews(response2);
      } else {
        console.error('Failed to add like:', response);
      }
    } catch (error) {
      console.error('Error while adding like:', error);
    }
  };

  return (
    <div>
      <button onClick={() => nav("/Movies")}>Back to Movies</button>

      <h2>Reviews for {title}</h2>
      {reviews.length === 0 ? (
        <p>No reviews available.</p>
      ) : (
        reviews.map((review) => (
          <div
            key={review.reviewId}
            style={{
              border: "1px solid #ccc",
              margin: "10px",
              padding: "10px",
            }}
          >
            <h4 style={{ textDecoration: "bold" }}>
              Reviewer: {review.reviewerName || "Anonymous"}
            </h4>
            <p>
              <strong>Rating:</strong> {review.rating} / 5
            </p>
            <p>{review.content}</p>
            <div>
              <button
                onClick={() => handleLike(review.reviewId)}
                style={{ marginRight: "10px", cursor: "pointer" }}
              >
                👍 {review.likes}
              </button>
              <button
                onClick={() => handleDislike(review.reviewId)}
                style={{ cursor: "pointer" }}
              >
                👎
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
  );
};

export default Reviews;
