import { useNavigate } from "react-router-dom"
import Showtimes from "../pages/Showtimes";

const MovieButton = ({to, title}) => {

    const navigate = useNavigate();

    return(
        <button className="buytickets--button" onClick={() => {navigate(`/Movies/${to}`)}}>
            <p>See Showtimes</p>
        </button>
    )
}

export default MovieButton