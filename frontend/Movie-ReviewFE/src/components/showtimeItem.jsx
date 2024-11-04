import showtimeButton from "./showtimeButton"

const showtimeItem = ({ time, id }) => {


    return(
        <div className="movie--item">
            <h2>{title}</h2>
            <showtimeButton to={id} title={title}/>
        </div>
    )
}

export default showtimeItem