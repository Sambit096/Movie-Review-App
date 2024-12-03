import ShowtimeButton from "./ShowtimeButton"
import "../showtime.css";
import React, { useState, useEffect } from "react";



const ShowtimeItem = ({title, time, id }) => {
    const [date, setDate] = useState('');

    const formatTime = () => {
        const aDate = new Date(time);
        const options = { weekday: 'long', month: 'short', day: 'numeric', hour: '2-digit', minute: '2-digit', };
        const dateString = aDate.toLocaleString('en-US', options);
        // Output: "Monday, December 2, 2024"

        return dateString;
    }
    useEffect(() => {
        setDate(formatTime(time)); // Set date when component mounts
    }, []);

    return(
        <div className="showtime--item">
            <h2>{date}</h2>
            <ShowtimeButton to={id} />
        </div>
    )
}

export default ShowtimeItem