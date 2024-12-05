/*const Cart = () => {
    return (
        <h1>TODO</h1>
    )
}
*/
import React, { useEffect, useState } from 'react';
import fetchData from "../utils/request-utils";

const Cart = () => {
    const [cart, setCart] = useState({ cart: null, tickets: [] });

    // Function to fetch the cart from localStorage
    //const fetchCartFromLocalStorage = () => {
    //     const storedCart = localStorage.getItem('cart');
    //     if (storedCart) {
    //         setCart(JSON.parse(storedCart));
    //     }
    // };

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
    // Listen for localStorage changes
    // useEffect(() => {
    //     const handleStorageChange = () => {
    //         fetchCartFromLocalStorage();
    //     };
    //     window.addEventListener('storage', handleStorageChange);
    //     return () => window.removeEventListener('storage', handleStorageChange);
    // }, []);

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
                    <button>Checkout</button>
                </div>
            )}
        </div>
    );
};

export default Cart;
