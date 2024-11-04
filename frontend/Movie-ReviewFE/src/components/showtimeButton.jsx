import { useNavigate } from "react-router-dom"

const ShowtimeButton = ({to, time}) => {

    const navigate = useNavigate();

    return(
        <button className="buytickets--button" onClick={() => {navigate(`/Tickets/${to}`)}}>
            <p>Buy Tickets</p>
        </button>
    )
}

export default ShowtimeButton