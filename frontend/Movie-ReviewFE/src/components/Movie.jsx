// Connect this to API Endpoint to fill with movie data

import PageButton from "./PageButton"

const Movie = ({title, desc}) => {


    return(
        <div className="movie--item">
            <div className="movie--item-container container column">
                <div className="movie--item-info">
                    <h2 className="movie--title">{title}</h2>
                    <p className="movie--desc">{desc}</p>
                </div>
            </div>
        </div>
    )
}

export default Movie