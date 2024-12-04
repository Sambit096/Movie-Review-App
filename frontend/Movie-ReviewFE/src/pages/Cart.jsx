/*const Cart = () => {
    return (
        <h1>TODO</h1>
    )
}
*/
import React, { useEffect, useState } from 'react';

const Cart = () => {
    const [cart, setCart] = useState(null);
    const [cartId, setCartId] = useState(1); // Assuming cartId is 1 for now

    useEffect(() => {
        fetchCart();
    }, []);

    const fetchCart = async () => {
        try {
            const response = await fetch(`http://localhost:5190/api/Cart/GetCart?cartId=${cartId}`);
            const data = await response.json();
            setCart(data);
        } catch (error) {
            console.error('Error fetching cart:', error);
        }
    };

    const removeTicketFromCart = async (ticketId) => {
        try {
            const response = await fetch(`http://localhost:5190/api/Cart/RemoveTicketFromCart?cartId=${cartId}&ticketId=${ticketId}`, {
                method: 'POST'
            });
            if (response.ok) {
                fetchCart(); // Refresh cart after removing ticket
            } else {
                console.error('Error removing ticket from cart');
            }
        } catch (error) {
            console.error('Error removing ticket from cart:', error);
        }
    };

    if (!cart) {
        return <div>Loading...</div>;
    }

    const totalPrice = cart.tickets.reduce((total, ticket) => total + ticket.price, 0);

    return (
        <div>
            <h1>Cart</h1>
            {cart.tickets.length === 0 ? (
                <p>No items in cart</p>
            ) : (
                <div>
                    {cart.tickets.map(ticket => (
                        <div key={ticket.ticketId} style={{ marginBottom: '10px' }}>
                            Ticket ID: {ticket.ticketId}, Price: {ticket.price}, Availability: {ticket.availability ? 'Available' : 'Not Available'}
                            <button onClick={() => removeTicketFromCart(ticket.ticketId)} style={{ marginLeft: '10px' }}>Remove</button>
                        </div>
                    ))}
                    <h2>Total Price: {totalPrice}</h2>
                </div>
            )}
        </div>
    );
};

export default Cart;
//export default Cart