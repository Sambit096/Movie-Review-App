// Movies.jsx
import { useState, useEffect } from "react";
import MovieItem from "../components/MovieItem";
import fetchData from "../utils/request-utils";
import { useUser } from "../UserContext";
import { useNavigate } from "react-router-dom";

const Movies = () => {
  const nav = useNavigate();
  const [movies, setMovies] = useState([]);
  const [filteredMovies, setFilteredMovies] = useState([]);
  const [selectedGenre, setSelectedGenre] = useState("");
  
  // New state for Selected Rating
  const [selectedRating, setSelectedRating] = useState("");
  
  const [userData, setUserData] = useState(null);

  // Unified Filtering Function
  const filterMovies = (genre, rating) => {
    let filtered = movies;

    if (genre !== "") {
      filtered = filtered.filter((movie) => movie.genre === genre);
    }

    if (rating !== "") {
      filtered = filtered.filter((movie) => movie.rating === rating);
    }

    setFilteredMovies(filtered);
  };

  // Handler for Genre Change
  const handleGenreChange = (event) => {
    const genre = event.target.value;
    setSelectedGenre(genre);

    // Apply both Genre and Rating filters
    filterMovies(genre, selectedRating);
  };

  // Handler for Rating Change
  const handleRatingChange = (event) => {
    const rating = event.target.value;
    setSelectedRating(rating);

    // Apply both Genre and Rating filters
    filterMovies(selectedGenre, rating);
  };

  useEffect(() => {
    const fetchMovies = async () => {
      try {
        const data = await fetchData(
          "http://localhost:5190/api/Movie/GetMovies"
        );
        setMovies(data);
        setFilteredMovies(data);
      } catch (err) {
        console.log(err);
      }
    };

    fetchMovies();
    const storedValue = localStorage.getItem("user");
    if (storedValue) {
      const parse = JSON.parse(storedValue);
      setUserData(parse);
    }
  }, []);

  return (
    <div>
      <div className="heading--filter">
        {userData && <h1>Welcome, {userData.username}!</h1>}
        <h2>Currently Playing:</h2>

        <div>
          {/* Genre Filter */}
          <label htmlFor="genre-filter">Filter by Genre: </label>
          <select
            id="genre-filter"
            value={selectedGenre}
            onChange={handleGenreChange}
          >
            <option value="">All</option>
            {[...new Set(movies.map((movie) => movie.genre))].map((genre) => (
              <option key={genre} value={genre}>
                {genre}
              </option>
            ))}
          </select>

          {/* Rating Filter */}
          <label htmlFor="rating-filter" style={{ marginLeft: "20px" }}>
            Filter by Rating:{" "}
          </label>
          <select
            id="rating-filter"
            value={selectedRating}
            onChange={handleRatingChange}
          >
            <option value="">All</option>
            <option value="G">G</option>
            <option value="PG">PG</option>
            <option value="PG13">PG13</option>
            <option value="R">R</option>
            <option value="NC17">NC17</option>
          </select>

          <div className="my-reviews" style={{ display: "inline-block", marginLeft: "20px" }}>
            {userData && (
              <button onClick={() => nav("/MyReviews")}>
                View My Reviews
              </button>
            )}
          </div>
        </div>
      </div>
      <div className="movie--list">
        {filteredMovies.length > 0 ? (
          filteredMovies.map((movie) => (
            <MovieItem
              key={movie.movieId}
              id={movie.movieId}
              title={movie.title}
              desc={movie.description}
              genre={movie.genre}
              rating={movie.rating} // Pass rating as string
            />
          ))
        ) : (
          <p>No movies found matching the selected criteria.</p>
        )}
      </div>
    </div>
  );
};

export default Movies;
