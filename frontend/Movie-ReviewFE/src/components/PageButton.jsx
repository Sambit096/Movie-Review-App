import { useNavigate } from "react-router-dom"

const PageButton = ({ to }) => {

    const navigate = useNavigate();

    return (
        <button onClick={() => {navigate(`/${to}`)}}>
            {to === "" ? "Home" : to}
        </button>
    )
}

export default PageButton