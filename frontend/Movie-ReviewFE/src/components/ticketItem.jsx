import { useState, useEffect } from "react";
import fetchData from "../utils/request-utils";
import { useParams } from 'react-router-dom'; 


const TicketItem = ({ ticketID, ticketPrice }) => {
  const [movie, setMovie] = useState([]);
  const { movieId } = useParams();
  const { ticketRes, setTicketRes} = useState({});
  const fetchMovie = async () => {
    try {
      const res = await fetch(`http://localhost:5190/api/Movie/GetMovieById?id=${movieId}`);
      const data = await res.json();
      setMovie(data);
  } catch (err) {
      console.log(err);
  }
  }

  useEffect(() => {
    fetchMovie();
  }, []);

  const addTicket = async () => {
    const cartId = 5;
    const url = `http://localhost:5190/api/Cart/AddTicketToCart?cartId=${cartId}&ticketId=${ticketID}&quantity=1`;
    console.log(url);
    let cartRes = await fetch(url, {
        method: 'POST',
        headers: {
          'Accept': '*/*', // or a more specific accept header like 'application/json'
        }
      }
    )
      .then(response => response.json()) // Parse the JSON response
      .then(data => {
        console.log('Success:', data); // Handle the response data

      })
      .catch(error => {
        console.error('Error:', error); // Handle any errors that occur
      });
      console.log(cartRes)
    // setTicketRes(cartRes);
    
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
      <div className="ticket--simple--list">
        <p className="item--title">{movie.title}</p>
        <p>Price: ${ticketPrice}</p>
      </div>
        <button className="add--to--cart" onClick={addTicket}> Add to Cart </button>
    </div>
  )
}


export default TicketItem

