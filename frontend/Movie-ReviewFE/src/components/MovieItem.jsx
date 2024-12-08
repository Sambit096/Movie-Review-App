// MovieItem.jsx
import React from "react";
import MovieButton from "./MovieButton";
import ReviewButton from "./ReviewButton";

const MovieItem = ({ title, id, desc, genre, rating }) => {
    return (
        <div className="movie--item">
            <div className="movie--details">
                <h2>{title}</h2>
                <p>{desc}</p>
                <div className="movie--metadata">
                    <p><strong>Genre:</strong> {genre}</p>
                    <p><strong>Rating:</strong> {rating}</p>
                </div>
            </div>
            <div className="movie--buttons">
                <MovieButton to={id} title={title} />
                <ReviewButton to={id} title={title} />
            </div>
        </div>
    );
};

export default MovieItem;
