import { useNavigate } from "react-router-dom";

const LoginButton = ({to}) => {

    const navigate = useNavigate()

    const buttonText = () => {
        if(to === "Login") return to
        else {
            to = "Movies"
            return "Continue As Guest"
        }
    }

    return (
        <button onClick={() => {navigate(`/${to}`)}} className="login--button">
            <p>{buttonText()}</p>
        </button>
    )
}

export default LoginButton