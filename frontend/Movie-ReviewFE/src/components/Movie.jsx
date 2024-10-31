import { useParams } from "react-router-dom"
import { useEffect, useState } from "react"

const Movie = () => {

    const { id } = useParams()
    const [movie, setMovie] = useState({});

    const fetchData = async () => {

        try {
            const res = await fetch(`http://localhost:5190/api/Movie/GetMovieById?id=${id}`);
            const data = await res.json();
            console.log(data);
            setMovie(data);
        } catch (err) {
            console.log(err);
        }
    }

    useEffect(() => {
        fetchData();
    }, []);

    return (
        <section>
            <h1>{movie.title}</h1>
            <h2>{movie.description}</h2>
        </section>
    )
}

export default Movie