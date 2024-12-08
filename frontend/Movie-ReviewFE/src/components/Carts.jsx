const Carts = ({ data, movies, showTimes }) => {
    // Helper to find a movie by ID
    const getMovieById = (movieId) => movies.find((movie) => movie.movieId === movieId);

    // Helper to find a showtime by ID
    const getShowTimeById = (showTimeId) => showTimes.find((showTime) => showTime.showTimeId === showTimeId);

    return (
        <div style={{ textAlign: "center" }}>
            {data.map((entry, index) => {
                const { cart, tickets } = entry;

                return (
                    <div key={index} style={{
                        border: "1px solid #ccc",
                        margin: "20px auto",
                        padding: "20px",
                        width: "80%",
                        borderRadius: "10px",
                    }}>
                        <h2>Cart ID: {cart.cartId}</h2>
                        <p><strong>Total:</strong> ${cart.total.toFixed(2)}</p>
                        <p><strong>Total w/ Tax:</strong> ${(cart.total * 1.06).toFixed(2)}</p>
                        <h3>Tickets in Cart:</h3>
                        {tickets.length > 0 ? (
                            <ul style={{padding: '0px', margin: '0px' }}>
                                {tickets.map((ticket) => {
                                    const showTime = getShowTimeById(ticket.showTimeId);
                                    const movie = showTime ? getMovieById(showTime.movieId) : null;

                                    return (
                                        <li key={ticket.ticketId} style={{
                                            marginBottom: "20px",
                                            padding: "10px",
                                            border: "1px solid #ddd",
                                            borderRadius: "5px",
                                            display: "inline-block",
                                            textAlign: "center",
                                            width: "80%",
                                        }}>
                                            <p><strong>Ticket ID:</strong> {ticket.ticketId}</p>
                                            <p><strong>Price:</strong> ${ticket.price.toFixed(2)}</p>
                                            {movie && (
                                                <>
                                                    <p><strong>Movie:</strong> {movie.title}</p>
                                                    <p><strong>Genre:</strong> {movie.genre}</p>
                                                    <p><strong>Rating:</strong> {movie.rating}</p>
                                                </>
                                            )}
                                            {showTime && (
                                                <>
                                                    <p><strong>Showtime:</strong> {new Date(showTime.viewingTime).toLocaleString()}</p>
                                                </>
                                            )}
                                        </li>
                                    )
                                })}
                            </ul>
                        ) : (
                            <p>No tickets found in this cart.</p>
                        )}
                    </div>
                )
            })}
        </div>
    );
};

export default Carts;
