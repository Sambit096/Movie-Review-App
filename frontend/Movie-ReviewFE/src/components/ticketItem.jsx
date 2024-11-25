

const showtimeItem = ({ ticketID }) => {

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
        <div className="movie--item">
          <button onClick={addTicket}> Buy Ticket </button>
        </div>
    )
}


export default showtimeItem

