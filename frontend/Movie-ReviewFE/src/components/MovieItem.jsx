import MovieButton from "./MovieButton"

const MovieItem = ({ title, movieId }) => {


    return(
        <div className="movie--item">
            <MovieButton to={movieId} title={title}/>
        </div>
    )
}

export default MovieItem