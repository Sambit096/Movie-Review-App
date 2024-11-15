const Showtimes = () => {
  const { showtimeID } = useParams();
  const [tickets, setTickets] = useState([]);

  // Fetch data from the GetShowtimes api endpoint
// const fetchData = async () => {
//   //Should be an API call to return all tickets for a showtimeID
//     try { 
//         const res = await fetch('http://localhost:5190/api/ShowTime/GetShowTimes/${movieId}');
//         if(res.ok) {
//             const data = await res.json();
//             console.log(data);
//             setShowtimes(data);
//         }
//     } catch (err) {
//         console.log(err);
//     }  
// }

// useEffect(() => {
//   fetchData();
// })

return (
  <>
    <h1>{movie.title}</h1>
    {tickets.map(ticket => (
      <ticketItem key={ticket.ticketID} id={ticket.ticketID} />
  ))}
  </>
)
}

export default Showtimes