import { useState, useEffect } from "react";
import { useParams } from 'react-router-dom'; 
import fetchData from "../utils/request-utils";
import TicketItem from "../components/TicketItem.jsx";
import "../showtime.css"; // Import the CSS file

const Tickets = () => {
  const { showtimeID } = useParams();
  const { movieID } = useParams();
  const [tickets, setTickets] = useState([]);

// Fetch data from the GetShowtimes api endpoint
const fetchTickets = async (showtimeID) => {
  //Should be an API call to return all tickets for a showtimeID
  try { 
    const data = await fetchData(`http://localhost:5190/api/ShowTime/GetTicketsForShowTime/${showtimeID}`);
    setTickets(data);
    console.log(data);
  } catch (err) {
    setTickets([])
  }  
}

useEffect(() => {  
  if (showtimeID) {
    fetchTickets(showtimeID);
  } else {
    console.log("Error: showtimeID is undefined");
  }
}, [showtimeID]);

return (
  <>
    <h1>Available Tickets</h1>
    <div className="tickets--container">
      {tickets.filter(ticket => ticket.availability === true).length == 0 ? <p>Sold Out</p>: tickets.filter(ticket => ticket.availability === true).map(ticket => (
        <TicketItem key={ticket.ticketId} ticketID={ticket.ticketId} ticketPrice={ticket.price}/>
      ))}     
    </div>
  </>
)
}

export default Tickets