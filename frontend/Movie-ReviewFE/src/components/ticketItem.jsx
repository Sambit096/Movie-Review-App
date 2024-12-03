import { useState, useEffect } from "react";
import fetchData from "../utils/request-utils";
import { useParams } from 'react-router-dom'; 


const TicketItem = ({ ticketID }) => {
  const [movie, setMovie] = useState([]);
  const { movieId } = useParams();

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

  useEffect(() => {
    fetchMovie();
  }, []);

  const addTicket = async () => {

// Needs an addTicket API request
    // try {
    //     const response = await fetch('https://api.example.com/data'); // Replace with your API endpoint
    //     if (!response.ok) {
    //         throw new Error('Network response was not ok');
    //     }
    //     const result = await response.json();
    //     setData(result); // Set the data state with the fetched data
    // } catch (error) {
    //     setError(error.message); // Capture any errors
    // } finally {
    //     setLoading(false); // Reset loading state
    // }
};
  return(
    <div className="ticket--item">
      <p>{}</p>
      <button onClick={addTicket}> Buy Ticket </button>
    </div>
  )
}


export default TicketItem

