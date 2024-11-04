import { useState, useEffect } from 'react';
import MovieItem from "../components/MovieItem";

const Movies = () => {

    // const [movies, setMovies] = useState([]);

    // Fetch data from the GetMovies api endpoint
    // const fetchData = async () => {
    //     try {
    //         const res = await fetch('http://localhost:5190/api/Movie/GetMovies');
    //         if(res.ok) {
    //             const data = await res.json();
    //             console.log(data);
    //             setMovies(data);
    //         }
    //     } catch (err) {
    //         console.log(err);
    //     }
    // }
    const [movies, setMovies] = useState([]);

    
    useEffect(() => {
        // fetchData();
        setMovies([{
            "movieId": 1,
            "title": "Eternal Horizons",
            "genre": "Science Fiction",
            "description": "In a distant future, humanity discovers a portal to alternate realities, but each choice leads to unforeseen consequences.",
            "rating": 8
          },
          {
            "movieId": 2,
            "title": "Whispers in the Dark",
            "genre": "Horror",
            "description": "A group of friends must confront their deepest fears when they spend a night in a haunted mansion with a dark history.",
            "rating": 7
          },
          {
            "movieId": 3,
            "title": "The Last Melody",
            "genre": "Drama",
            "description": "A once-famous musician grapples with his fading legacy while trying to reconnect with his estranged daughter.",
            "rating": 9
          },
          {
            "movieId": 4,
            "title": "Galactic Quest",
            "genre": "Adventure",
            "description": "An unlikely hero teams up with a diverse crew of intergalactic misfits to save the galaxy from an ancient evil.",
            "rating": 8
          },
          {
            "movieId": 5,
            "title": "Love in Bloom",
            "genre": "Romantic Comedy",
            "description": "Two rival florists compete for the title of best florist in town, only to find themselves falling for each other.",
            "rating": 7
          }])
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