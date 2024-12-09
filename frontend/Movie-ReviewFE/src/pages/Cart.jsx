import React, { useEffect, useState } from 'react';
import fetchData from "../utils/request-utils";

const Cart = () => {
    const [cart, setCart] = useState({ cart: null, tickets: [] });
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [paymentInfo, setPaymentInfo] = useState({
        cardNumber: "",
        expirationDate: "",
        cardHolderName: "",
        cvc: "",
    });
    const [paymentError, setPaymentError] = useState("");

    // Load cart when component mounts
    useEffect(() => {
          fetchCart();
    }, []);

    const fetchCart = async () => {
        try {
        const cartId = (JSON.parse(localStorage.getItem('cart')).cartId);
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
            localStorage.removeItem('cart');
            const userId = (JSON.parse(localStorage.getItem('user')).userId);
              const response = await fetchData(
                `http://localhost:5190/api/Cart/GetCartIdByUser?userId=${userId}`
              );
              localStorage.setItem('cart', JSON.stringify({cartId: response}))
              fetchCart();
            } catch (err) {
              console.log("Failed to fetch cart");
            }
    }
    // Remove a ticket from the cart
    const removeTicketFromCart = async (ticketId) => {
        try {
            const cartId = (JSON.parse(localStorage.getItem('cart')).cartId);
              const response = await fetch(
                `http://localhost:5190/api/Cart/RemoveTicketFromCart?cartId=${cartId}&ticketId=${ticketId}`, {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                });
              fetchCart();
            } catch (err) {
              console.log("Failed to remove ticket");
            }
    };

    const totalPrice = cart.tickets?.reduce((total, ticket) => total + ticket.price, 0);
    const totalPriceWithTax = ((cart.tickets?.reduce((total, ticket) => total + ticket.price, 0)) * 1.06).toFixed(2);

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
        fetchNewCart();
        setIsModalOpen(false);
        } else {
            const errorData = await response.json();
            const conciseMessage = errorData.message.split('\n')[0].split(':').pop().trim();
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
                    {cart.tickets.map((ticket) => (
                        <div key={ticket.ticketId} style={{ marginBottom: '10px' }}>
                            Ticket ID: {ticket.ticketId}, ShowTime ID: {ticket.showTimeId}, Price: {ticket.price}
                            <button
                                onClick={() => removeTicketFromCart(ticket.ticketId)}
                                style={{ marginLeft: '10px' }}
                            >
                                Remove
                            </button>
                        </div>
                    ))}
                    <h2>Total Price: ${totalPrice}</h2>
                    <h2>Total Price with Tax: ${totalPriceWithTax}</h2>
                    <button onClick={() => setIsModalOpen(true)}>Checkout</button>
                </div>
            )}

            {isModalOpen && (
                    <div
                    className='modal'
                    >
                    <div
                        className='modal-content'
                    >
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
