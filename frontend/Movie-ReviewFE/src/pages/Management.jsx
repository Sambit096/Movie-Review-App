import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "../Management.css"; // Import the CSS file

// Map numeric rating to MPAARating string
const ratingMap = {
  "0": "G",
  "1": "PG",
  "2": "PG13",
  "3": "R",
  "4": "NC17"
};

const Management = () => {
  const navigate = useNavigate();
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
    const updatedUser = JSON.parse(localStorage.getItem("user")) || {};
    if (updatedUser && updatedUser.userType !== "Admin") {
      navigate("/");
    } else {
      fetchMovies();
    }
  }, [navigate]);

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

      if (!showTimes || showTimes.length === 0) {
        return false;
      }

      return true;
    } catch (error) {
      console.error("Error fetching showtimes:", error);
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

      console.log("API Response for Available Tickets:", response);

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
    e.preventDefault();
    console.log("addMovie called");

    if (!movie.title.trim()) {
      alert("Please add a title.");
      return;
    }
    if (!movie.genre.trim()) {
      alert("Please add a genre.");
      return;
    }
    if (!movie.description.trim()) {
      alert("Please add a description.");
      return;
    }
    if (!movie.rating.trim()) {
      alert("Please select a rating.");
      return;
    }

    try {
      const movieData = {
        title: movie.title.trim(),
        genre: movie.genre.trim(),
        description: movie.description.trim(),
        rating: Number(movie.rating),
      };

      console.log("Making API call with data:", movieData);

      const response = await fetch("http://localhost:5190/api/Management/AddMovie", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(movieData),
      });

      const data = await response.json();

      if (response.ok) {
        alert(data.message);
        setMovie({ title: "", genre: "", description: "", rating: "" });
        fetchMovies();
      } else {
        alert(data.message);
      }
    } catch (err) {
      console.error("Error adding movie:", err.message);
      alert("Failed to add movie. Please try again.");
    }
  };

  // Edit an existing movie
  const editMovie = async () => {
    if (!editMovieData.movieId) {
      alert("Please select a movie.");
      return;
    }
    if (!editMovieData.title.trim()) {
      alert("Please add a title.");
      return;
    }
    if (!editMovieData.genre.trim()) {
      alert("Please add a genre.");
      return;
    }
    if (!editMovieData.description.trim()) {
      alert("Please add a description.");
      return;
    }
    if (editMovieData.rating === "") {
      alert("Please select a rating.");
      return;
    }

    try {
      const movieId = Number(editMovieData.movieId);
      const newMovieRating = ratingMap[editMovieData.rating];

      if (!newMovieRating) {
        alert("Invalid rating selected.");
        return;
      }

      const requestData = {
        oldMovie: {
          movieId: movieId,
          title: "",
          genre: "",
          description: "",
          rating: "G"
        },
        newMovie: {
          movieId: movieId,
          title: editMovieData.title.trim(),
          genre: editMovieData.genre.trim(),
          description: editMovieData.description.trim(),
          rating: newMovieRating
        }
      };

      console.log("EditMovie Request Data:", requestData);

      const response = await fetch("http://localhost:5190/api/Management/EditMovie", {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(requestData),
      });

      const data = await response.json();
      if (response.ok) {
        alert(data.Message || "Movie edited successfully!");
        setEditMovieData({
          movieId: "",
          title: "",
          genre: "",
          description: "",
          rating: "",
        });
        fetchMovies();
      } else {
        alert(data.Message || "Failed to edit movie.");
      }
    } catch (err) {
      console.error("Error editing movie:", err.message);
      alert("Failed to edit movie. Please try again.");
    }
  };

  // Add a showtime
  const addShowTime = async () => {
    if (!showTime.movieId) {
      alert("Please select a movie.");
      return;
    }
    if (!showTime.viewingTime.trim()) {
      alert("Please provide a Viewing Time.");
      return;
    }

    try {
      const movieId = Number(showTime.movieId);
      const showTimeData = {
        movieId: movieId,
        viewingTime: showTime.viewingTime,
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
      setShowTime({ movieId: "", viewingTime: "" });
    } catch (err) {
      console.error("Error adding showtime:", err);
      alert("Failed to add showtime. Please try again.");
    }
  };

  // Remove movie
  const removeMovie = async () => {
    if (!removeMovieId) {
      alert("Please select a movie.");
      return;
    }

    const movieId = Number(removeMovieId);
    if (isNaN(movieId)) {
      alert("Invalid Movie ID.");
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
    if (!ticketData.movieId) {
      alert("Please select a movie.");
      return;
    }
    if (
      !ticketData.numberOfTickets ||
      isNaN(ticketData.numberOfTickets) ||
      Number(ticketData.numberOfTickets) <= 0
    ) {
      alert("Please provide a valid number of tickets.");
      return;
    }

    const numberOfTickets = Number(ticketData.numberOfTickets);
    const movieId = Number(ticketData.movieId);

    try {
      const showtimesExist = await hasShowtimes(movieId);

      if (!showtimesExist) {
        alert("No showtimes associated with this movie. Please add a showtime first.");
        return;
      }

      const requestData = {
        movie: {
          movieId: movieId,
          title: "",
          genre: "",
          description: "",
          rating: "G"
        },
        numberOfTickets: numberOfTickets,
      };

      console.log("AddTicketsToMovie Request Data:", requestData);

      const response = await fetch("http://localhost:5190/api/Management/AddTicketsToMovie", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(requestData),
      });

      if (response.ok) {
        const data = await response.json();
        alert(data.Message || "Tickets added successfully!");
        setTicketData({ movieId: "", numberOfTickets: "" });
        setAvailableTickets(0);
      } else {
        const errorData = await response.json();
        console.log("Error response data:", errorData);

        if (errorData.errors) {
          const validationErrors = Object.values(errorData.errors)
            .flat()
            .join("\n");
          alert(`Validation Errors:\n${validationErrors}`);
        } else {
          alert(errorData.Message || "Failed to add tickets.");
        }
      }
    } catch (err) {
      console.error("Error adding tickets:", err.message);
      alert("Failed to add tickets. Please try again.");
    }
  };

  const removeTicketsFromMovie = async () => {
    if (!removeTicketData.movieId) {
      alert("Please select a movie.");
      return;
    }
    if (
      !removeTicketData.numberOfTickets ||
      isNaN(removeTicketData.numberOfTickets) ||
      Number(removeTicketData.numberOfTickets) <= 0
    ) {
      alert("Please provide a valid number of tickets to remove.");
      return;
    }

    const numberOfTickets = Number(removeTicketData.numberOfTickets);
    const movieId = Number(removeTicketData.movieId);

    try {
      const tickets = await fetchAvailableTickets(movieId);

      if (tickets === 0) {
        alert("No available tickets to remove for this movie.");
        return;
      }

      if (numberOfTickets > tickets) {
        alert(`Cannot remove ${numberOfTickets} tickets. Only ${tickets} tickets are available.`);
        return;
      }

      const requestData = {
        movie: {
          movieId: movieId,
          title: "",
          genre: "",
          description: "",
          rating: "G"
        },
        numberOfTickets: numberOfTickets,
      };

      console.log("RemoveTicketsFromMovie Request Data:", requestData);

      const response = await fetch("http://localhost:5190/api/Management/RemoveTicketsFromMovie", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(requestData),
      });

      if (response.ok) {
        const data = await response.json();
        alert(data.Message || "Tickets removed successfully!");
        setRemoveTicketData({ movieId: "", numberOfTickets: "" });
        setAvailableTickets(0);
      } else {
        const errorData = await response.json();
        console.log("Error response data:", errorData);

        if (errorData.errors) {
          const validationErrors = Object.values(errorData.errors)
            .flat()
            .join("\n");
          alert(`Validation Errors:\n${validationErrors}`);
        } else {
          alert(errorData.Message || "Failed to remove tickets.");
        }
      }
    } catch (err) {
      console.error("Error removing tickets:", err.message);
      alert("Failed to remove tickets. Please try again.");
    }
  };

  // Edit Tickets
  const editTickets = async () => {
    if (!editTicketData.movieId) {
      alert("Please select a movie.");
      return;
    }
    if (!editTicketData.showTimeId) {
      alert("Please select a showtime.");
      return;
    }
    if (editTicketData.price === "") {
      alert("Please enter a valid price.");
      return;
    }

    const price = parseFloat(editTicketData.price);
    if (isNaN(price) || price < 0) {
      alert("Please enter a valid price.");
      return;
    }

    const movieId = Number(editTicketData.movieId);
    const showTimeId = Number(editTicketData.showTimeId);
    const availability = editTicketData.availability;

    try {
      const ticketsAvailable = await fetchAvailableTickets(movieId);

      if (ticketsAvailable === 0) {
        alert("No tickets available to edit for this movie and showtime. Add tickets first.");
        return;
      }

      const requestData = {
        movie: {
          movieId: movieId,
          title: "",
          genre: "",
          description: "",
          rating: "G"
        },
        newTicket: {
          ticketId: 0,
          showTimeId: showTimeId,
          price: price,
          availability: availability,
          cartId: 0,
          showTime: {
            showTimeId: showTimeId,
            movieId: movieId,
            viewingTime: "2024-12-10T00:42:49.259Z", 
            movie: {
              movieId: movieId,
              title: "",
              genre: "",
              description: "",
              rating: "G"
            }
          },
          cart: {
            cartId: 0,
            total: 0,
            userId: 0,
            purchased: true,
            user: {
              userId: 0,
              email: "string",
              username: "string",
              firstName: "string",
              lastName: "string",
              gender: "None",
              ageGroup: "Teen",
              password: "string",
              notiPreference: "SMS",
              userType: "User"
            }
          }
        }
      };

      console.log("EditTickets Request Data:", requestData);

      const response = await fetch("http://localhost:5190/api/Management/EditTickets", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(requestData),
      });

      if (response.ok) {
        const data = await response.json();
        alert(data.Message || "Tickets updated successfully!");
        setEditTicketData({
          movieId: "",
          showTimeId: "",
          price: "",
          availability: false,
        });
        setShowTimesForEdit([]);
      } else {
        const errorData = await response.json();
        alert(errorData.Message || "Failed to edit tickets.");
        console.error("API call failed:", errorData.Message);
      }
    } catch (err) {
      console.error("Error editing tickets:", err.message);
      if (err.message.includes("Movie or Showtime not found")) {
        alert("Movie or Showtime not found. Please check your selections.");
      } else {
        alert("Failed to edit tickets. Please try again.");
      }
    }
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
              // Do not populate old values; leave them blank
              setEditMovieData({
                movieId: selectedMovieId,
                title: "",
                genre: "",
                description: "",
                rating: "",
              });
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
            disabled={availableTickets === 0}
          />

          <div className="checkbox-container">
            <input
              type="checkbox"
              id="availability"
              checked={editTicketData.availability}
              onChange={(e) =>
                setEditTicketData({ ...editTicketData, availability: e.target.checked })
              }
              disabled={availableTickets === 0}
            />
            <label htmlFor="availability">Available</label>
          </div>

          <button type="button" onClick={editTickets} disabled={availableTickets === 0}>
            Edit Tickets
          </button>
        </form>
      </section>
    </div>
  );
};

export default Management;
