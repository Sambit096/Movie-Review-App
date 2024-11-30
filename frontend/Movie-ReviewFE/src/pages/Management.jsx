import React, { useState, useEffect } from "react";
import "../Management.css"; // Import the CSS file

const Management = () => {
  const [movie, setMovie] = useState({
    title: "",
    genre: "",
    description: "",
    rating: "",
  });
  const [movieList, setMovieList] = useState([]);
  const [editMovieData, setEditMovieData] = useState({
    movieId: "",
    title: "",
    genre: "",
    description: "",
    rating: "",
  });
  const [showTime, setShowTime] = useState({
    movieId: "",
    viewingTime: "",
    status: "",
  });

  const [removeMovieId, setRemoveMovieId] = useState("");

  const [ticketData, setTicketData] = useState({
    movieId: "",
    numberOfTickets: "",
  });

  const [removeTicketData, setRemoveTicketData] = useState({
    movieId: "",
    numberOfTickets: "",
  });

  const [editTicketData, setEditTicketData] = useState({
    movieId: "",
    showTimeId: "",
    price: "",
    availability: false,
  });
  const [showTimesForEdit, setShowTimesForEdit] = useState([]);
  const [availableTickets, setAvailableTickets] = useState(0);

  // Defining fetchData locally with improved error handling
  const fetchData = async (url, options) => {
    const defaultHeaders = {
      "Content-Type": "application/json",
    };

    const finalOptions = {
      ...options,
      headers: {
        ...defaultHeaders,
        ...(options?.headers || {}),
      },
    };

    try {
      const response = await fetch(url, finalOptions);

      if (!response.ok) {
        const errorText = await response.text();
        console.error(`HTTP Error: ${response.status} - ${errorText}`);
        throw new Error(errorText || `HTTP error! status: ${response.status}`);
      }

      // Attempt to parse the response as JSON
      const contentType = response.headers.get("content-type");
      if (contentType && contentType.includes("application/json")) {
        return await response.json();
      }

      return null;
    } catch (error) {
      console.error("Fetch error:", error);
      throw error;
    }
  };

  // Fetch all movies on component mount
  const fetchMovies = async () => {
    try {
      const response = await fetchData(
        "http://localhost:5190/api/Management/GetAllMovies",
        {
          method: "GET",
        }
      );
      if (response) {
        setMovieList(response); // Set the retrieved movies in the state
      } else {
        console.error("Received null response when fetching movies.");
      }
    } catch (err) {
      console.error("Error fetching movies:", err);
    }
  };

  useEffect(() => {
    fetchMovies();
  }, []);

  // Helper function to check if a movie has showtimes
  const hasShowtimes = async (movieId) => {
    try {
      const showTimes = await fetchData(
        `http://localhost:5190/api/Management/GetShowTimesByMovie/${movieId}`,
        {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
          },
        }
      );

      // If showTimes is null or empty, return false
      if (!showTimes || showTimes.length === 0) {
        return false;
      }

      return true;
    } catch (error) {
      console.error("Error fetching showtimes:", error);
      // Optionally, handle specific error scenarios here
      return false;
    }
  };

  const fetchShowTimesForEdit = async (movieId) => {
    if (!movieId) {
      setShowTimesForEdit([]);
      return;
    }

    try {
      const response = await fetchData(
        `http://localhost:5190/api/Management/GetShowTimesByMovie/${movieId}`,
        {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
          },
        }
      );

      if (response) {
        setShowTimesForEdit(response);
      } else {
        console.error("Received null response when fetching showtimes.");
        setShowTimesForEdit([]);
      }
    } catch (err) {
      console.error("Error fetching showtimes for edit:", err);
      setShowTimesForEdit([]);
    }
  };

  // Helper function to fetch available tickets for a movie
  const fetchAvailableTickets = async (movieId) => {
    try {
      const response = await fetchData(
        `http://localhost:5190/api/Management/GetAvailableTicketsByMovie/${movieId}`,
        {
          method: "GET",
        }
      );

      if (response && response.AvailableTickets !== undefined) {
        setAvailableTickets(response.AvailableTickets);
        return response.AvailableTickets;
      } else {
        console.error("Unexpected response structure:", response);
        setAvailableTickets(0);
        return 0;
      }
    } catch (error) {
      console.error("Error fetching available tickets:", error);
      if (error.message.includes("Not Found")) {
        alert("Movie not found. Please select a valid movie.");
      }
      setAvailableTickets(0);
      return 0;
    }
  };

  // Add movie
  const addMovie = async (e) => {
    e.preventDefault(); // Prevents the default form submission behavior
    console.log("addMovie called");

    // Validate inputs
    if (!movie.title.trim()) {
      alert("Please add a title.");
      console.error("Validation failed: Missing title");
      return;
    }
    if (!movie.genre.trim()) {
      alert("Please add a genre.");
      console.error("Validation failed: Missing genre");
      return;
    }
    if (!movie.description.trim()) {
      alert("Please add a description.");
      console.error("Validation failed: Missing description");
      return;
    }
    if (!movie.rating.trim()) {
      alert("Please select a rating.");
      console.error("Validation failed: Missing rating");
      return;
    }

    console.log("All validations passed. Proceeding to API call.");

    try {
      const movieData = {
        title: movie.title,
        genre: movie.genre,
        description: movie.description,
        rating: Number(movie.rating),
      };

      console.log("Making API call with data:", movieData);

      const response = await fetchData("http://localhost:5190/api/Management/AddMovie", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(movieData),
      });

      console.log("API call successful:", response);
      alert("Movie added successfully!");
      // Reset the form only after successful submission
      setMovie({ title: "", genre: "", description: "", rating: "" });
      // Refresh the movie list to include the new movie
      fetchMovies();
    } catch (err) {
      console.error("Error adding movie:", err.message);
      alert("Failed to add movie. Please try again.");
    }
  };

  // Edit an existing movie
  const editMovie = async () => {
    // Validate inputs
    if (!editMovieData.movieId) {
      alert("Please select a movie.");
      console.error("Validation failed: Missing Movie ID");
      return;
    }
    if (!editMovieData.title.trim()) {
      alert("Please add a title.");
      console.error("Validation failed: Missing title");
      return;
    }
    if (!editMovieData.genre.trim()) {
      alert("Please add a genre.");
      console.error("Validation failed: Missing genre");
      return;
    }
    if (!editMovieData.description.trim()) {
      alert("Please add a description.");
      console.error("Validation failed: Missing description");
      return;
    }
    if (editMovieData.rating === "") {
      alert("Please select a rating.");
      console.error("Validation failed: Missing rating");
      return;
    }

    try {
      const movieId = Number(editMovieData.movieId);

      const movieData = {
        movieId: movieId,
        title: editMovieData.title,
        genre: editMovieData.genre,
        description: editMovieData.description,
        rating: Number(editMovieData.rating),
      };

      console.log("Request URL:", `http://localhost:5190/api/Management/EditMovie/${movieId}`);
      console.log("Request Body:", JSON.stringify(movieData));

      await fetchData(
        `http://localhost:5190/api/Management/EditMovie/${movieId}`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(movieData),
        }
      );
      alert("Movie updated successfully!");
      setEditMovieData({
        movieId: "",
        title: "",
        genre: "",
        description: "",
        rating: "",
      });
      // Refresh the movie list to reflect changes
      fetchMovies();
    } catch (err) {
      console.error("Error editing movie:", err);
      alert("Failed to edit movie. Please try again.");
    }
  };

  // Add a showtime
  const addShowTime = async () => {
    // Validate inputs
    if (!showTime.movieId) {
      alert("Please select a movie.");
      console.error("Validation failed: Missing Movie ID");
      return;
    }
    if (!showTime.viewingTime.trim()) {
      alert("Please provide a Viewing Time.");
      console.error("Validation failed: Missing Viewing Time");
      return;
    }
    if (showTime.status === "") {
      alert("Please select a Status.");
      console.error("Validation failed: Missing Status");
      return;
    }

    try {
      const movieId = Number(showTime.movieId);
      const status = Number(showTime.status);

      const showTimeData = {
        movieId: movieId,
        viewingTime: showTime.viewingTime,
        status: status,
      };

      console.log("Request Body:", JSON.stringify(showTimeData));

      await fetchData("http://localhost:5190/api/Management/AddShowTime", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(showTimeData),
      });
      alert("Showtime added successfully!");
      setShowTime({ movieId: "", viewingTime: "", status: "" });
      // Optionally, refresh showtimes if displayed elsewhere
    } catch (err) {
      console.error("Error adding showtime:", err);
      alert("Failed to add showtime. Please try again.");
    }
  };

  // Remove movie
  const removeMovie = async () => {
    // Validate input
    if (!removeMovieId) {
      alert("Please select a movie.");
      console.error("Validation failed: Missing Movie ID");
      return;
    }

    const movieId = Number(removeMovieId);
    if (isNaN(movieId)) {
      alert("Invalid Movie ID.");
      console.error("Validation failed: Movie ID is not a number");
      return;
    }

    try {
      console.log("Deleting Movie ID:", movieId);

      await fetchData(
        `http://localhost:5190/api/Management/RemoveMovie/${movieId}`,
        {
          method: "DELETE",
          headers: {
            "Content-Type": "application/json",
          },
        }
      );

      alert("Movie removed successfully!");
      setRemoveMovieId("");
      // Refresh the movie list to update the dropdowns
      fetchMovies();
    } catch (err) {
      console.error("Error removing movie:", err);
      if (err.message.includes("404")) {
        alert("Movie not found. Please check the selected movie.");
      } else {
        alert("Failed to remove movie. Please try again.");
      }
    }
  };

  // Add Tickets to Movie
  const addTicketsToMovie = async () => {
    // Validate inputs
    if (!ticketData.movieId) {
      alert("Please select a movie.");
      console.error("Validation failed: Missing Movie ID");
      return;
    }
    if (
      !ticketData.numberOfTickets ||
      isNaN(ticketData.numberOfTickets) ||
      Number(ticketData.numberOfTickets) <= 0
    ) {
      alert("Please provide a valid number of tickets.");
      console.error("Validation failed: Invalid number of tickets");
      return;
    }

    const numberOfTickets = Number(ticketData.numberOfTickets);
    const movieId = Number(ticketData.movieId);

    try {
      // Check if the selected movie has showtimes by verifying available tickets
      const tickets = await fetchAvailableTickets(movieId);

      if (tickets === 0) {
        alert("No showtimes associated with this movie. Please add a showtime first.");
        console.error("No showtimes found for Movie ID:", movieId);
        return;
      }

      const requestData = {
        movieId: movieId,
        numberOfTickets: numberOfTickets,
      };

      await fetchData(
        "http://localhost:5190/api/Management/AddTicketsToMovie",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(requestData),
        }
      );

      alert("Tickets added successfully!");
      // Reset the form
      setTicketData({ movieId: "", numberOfTickets: "" });
      setAvailableTickets(0);
      // Optionally, fetch tickets again if you have a list
    } catch (err) {
      console.error("Error adding tickets:", err);
      alert("Failed to add tickets. Please try again.");
    }
  };

  // Remove Tickets from Movie
  const removeTicketsFromMovie = async () => {
    // Validate inputs
    if (!removeTicketData.movieId) {
      alert("Please select a movie.");
      console.error("Validation failed: Missing Movie ID");
      return;
    }
    if (
      !removeTicketData.numberOfTickets ||
      isNaN(removeTicketData.numberOfTickets) ||
      Number(removeTicketData.numberOfTickets) <= 0
    ) {
      alert("Please provide a valid number of tickets to remove.");
      console.error("Validation failed: Invalid number of tickets");
      return;
    }

    const numberOfTickets = Number(removeTicketData.numberOfTickets);
    const movieId = Number(removeTicketData.movieId);

    try {
      // Fetch available tickets before attempting to remove
      const tickets = await fetchAvailableTickets(movieId);

      if (tickets === 0) {
        alert("No available tickets to remove for this movie.");
        console.error("No available tickets for Movie ID:", movieId);
        return;
      }

      if (numberOfTickets > tickets) {
        alert(`Cannot remove ${numberOfTickets} tickets. Only ${tickets} tickets are available.`);
        console.error(`Attempted to remove ${numberOfTickets} tickets, but only ${tickets} are available.`);
        return;
      }

      const requestData = {
        movieId: movieId,
        numberOfTickets: numberOfTickets,
      };

      await fetchData(
        "http://localhost:5190/api/Management/RemoveTicketsFromMovie",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(requestData),
        }
      );

      alert("Tickets removed successfully!");
      // Reset the form
      setRemoveTicketData({ movieId: "", numberOfTickets: "" });
      setAvailableTickets(0);
      // Optionally, fetch tickets again if you have a list
    } catch (err) {
      console.error("Error removing tickets:", err);
      if (err.message.includes("not enough available tickets")) {
        alert("Not enough available tickets to remove.");
      } else {
        alert("Failed to remove tickets. Please try again.");
      }
    }
  };

  // Edit Tickets
  const editTickets = async () => {
    // Validate inputs
    if (!editTicketData.movieId) {
      alert("Please select a movie.");
      console.error("Validation failed: Missing Movie ID");
      return;
    }
    if (!editTicketData.showTimeId) {
      alert("Please select a showtime.");
      console.error("Validation failed: Missing Showtime ID");
      return;
    }
    if (editTicketData.price === "") {
      alert("Please enter a valid price.");
      console.error("Validation failed: Missing price");
      return;
    }

    const price = parseFloat(editTicketData.price);
    if (isNaN(price) || price < 0) {
      alert("Please enter a valid price.");
      console.error("Validation failed: Invalid price");
      return;
    }

    try {
      const requestData = {
        movieId: Number(editTicketData.movieId),
        price: price,
        availability: editTicketData.availability,
        showTimeId: Number(editTicketData.showTimeId),
      };

      await fetchData(
        "http://localhost:5190/api/Management/EditTickets",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(requestData),
        }
      );

      alert("Tickets updated successfully!");
      // Reset the form
      setEditTicketData({
        movieId: "",
        showTimeId: "",
        price: "",
        availability: false,
      });
      setShowTimesForEdit([]);
    } catch (err) {
      console.error("Error editing tickets:", err);
      if (err.message.includes("404")) {
        alert("Movie or Showtime not found. Please check your selections.");
      } else {
        alert("Failed to edit tickets. Please try again.");
      }
    }
  };

  // Helper function to get movie title by ID
  const getMovieTitle = (movieId) => {
    const movie = movieList.find((m) => m.movieId === movieId);
    return movie ? movie.title : "Unknown Movie";
  };

  return (
    <div className="management-container">
      <h1>Management</h1>

      {/* Add Movie */}
      <section>
        <h2>Add Movie</h2>
        <form onSubmit={addMovie}>
          <label htmlFor="title">Title:</label>
          <input
            type="text"
            id="title"
            placeholder="Title"
            value={movie.title}
            onChange={(e) => setMovie({ ...movie, title: e.target.value })}
          />

          <label htmlFor="genre">Genre:</label>
          <input
            type="text"
            id="genre"
            placeholder="Genre"
            value={movie.genre}
            onChange={(e) => setMovie({ ...movie, genre: e.target.value })}
          />

          <label htmlFor="description">Description:</label>
          <textarea
            id="description"
            placeholder="Description"
            value={movie.description}
            onChange={(e) =>
              setMovie({ ...movie, description: e.target.value })
            }
          ></textarea>

          <label htmlFor="rating">Rating:</label>
          <select
            id="rating"
            value={movie.rating}
            onChange={(e) => setMovie({ ...movie, rating: e.target.value })}
          >
            <option value="">Select Rating</option>
            <option value="0">G</option>
            <option value="1">PG</option>
            <option value="2">PG13</option>
            <option value="3">R</option>
            <option value="4">NC17</option>
          </select>

          <button type="submit">Add Movie</button>
        </form>
      </section>

      {/* Edit Movie */}
      <section>
        <h2>Edit Movie</h2>
        <form>
          <label htmlFor="edit-movie">Select Movie:</label>
          <select
            id="edit-movie"
            value={editMovieData.movieId}
            onChange={(e) => {
              const selectedMovieId = e.target.value;
              setEditMovieData({ ...editMovieData, movieId: selectedMovieId });
              // Find the selected movie and populate the form fields
              const selectedMovie = movieList.find(
                (movie) => movie.movieId.toString() === selectedMovieId
              );
              if (selectedMovie) {
                setEditMovieData({
                  movieId: selectedMovie.movieId.toString(),
                  title: selectedMovie.title,
                  genre: selectedMovie.genre || "",
                  description: selectedMovie.description || "",
                  rating: selectedMovie.rating.toString(),
                });
              } else {
                setEditMovieData({
                  movieId: "",
                  title: "",
                  genre: "",
                  description: "",
                  rating: "",
                });
              }
            }}
          >
            <option value="">Select Movie</option>
            {movieList.map((movie) => (
              <option key={movie.movieId} value={movie.movieId}>
                {movie.title}
              </option>
            ))}
          </select>

          <label htmlFor="edit-title">Title:</label>
          <input
            type="text"
            id="edit-title"
            placeholder="Title"
            value={editMovieData.title}
            onChange={(e) =>
              setEditMovieData({ ...editMovieData, title: e.target.value })
            }
          />

          <label htmlFor="edit-genre">Genre:</label>
          <input
            type="text"
            id="edit-genre"
            placeholder="Genre"
            value={editMovieData.genre}
            onChange={(e) =>
              setEditMovieData({ ...editMovieData, genre: e.target.value })
            }
          />

          <label htmlFor="edit-description">Description:</label>
          <textarea
            id="edit-description"
            placeholder="Description"
            value={editMovieData.description}
            onChange={(e) =>
              setEditMovieData({ ...editMovieData, description: e.target.value })
            }
          ></textarea>

          <label htmlFor="edit-rating">Rating:</label>
          <select
            id="edit-rating"
            value={editMovieData.rating}
            onChange={(e) =>
              setEditMovieData({ ...editMovieData, rating: e.target.value })
            }
          >
            <option value="">Select Rating</option>
            <option value="0">G</option>
            <option value="1">PG</option>
            <option value="2">PG13</option>
            <option value="3">R</option>
            <option value="4">NC17</option>
          </select>

          <button type="button" onClick={editMovie}>
            Edit Movie
          </button>
        </form>
      </section>

      {/* Add Showtime */}
      <section>
        <h2>Add Showtime</h2>
        <form>
          <label htmlFor="showtime-movie">Select Movie:</label>
          <select
            id="showtime-movie"
            value={showTime.movieId}
            onChange={(e) => {
              const selectedMovieId = e.target.value;
              setShowTime({ ...showTime, movieId: selectedMovieId });
            }}
          >
            <option value="">Select Movie</option>
            {movieList && movieList.length > 0 ? (
              movieList.map((movie) => (
                <option key={movie.movieId} value={movie.movieId}>
                  {movie.title}
                </option>
              ))
            ) : (
              <option value="">No movies available</option>
            )}
          </select>

          <label htmlFor="viewing-time">Viewing Time:</label>
          <input
            type="datetime-local"
            id="viewing-time"
            value={showTime.viewingTime}
            onChange={(e) =>
              setShowTime({ ...showTime, viewingTime: e.target.value })
            }
          />

          <label htmlFor="showtime-status">Status:</label>
          <select
            id="showtime-status"
            value={showTime.status}
            onChange={(e) =>
              setShowTime({ ...showTime, status: e.target.value })
            }
          >
            <option value="">Select Status</option>
            <option value="1">Available</option>
            <option value="0">Not Available</option>
          </select>

          <button type="button" onClick={addShowTime}>
            Add Showtime
          </button>
        </form>
      </section>

      {/* Remove Movie */}
      <section>
        <h2>Remove Movie</h2>
        <form>
          <label htmlFor="remove-movie">Select Movie:</label>
          <select
            id="remove-movie"
            value={removeMovieId}
            onChange={(e) => setRemoveMovieId(e.target.value)}
          >
            <option value="">Select Movie</option>
            {movieList && movieList.length > 0 ? (
              movieList.map((movie) => (
                <option key={movie.movieId} value={movie.movieId}>
                  {movie.title}
                </option>
              ))
            ) : (
              <option value="">No movies available</option>
            )}
          </select>
          <button type="button" onClick={removeMovie}>
            Remove Movie
          </button>
        </form>
      </section>

      {/* Add Tickets to Movie */}
      <section>
        <h2>Add Tickets to Movie</h2>
        <form>
          <label htmlFor="add-ticket-movie">Select Movie:</label>
          <select
            id="add-ticket-movie"
            value={ticketData.movieId}
            onChange={async (e) => {
              const selectedMovieId = e.target.value;
              setTicketData({ ...ticketData, movieId: selectedMovieId });

              if (selectedMovieId) {
                const tickets = await fetchAvailableTickets(Number(selectedMovieId));
                setAvailableTickets(tickets);
              } else {
                setAvailableTickets(0);
              }
            }}
          >
            <option value="">Select Movie</option>
            {movieList && movieList.length > 0 ? (
              movieList.map((movie) => (
                <option key={movie.movieId} value={movie.movieId}>
                  {movie.title}
                </option>
              ))
            ) : (
              <option value="">No movies available</option>
            )}
          </select>

          {ticketData.movieId && (
            <p>Available Tickets: {availableTickets}</p>
          )}

          <label htmlFor="number-of-tickets">Number of Tickets:</label>
          <input
            type="number"
            id="number-of-tickets"
            placeholder="Number of Tickets"
            value={ticketData.numberOfTickets}
            onChange={(e) =>
              setTicketData({ ...ticketData, numberOfTickets: e.target.value })
            }
            min="1"
          />

          <button type="button" onClick={addTicketsToMovie}>
            Add Tickets
          </button>
        </form>
      </section>

      {/* Remove Tickets from Movie */}
      <section>
        <h2>Remove Tickets from Movie</h2>
        <form>
          <label htmlFor="remove-ticket-movie">Select Movie:</label>
          <select
            id="remove-ticket-movie"
            value={removeTicketData.movieId}
            onChange={async (e) => {
              const selectedMovieId = e.target.value;
              setRemoveTicketData({ ...removeTicketData, movieId: selectedMovieId });

              if (selectedMovieId) {
                const tickets = await fetchAvailableTickets(Number(selectedMovieId));
                setAvailableTickets(tickets);
              } else {
                setAvailableTickets(0);
              }
            }}
          >
            <option value="">Select Movie</option>
            {movieList && movieList.length > 0 ? (
              movieList.map((movie) => (
                <option key={movie.movieId} value={movie.movieId}>
                  {movie.title}
                </option>
              ))
            ) : (
              <option value="">No movies available</option>
            )}
          </select>

          {removeTicketData.movieId && (
            <p>Available Tickets: {availableTickets}</p>
          )}

          <label htmlFor="remove-number-of-tickets">Number of Tickets to Remove:</label>
          <input
            type="number"
            id="remove-number-of-tickets"
            placeholder="Number of Tickets to Remove"
            value={removeTicketData.numberOfTickets}
            onChange={(e) =>
              setRemoveTicketData({ ...removeTicketData, numberOfTickets: e.target.value })
            }
            min="1"
            max={availableTickets}
          />

          <button type="button" onClick={removeTicketsFromMovie} disabled={availableTickets === 0}>
            Remove Tickets
          </button>
        </form>
      </section>

      {/* Edit Tickets */}
      <section>
        <h2>Edit Tickets</h2>
        <form>
          <label htmlFor="edit-ticket-movie">Select Movie:</label>
          <select
            id="edit-ticket-movie"
            value={editTicketData.movieId}
            onChange={(e) => {
              const selectedMovieId = e.target.value;
              setEditTicketData({ ...editTicketData, movieId: selectedMovieId, showTimeId: "" });
              // Fetch showtimes for the selected movie
              fetchShowTimesForEdit(selectedMovieId);
            }}
          >
            <option value="">Select Movie</option>
            {movieList && movieList.length > 0 ? (
              movieList.map((movie) => (
                <option key={movie.movieId} value={movie.movieId}>
                  {movie.title}
                </option>
              ))
            ) : (
              <option value="">No movies available</option>
            )}
          </select>

          <label htmlFor="edit-showtime">Select Showtime:</label>
          <select
            id="edit-showtime"
            value={editTicketData.showTimeId}
            onChange={(e) => {
              const selectedShowTimeId = e.target.value;
              setEditTicketData({ ...editTicketData, showTimeId: selectedShowTimeId });
            }}
            disabled={!editTicketData.movieId || showTimesForEdit.length === 0}
          >
            <option value="">Select Showtime</option>
            {showTimesForEdit.map((showTime) => (
              <option key={showTime.showTimeId} value={showTime.showTimeId}>
                {new Date(showTime.viewingTime).toLocaleString()}
              </option>
            ))}
          </select>

          <label htmlFor="edit-price">New Price:</label>
          <input
            type="number"
            id="edit-price"
            placeholder="New Price"
            value={editTicketData.price}
            onChange={(e) =>
              setEditTicketData({ ...editTicketData, price: e.target.value })
            }
            min="0"
            step="0.01"
          />

          <div className="checkbox-container">
            <input
              type="checkbox"
              id="availability"
              checked={editTicketData.availability}
              onChange={(e) =>
                setEditTicketData({ ...editTicketData, availability: e.target.checked })
              }
            />
            <label htmlFor="availability">Available</label>
          </div>

          <button type="button" onClick={editTickets}>
            Edit Tickets
          </button>
        </form>
      </section>
    </div>
  );
};

export default Management;
