import MovieButton from "./MovieButton"
import ReviewButton from "./ReviewButton"

const MovieItem = ({ title, id, desc, genre }) => {


    return(
        <div className="movie--item">
            <div className="movie--details">
                <h2>{title}</h2>
                <h3>{desc}</h3>
                <h3>{genre}</h3>
            </div>
            <div className="movie--buttons">
                <MovieButton to={id} title={title}/>
                <ReviewButton to={id} title={title}/>
            </div>
        </div>
    )
}

export default MovieItem