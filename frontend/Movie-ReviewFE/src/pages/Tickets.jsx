import { useState, useEffect } from "react";
import { useParams } from 'react-router-dom'; 
import fetchData from "../utils/request-utils";
import TicketItem from "../components/TicketItem.jsx";

const Tickets = () => {
  const { showtimeID } = useParams();
  const [tickets, setTickets] = useState([]);

// Fetch data from the GetShowtimes api endpoint
const fetchTickets = async (showtimeID) => {
  //Should be an API call to return all tickets for a showtimeID
  try { 
    const data = await fetchData(`http://localhost:5190/api/ShowTime/GetTicketsForShowTime/${showtimeID}`);
    setTickets(data);    
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
    <h1>Tickets</h1>
    {tickets.length == 0 ? <p>Sold Out</p>: tickets.map(ticket => (
      <TicketItem key={ticket.ticketID} id={ticket.ticketID} />
  ))}
  </>
)
}

export default Tickets