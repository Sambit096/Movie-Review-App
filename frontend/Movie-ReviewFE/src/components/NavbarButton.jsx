import { useNavigate } from "react-router-dom"

const NavbarButton = ({ to, label }) => {

    const navigate = useNavigate();

    return (
        <button onClick={() => {navigate(`/${to}`)}} className="page--button">
            <p className="nav--item">{to === "" ? "Home" : label}</p>
        </button>
    )
}

export default NavbarButton