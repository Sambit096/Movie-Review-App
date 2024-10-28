import { useState, useEffect } from 'react';
import Movie from '../components/Movie';

const Home = () => {

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
        <section className="page" id="home--page">
            <h1 className="page--header">Welcome to our Movie review site!</h1>
            {movies.map(movie => (
                <Movie key={movie.movieId} title={movie.title}/>
            ))}
        </section>
    )
}

export default Home