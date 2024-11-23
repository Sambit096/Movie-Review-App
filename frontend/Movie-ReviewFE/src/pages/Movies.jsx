import { useState, useEffect } from 'react';
import MovieItem from "../components/MovieItem";
import fetchData from "../utils/request-utils";
import { useUser } from '../UserContext';
import { useNavigate  } from 'react-router-dom'; 

const Movies = () => {
    const nav = useNavigate();
    const [movies, setMovies] = useState([]);
    const [filteredMovies, setFilteredMovies] = useState([]);
    const [selectedGenre, setSelectedGenre] = useState('');
    const { user } = useUser();
    const [userData, setUserData] = useState(null);
    
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
        const storedValue = localStorage.getItem('user');
        if (storedValue) {
            const parse = JSON.parse(storedValue);
            setUserData(parse);
        }
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
            {userData && <h1>Welcome, {userData.username}!</h1>}
            <h2>Currently Playing:</h2>

            <div>
                <label htmlFor="genre-filter">Filter by Genre: </label>
                <select id="genre-filter" value={selectedGenre} onChange={handleGenreChange}>
                    <option value="">All</option>
                    {[...new Set(movies.map(movie => movie.genre))].map(genre => (
                        <option key={genre} value={genre}>{genre}</option>
                    ))}
                </select>
                <div className="my-reviews"><button onClick={() => nav('/MyReviews')}>View My Reviews</button></div>
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