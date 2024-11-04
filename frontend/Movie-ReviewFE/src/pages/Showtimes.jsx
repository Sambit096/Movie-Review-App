const Showtimes = () => {
    const { movieId } = useParams();
    const [showtimes, setShowtimes] = useState([]);
    const [movie, setMovie] = useState({});

    // Fetch data from the GetShowtimes api endpoint
  // const fetchData = async () => {
  //   //Should be an API call to return all showtimes for a movie by ID
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
  //   //Should be an API call to return info about a movie by ID
  //     try { 
  //       const res = await fetch('http://localhost:5190/api/ShowTime/GetShowTimes/${movieId}');
  //       if(res.ok) {
  //           const data = await res.json();
  //           console.log(data);
  //           setShowtimes(data);
  //       }
  //     } catch (err) {
  //       console.log(err);
  //     }
    
      
  // }

  // useEffect(() => {
  //   fetchData();
  // })

  return (
    <>
      <h1>{movie.title}</h1>
      {showtimes.map(showtime => (
        <ShowtimeItem key={showtime.showtimeID} id={movie.showtimeId} time={showtime.time}/>
    ))}
    </>
  )
}

export default Showtimes