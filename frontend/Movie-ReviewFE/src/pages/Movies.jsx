import { useState, useEffect } from 'react';
import MovieItem from "../components/MovieItem";
import fetchData from "../utils/request-utils";
import { useUser } from '../UserContext';

const Movies = () => {

    const [movies, setMovies] = useState([]);
    const { user } = useUser();

    // Fetch data from the GetMovies api endpoint
    // const fetchData = async () => {
    //     try {
    //         const res = await fetch('http://localhost:5190/api/Movie/GetMovies');
    //         if(res.ok) {
    //             const data = await res.json();
    //             setMovies(data);
    //         }
    //     } catch (err) {
    //         console.log(err);
    //     }
    // }
    
    useEffect(() => {
        const fetchMovies = async () => {
            try {
                const data = await fetchData('http://localhost:5190/api/Movie/GetMovies');
                setMovies(data);
            } catch (err) {
                console.log(err);
            }
        };

        fetchMovies();
    }, []);

    return (
        <div className='movie--list'>
            {user && <h1>Welcome, {user.username}!</h1>}
            <h2>Currently Playing:</h2>
            {movies.map(movie => (
                <MovieItem key={movie.movieId} id={movie.movieId} title={movie.title}/>
            ))}
        </div>
    )
}

export default Movies