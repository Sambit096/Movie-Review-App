import { useNavigate } from "react-router-dom"

const MovieButton = ({to, title}) => {

    const navigate = useNavigate();

    return(
        <button onClick={() => {navigate(`/Movies/${to}`)}}>
            <p>{title}</p>
        </button>
    )
}

export default MovieButton