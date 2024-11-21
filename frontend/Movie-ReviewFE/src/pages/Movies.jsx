import { useState, useEffect } from 'react';
import MovieItem from "../components/MovieItem";
import fetchData from "../utils/request-utils";
import { useUser } from '../UserContext';

const Movies = () => {

    const [movies, setMovies] = useState([]);
    const [filteredMovies, setFilteredMovies] = useState([]);
    const [selectedGenre, setSelectedGenre] = useState('');
    const { user } = useUser();
    
    useEffect(() => {
        const fetchMovies = async () => {
            try {
                const data = await fetchData('http://localhost:5190/api/Movie/GetMovies');
                setMovies(data);
                setFilteredMovies(data);
            } catch (err) {
                console.log(err);
            }
        };

        fetchMovies();
    }, []);

    const handleGenreChange = (event) => {
        const genre = event.target.value;
        setSelectedGenre(genre);

        // Filter movies by genre or show all if no genre selected
        if (genre === '') {
            setFilteredMovies(movies);
        } else {
            setFilteredMovies(movies.filter(movie => movie.genre === genre));
        }
    };


    return (
        <div>
            <div className='heading--filter'>
            {user && <h1>Welcome, {user.username}!</h1>}
            <h2>Currently Playing:</h2>

            {/* Genre Filter */}
            <div>
                <label htmlFor="genre-filter">Filter by Genre: </label>
                <select id="genre-filter" value={selectedGenre} onChange={handleGenreChange}>
                    <option value="">All</option>
                    {/* Create unique genre options dynamically */}
                    {[...new Set(movies.map(movie => movie.genre))].map(genre => (
                        <option key={genre} value={genre}>{genre}</option>
                    ))}
                </select>
            </div>
            </div>
            <div className="movie--list">
                {filteredMovies.map(movie => (
                    <MovieItem key={movie.movieId} id={movie.movieId} title={movie.title} desc={movie.description} genre={movie.genre}/>
                ))}
            </div>
        </div>
    )
}

export default Movies