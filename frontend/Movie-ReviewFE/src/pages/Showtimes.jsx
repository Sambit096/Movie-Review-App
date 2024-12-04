import fetchData from "../utils/request-utils";
import { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom'; 
import ShowtimeItem from "../components/ShowtimeItem";
import "../showtime.css"; // Import the CSS file


const Showtimes = () => {
  const { movieId } = useParams();
  const [showtimes, setShowtimes] = useState([]);
  const [movie, setMovie] = useState([]);

  const fetchMovie = async () => {
    try {
      const res = await fetch(`http://localhost:5190/api/Movie/GetMovieById?id=${movieId}`);
      const data = await res.json();
      console.log(data);
      setMovie(data);
  } catch (err) {
      console.log(err);
  }
  }
  const fetchShowtimes = async () => {
    try {
        const data = await fetchData(`http://localhost:5190/api/ShowTime/GetShowTimes/${movieId}`);
        setShowtimes(data);
    } catch (err) {
      console.log(err);
    }
};
const addToCart = (ticket) => {
  let cart = JSON.parse(localStorage.getItem('cart')) || { tickets: [] };
  cart.tickets.push(ticket);
  localStorage.setItem('cart', JSON.stringify(cart));
  alert("Ticket added to cart!"); 
};
  useEffect(() => {

    fetchMovie();
    fetchShowtimes();
    console.log(showtimes)
  }, []);

  return (
    <>
      <h1 className='title'>Showtimes for {movie.title}</h1>
      <div className='showtime--container'>
        {showtimes.map(showtime => (
          <ShowtimeItem key={showtime.showTimeId} id={showtime.showTimeId} title={showtime.movieId} time={showtime.viewingTime}/>
        ))}
      </div>
    </>
  );
};

export default Showtimes;