import { useNavigate } from "react-router-dom"

const MovieButton = ({to, title}) => {

    const navigate = useNavigate();

    return(
        <button className="buytickets--button" onClick={() => {navigate(`/Movies/${to}`)}}>
            <p>Buy Tickets</p>
        </button>
    )
}

export default MovieButton