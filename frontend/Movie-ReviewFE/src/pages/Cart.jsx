/*const Cart = () => {
    return (
        <h1>TODO</h1>
    )
}
*/
import React, { useEffect, useState } from 'react';

const Cart = () => {
    const [cart, setCart] = useState({ tickets: [] });

    // Function to fetch the cart from localStorage
    const fetchCartFromLocalStorage = () => {
        const storedCart = localStorage.getItem('cart');
        if (storedCart) {
            setCart(JSON.parse(storedCart));
        }
    };

    // Load cart when component mounts
    useEffect(() => {
        fetchCartFromLocalStorage();
    }, []);

    // Listen for localStorage changes
    useEffect(() => {
        const handleStorageChange = () => {
            fetchCartFromLocalStorage();
        };
        window.addEventListener('storage', handleStorageChange);
        return () => window.removeEventListener('storage', handleStorageChange);
    }, []);

    // Remove a ticket from the cart
    const removeTicketFromCart = (ticketId) => {
        const updatedCart = {
            ...cart,
            tickets: cart.tickets.filter((ticket) => ticket.ticketId !== ticketId),
        };
        setCart(updatedCart);
        localStorage.setItem('cart', JSON.stringify(updatedCart));
    };

    const totalPrice = cart.tickets.reduce((total, ticket) => total + ticket.price, 0);

    return (
        <div>
            <h1>Your Cart</h1>
            {cart.tickets.length === 0 ? (
                <p>No items in cart</p>
            ) : (
                <div>
                    {cart.tickets.map((ticket) => (
                        <div key={ticket.ticketId} style={{ marginBottom: '10px' }}>
                            Ticket ID: {ticket.ticketId}, Price: {ticket.price}, Availability: {ticket.availability ? 'Available' : 'Not Available'}
                            <button
                                onClick={() => removeTicketFromCart(ticket.ticketId)}
                                style={{ marginLeft: '10px' }}
                            >
                                Remove
                            </button>
                        </div>
                    ))}
                    <h2>Total Price: ${totalPrice}</h2>
                </div>
            )}
        </div>
    );
};

export default Cart;
