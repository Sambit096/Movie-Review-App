import MovieButton from "./MovieButton"

const MovieItem = ({ title, id }) => {


    return(
        <div className="movie--item">
            <MovieButton to={id} title={title}/>
        </div>
    )
}

export default MovieItem