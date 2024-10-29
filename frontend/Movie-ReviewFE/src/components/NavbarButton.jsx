import { useNavigate } from "react-router-dom"

const NavbarButton = ({ to }) => {

    const navigate = useNavigate();

    return (
        <button onClick={() => {navigate(`/${to}`)}} className="page--button">
            <p className="nav--item">{to === "" ? "Home" : to}</p>
        </button>
    )
}

export default NavbarButton