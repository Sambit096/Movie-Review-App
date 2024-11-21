import { useNavigate } from "react-router-dom"
import Reviews from "../pages/Reviews";

const ReviewButton = ({to, title}) => {

    const navigate = useNavigate();

    return(
        <button className="buytickets--button" onClick={() => {
            navigate(`/Reviews/${to}`, { state: { title } }); }}>
            <p>See Reviews</p>
        </button>
    )
}

export default ReviewButton