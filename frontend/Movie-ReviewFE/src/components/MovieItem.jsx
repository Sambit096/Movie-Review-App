import MovieButton from "./MovieButton"

const MovieItem = ({ title, id }) => {


    return(
        <div className="movie--item">
            <h2>{title}</h2>
            <MovieButton to={movieId} title={title}/>
        </div>
    )
}

export default MovieItem