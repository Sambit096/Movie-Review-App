import { useState, useEffect } from "react";
import { useParams } from 'react-router-dom'; 
import fetchData from "../utils/request-utils";
import ticketItem from "../components/ticketItem";

const Tickets = () => {
  const { showtimeID } = useParams();
  const [tickets, setTickets] = useState([]);

// Fetch data from the GetShowtimes api endpoint
const fetchTickets = async (showtimeID) => {
  //Should be an API call to return all tickets for a showtimeID
  try { 
    const data = await fetchData(`http://localhost:5190/api/ShowTime/GetTicketsForShowTime/${showtimeID}`);
    console.log('tickers:',data);
    setTickets(data);
    console.log(tickets);
    
  } catch (err) {
    setTickets([])
  }  
}

useEffect(() => {
  console.log("showtimeID:", showtimeID);
  
  if (showtimeID) {
    fetchTickets(showtimeID);
    // console.log(tickets)
  } else {
    console.log("Error: showtimeID is undefined");
  }
}, [showtimeID]);

return (
  <>
    <h1>Tickets</h1>
    {/* {tickets.length == 0 ? <p>Sold Out</p>: tickets.map(ticket => (
      <ticketItem key={ticket.ticketID} id={ticket.ticketID} />
  ))} */}
  </>
)
}

export default Tickets