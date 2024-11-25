import React, { useState } from "react";
import fetchData from "../utils/request-utils";

const Management = () => {
  const [movie, setMovie] = useState({
    title: "",
    genre: "",
    description: "",
    rating: 0,
  });
  const [editMovieData, setEditMovieData] = useState({
    movieId: "",
    title: "",
    genre: "",
    description: "",
    rating: 0,
  });
  const [showTime, setShowTime] = useState({
    movieId: "",
    viewingTime: "",
    status: 1,
  });

  // Add a new movie
  const addMovie = async () => {
    try {
      await fetchData("http://localhost:5190/api/Management", {
        method: "POST",
        body: JSON.stringify(movie),
      });
      alert("Movie added successfully!");
      setMovie({ title: "", genre: "", description: "", rating: 0 });
    } catch (err) {
      console.error("Error adding movie:", err);
      alert("Failed to add movie. Please try again.");
    }
  };

  // Edit an existing movie
  const editMovie = async () => {
    try {
      await fetchData(
        `http://localhost:5190/api/Management/EditMovie/${editMovieData.movieId}`,
        {
          method: "PUT",
          body: JSON.stringify(editMovieData),
        }
      );
      alert("Movie updated successfully!");
      setEditMovieData({
        movieId: "",
        title: "",
        genre: "",
        description: "",
        rating: 0,
      });
    } catch (err) {
      console.error("Error editing movie:", err);
      alert("Failed to edit movie. Please try again.");
    }
  };

  // Add a showtime
  const addShowTime = async () => {
    try {
      await fetchData("http://localhost:5190/api/Management/AddShowTime", {
        method: "POST",
        body: JSON.stringify(showTime),
      });
      alert("Showtime added successfully!");
      setShowTime({ movieId: "", viewingTime: "", status: 1 });
    } catch (err) {
      console.error("Error adding showtime:", err);
      alert("Failed to add showtime. Please try again.");
    }
  };

  return (
    <div className="management-container">
      <h1>Management</h1>

      {/* Add Movie */}
      <section>
        <h2>Add Movie</h2>
        <input
          type="text"
          placeholder="Title"
          value={movie.title}
          onChange={(e) => setMovie({ ...movie, title: e.target.value })}
        />
        <input
          type="text"
          placeholder="Genre"
          value={movie.genre}
          onChange={(e) => setMovie({ ...movie, genre: e.target.value })}
        />
        <textarea
          placeholder="Description"
          value={movie.description}
          onChange={(e) =>
            setMovie({ ...movie, description: e.target.value })
          }
        />
        <input
          type="number"
          placeholder="Rating"
          value={movie.rating}
          onChange={(e) =>
            setMovie({ ...movie, rating: parseInt(e.target.value) })
          }
        />
        <button onClick={addMovie}>Add Movie</button>
      </section>

      {/* Edit Movie */}
      <section>
        <h2>Edit Movie</h2>
        <input
          type="text"
          placeholder="Movie ID"
          value={editMovieData.movieId}
          onChange={(e) =>
            setEditMovieData({ ...editMovieData, movieId: e.target.value })
          }
        />
        <input
          type="text"
          placeholder="Title"
          value={editMovieData.title}
          onChange={(e) =>
            setEditMovieData({ ...editMovieData, title: e.target.value })
          }
        />
        <input
          type="text"
          placeholder="Genre"
          value={editMovieData.genre}
          onChange={(e) =>
            setEditMovieData({ ...editMovieData, genre: e.target.value })
          }
        />
        <textarea
          placeholder="Description"
          value={editMovieData.description}
          onChange={(e) =>
            setEditMovieData({ ...editMovieData, description: e.target.value })
          }
        />
        <input
          type="number"
          placeholder="Rating"
          value={editMovieData.rating}
          onChange={(e) =>
            setEditMovieData({
              ...editMovieData,
              rating: parseInt(e.target.value),
            })
          }
        />
        <button onClick={editMovie}>Edit Movie</button>
      </section>

      {/* Add Showtime */}
      <section>
        <h2>Add Showtime</h2>
        <input
          type="text"
          placeholder="Movie ID"
          value={showTime.movieId}
          onChange={(e) =>
            setShowTime({ ...showTime, movieId: e.target.value })
          }
        />
        <input
          type="datetime-local"
          value={showTime.viewingTime}
          onChange={(e) =>
            setShowTime({ ...showTime, viewingTime: e.target.value })
          }
        />
        <input
          type="number"
          placeholder="Status"
          value={showTime.status}
          onChange={(e) =>
            setShowTime({ ...showTime, status: parseInt(e.target.value) })
          }
        />
        <button onClick={addShowTime}>Add Showtime</button>
      </section>
    </div>
  );
};

export default Management;
