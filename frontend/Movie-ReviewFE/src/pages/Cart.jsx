import React, { useEffect, useState } from "react";
import fetchData from "../utils/request-utils";

const Cart = () => {
  const [cart, setCart] = useState({ cart: null, tickets: [] });
  const [movies, setMovies] = useState([]);
  const [showTimes, setShowTimes] = useState([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [paymentInfo, setPaymentInfo] = useState({
    cardNumber: "",
    expirationDate: "",
    cardHolderName: "",
    cvc: "",
  });
  const [paymentError, setPaymentError] = useState("");

  useEffect(() => {
    fetchMovieData();
    fetchShowTimeData();
    fetchCart();
  }, []);

  const fetchMovieData = async () => {
    try {
      const response = await fetch("http://localhost:5190/api/Movie/GetMovies");
      if (!response.ok) {
        throw new Error("Failed to fetch movie data.");
      }
      const data = await response.json();
      setMovies(data);
    } catch (err) {
      console.log(err.message);
    }
  };

  const fetchShowTimeData = async () => {
    try {
      const response = await fetch(
        "http://localhost:5190/api/ShowTime/GetAllShowTimes"
      );
      if (!response.ok) {
        throw new Error("Failed to fetch showtime data.");
      }
      const data = await response.json();
      setShowTimes(data);
    } catch (err) {
      console.log(err.message);
    }
  };

  const fetchCart = async () => {
    try {
      const cartId = JSON.parse(localStorage.getItem("cart")).cartId;
      const response = await fetchData(
        `http://localhost:5190/api/Cart/GetCart?cartId=${cartId}`
      );
      setCart(response);
    } catch (err) {
      console.log("Failed to fetch cart");
    }
  };

  const fetchNewCart = async () => {
    try {
      localStorage.removeItem("cart");
      const userId = JSON.parse(localStorage.getItem("user")).userId;
      const response = await fetchData(
        `http://localhost:5190/api/Cart/GetCartIdByUser?userId=${userId}`
      );
      localStorage.setItem("cart", JSON.stringify({ cartId: response }));
      fetchCart();
    } catch (err) {
      console.log("Failed to fetch cart");
    }
  };
  // Remove a ticket from the cart
  const removeTicketFromCart = async (ticketId) => {
    try {
      const cartId = JSON.parse(localStorage.getItem("cart")).cartId;
      const response = await fetch(
        `http://localhost:5190/api/Cart/RemoveTicketFromCart?cartId=${cartId}&ticketId=${ticketId}`,
        {
          method: "POST",
          headers: { "Content-Type": "application/json" },
        }
      );
      fetchCart();
    } catch (err) {
      console.log("Failed to remove ticket");
    }
  };

  const sendEmailNotification = async (cartId) => {
    try {
      const response = await fetch(
        `http://localhost:5190/api/Mail/SendEmail?cartId=${cartId}`,
        {
          method: "POST",
          headers: { "Content-Type": "application/json" },
        }
      );
    } catch (err) {
      console.log("Failed to send notification");
    }
  };

  const totalPrice = cart.tickets?.reduce(
    (total, ticket) => total + ticket.price,
    0
  ).toFixed(2);
  const totalPriceWithTax = (
    cart.tickets?.reduce((total, ticket) => total + ticket.price, 0) * 1.06
  ).toFixed(2);

  const getMovieById = (movieId) =>
    movies.find((movie) => movie.movieId === movieId);
  const getShowTimeById = (showTimeId) =>
    showTimes.find((showTime) => showTime.showTimeId === showTimeId);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setPaymentInfo({ ...paymentInfo, [name]: value });
  };

  const handleCheckout = async () => {
    try {
      const cartId = JSON.parse(localStorage.getItem("cart")).cartId;
      const response = await fetch(
        `http://localhost:5190/api/Cart/ProcessPayment?cartId=${cartId}&cardNumber=${paymentInfo.cardNumber}&exp=${paymentInfo.expirationDate}&cardHolderName=${paymentInfo.cardHolderName}&cvc=${paymentInfo.cvc}`,
        {
          method: "POST",
          headers: { "Content-Type": "application/json" },
        }
      );
      if (response.ok) {
        alert("Payment successful!");
        if (
          JSON.parse(localStorage.getItem("user")).notiPreference == "Both" ||
          JSON.parse(localStorage.getItem("user")).notiPreference == "Email"
        ) {
          sendEmailNotification(cartId);
        }
        fetchNewCart();
        setIsModalOpen(false);
      } else {
        const errorData = await response.json();
        const conciseMessage = errorData.message
          .split("\n")[0]
          .split(":")
          .pop()
          .trim();
        setPaymentError(conciseMessage || "Payment failed. Please try again.");
      }
    } catch (err) {
      setPaymentError(err);
    }
  };
  return (
    <div>
      <h1>Your Cart</h1>
      {cart.tickets.length === 0 ? (
        <p>No items in cart</p>
      ) : (
        <div>
          {cart.tickets.map((ticket) => {
            const showTime = getShowTimeById(ticket.showTimeId);
            const movie = getMovieById(showTime.movieId); // Assuming showTime includes `movieId`

            return (
              <div
                key={ticket.ticketId}
                style={{
                  marginBottom: "15px",
                  border: "1px solid #ddd",
                  borderRadius: "5px",
                  padding: "10px",
                }}
              >
                <div>
                  <strong>Ticket ID:</strong> {ticket.ticketId}
                </div>
                <div>
                  <strong>Movie:</strong>{" "}
                  {movie ? movie.title : "Unknown Movie"}{" "}
                </div>
                <div>
                  <strong> Showtime:</strong>{" "}
                  {showTime
                    ? new Date(showTime.viewingTime).toLocaleString()
                    : "Unknown Showtime"}
                </div>
                <div>
                  <strong>Price:</strong> ${ticket.price.toFixed(2)}
                  <button
                    onClick={() => removeTicketFromCart(ticket.ticketId)}
                    style={{ marginLeft: "10px" }}
                  >
                    Remove
                  </button>
                </div>
              </div>
            );
          })}
          <h2>Total Price: ${totalPrice}</h2>
          <h2>Total Price with Tax: ${totalPriceWithTax}</h2>
          <button onClick={() => setIsModalOpen(true)}>Checkout</button>
        </div>
      )}

      {isModalOpen && (
        <div className="modal">
          <div className="modal-content">
            <h2>Enter Payment Details</h2>
            <div>
              <label>
                Card Number:
                <input
                  type="text"
                  name="cardNumber"
                  value={paymentInfo.cardNumber}
                  onChange={handleInputChange}
                />
              </label>
            </div>
            <div>
              <label>
                Expiration Date:
                <input
                  type="text"
                  name="expirationDate"
                  value={paymentInfo.expirationDate}
                  onChange={handleInputChange}
                  placeholder="MM/YY"
                />
              </label>
            </div>
            <div>
              <label>
                Card Holder Name:
                <input
                  type="text"
                  name="cardHolderName"
                  value={paymentInfo.cardHolderName}
                  onChange={handleInputChange}
                />
              </label>
            </div>
            <div>
              <label>
                CVC:
                <input
                  type="text"
                  name="cvc"
                  value={paymentInfo.cvc}
                  onChange={handleInputChange}
                />
              </label>
            </div>
            {paymentError && <p style={{ color: "red" }}>{paymentError}</p>}
            <button onClick={handleCheckout} style={{ margin: "10px" }}>
              Submit Payment
            </button>
            <button onClick={() => setIsModalOpen(false)}>Close</button>
          </div>
        </div>
      )}
    </div>
  );
};

export default Cart;
