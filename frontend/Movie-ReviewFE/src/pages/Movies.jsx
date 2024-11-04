import { useState, useEffect } from 'react';
import MovieItem from "../components/MovieItem";
import fetchData from "../utils/request-utils"

const Movies = () => {

    const [movies, setMovies] = useState([]);

    useEffect(() => {
        const fetchMovies = async () => {
            try {
                const data = await fetchData('http://localhost:5190/api/Movie/GetMovies');
                setMovies(data);
            } catch (err) {
                setError('Failed to load movies.');
            }
        };

        fetchMovies();
    }, []);

    return (
        <div>
            <h1>Currently Playing:</h1>
            {movies.map(movie => (
                <MovieItem key={movie.movieId} id={movie.movieId} title={movie.title}/>
            ))}
        </div>
    )
}

export default Movies