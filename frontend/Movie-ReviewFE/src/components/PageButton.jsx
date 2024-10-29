import { useNavigate } from "react-router-dom"

const PageButton = ({ to }) => {

    const navigate = useNavigate();

    return (
        <button onClick={() => {navigate(`/${to}`)}} className="page--button">
            <p className="nav--item">{to === "" ? "Home" : to }</p>
        </button>
    )
}

export default PageButton