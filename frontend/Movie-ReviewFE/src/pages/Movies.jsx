import { useState, useEffect } from 'react';
import MovieItem from "../components/MovieItem";

const Movies = () => {

    // const [movies, setMovies] = useState([]);
    const [movies, setMovies] = useState([]);

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
    useEffect(() => {
        // Set static movie data here
        setMovies([
            {
                "MovieId": 1,
                "Title": "The Shawshank Redemption",
                "Genre": "Drama",
                "Description": "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                "Rating": "R"
            },
            {
                "MovieId": 2,
                "Title": "The Godfather",
                "Genre": "Crime, Drama",
                "Description": "An organized crime dynasty's aging patriarch transfers control of his clandestine empire to his reluctant son.",
                "Rating": "R"
            },
            {
                "MovieId": 3,
                "Title": "The Dark Knight",
                "Genre": "Action, Crime, Drama",
                "Description": "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham.",
                "Rating": "PG13"
            },
            {
                "MovieId": 4,
                "Title": "Pulp Fiction",
                "Genre": "Crime, Drama",
                "Description": "The lives of two mob hitmen, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                "Rating": "R"
            },
            {
                "MovieId": 5,
                "Title": "The Lord of the Rings: The Fellowship of the Ring",
                "Genre": "Action, Adventure, Fantasy",
                "Description": "A meek hobbit from the Shire and eight companions set out on a journey to destroy a powerful ring and save Middle-earth from the Dark Lord Sauron.",
                "Rating": "PG13"
            },
            {
                "MovieId": 6,
                "Title": "Finding Nemo",
                "Genre": "Animation, Adventure, Comedy",
                "Description": "A clownfish named Marlin embarks on a journey to rescue his son, Nemo, who has been captured by a diver.",
                "Rating": "G"
            },
            {
                "MovieId": 7,
                "Title": "The Matrix",
                "Genre": "Action, Sci-Fi",
                "Description": "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.",
                "Rating": "R"
            },
            {
                "MovieId": 8,
                "Title": "Inception",
                "Genre": "Action, Adventure, Sci-Fi",
                "Description": "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a CEO.",
                "Rating": "PG13"
            },
            {
                "MovieId": 9,
                "Title": "Titanic",
                "Genre": "Drama, Romance",
                "Description": "A seventeen-year-old aristocrat falls in love with a kind but poor artist aboard the luxurious, ill-fated R.M.S. Titanic.",
                "Rating": "PG13"
            },
            {
                "MovieId": 10,
                "Title": "Jurassic Park",
                "Genre": "Action, Adventure, Sci-Fi",
                "Description": "During a preview tour, a theme park suffers a major power breakdown that allows its cloned dinosaur exhibits to run amok.",
                "Rating": "PG13"
            }
        ]);
    }, []);
    // useEffect(() => {
    //     fetchData();
    // }, []);

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