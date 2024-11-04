import fetchData from "../utils/request-utils"
import { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom'; 
import ShowtimeItem from "../components/ShowtimeItem";


const Showtimes = () => {
    const { movieId } = useParams();
    const [showtimes, setShowtimes] = useState([]);
    const [movie, setMovie] = useState([]);


useEffect(() => {
  const fetchShowtimes = async () => {
      try {
          const data = await fetchData(`http://localhost:5190/api/ShowTime/GetShowTimes/${movieId}`);
          setShowtimes(data);
          console.log(data);
      } catch (err) {
        console.log(err);
      }
  };

  fetchShowtimes();
  console.log(showtimes)
}, []);
useEffect(() => {
  const fetchMovie = async() => {
    try {
      const movieData = await fetchData(`http://localhost:5190/api/Movie/GetMovieById/${movieId}`);
      setMovie(data);
      console.log(data)
    } catch {
      console.log(err);
    }
  }
})

  return (
    <>
      <h1>Showtimes for {movie.movieId}</h1>
      {showtimes.map(showtime => (
        <ShowtimeItem key={showtime.showTimeId} id={showtime.showTimeId} title={showtime.movieId} time={showtime.viewingTime}/>
      ))}
    </>
  )
}

export default Showtimes