import { useState, useEffect } from 'react';
import MovieItem from "../components/MovieItem";

const Movies = () => {

    const [movies, setMovies] = useState([]);

    // Fetch data from the GetMovies api endpoint
    const fetchData = async () => {
        try {
            const res = await fetch('http://localhost:5190/api/Movie/GetMovies');
            if(res.ok) {
                const data = await res.json();
                console.log(data);
                setMovies(data);
            }
        } catch (err) {
            console.log(err);
        }
    }
    
    useEffect(() => {
        fetchData();
    }, []);

    return (
        <div>
            <h1>Currently Playing:</h1>
            {movies.map(movie => (
                <MovieItem key={movie.MovieId} title={movie.Title} movieId={movie.MovieId}/>
            ))}
        </div>
    )
}

export default Movies