import { useState } from "react"
import { useNavigate } from 'react-router-dom'
import { useUser } from "../UserContext";

const Login = () => {

    const [isLogin, setIsLogin] = useState(true);
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [username, setUsername] = useState('')
    const [error, setError] = useState('')
    const [success, setSuccess] = useState('')

    const navigate = useNavigate()

    const handleLogin = async (e) => {
        e.preventDefault()
        setError('')
        setSuccess('')
        

        // Call our login API
        try {
            const res = await fetch('http://localhost:5190/api/User/login', {
                method: 'POST',
                headers: {'Content-Type': 'application/json'},
                body: JSON.stringify({email, password}),
            });
            if(!res.ok) throw new Error('Login Failed')
            const data = await res.json()
            setSuccess(data.message)
            localStorage.setItem('user', JSON.stringify({email}))
            navigate('/Movies')
        } catch (err) {
            setError(err.message);
        }
    }

    const handleSignup = async (e) => {
        e.preventDefault()
        setError('')
        setSuccess('')

        // Call our signup API
        try {
            const res = await fetch('http://localhost:5190/api/User/signup', {
                method: 'POST',
                headers: {'Content-Type': 'application/json'},
                body: JSON.stringify({email, username, password}),
            });
            if(!res.ok) throw new Error('Signup Failed')
            const data = await res.json()
            setSuccess(data.message)
            setIsLogin(true)
        } catch (err) {
            setError(err.message);
        }
    }

    return(
        <div>
            <h2>{isLogin ? 'Login' : 'Create an Account'}</h2>
            {error && <div style={{color: 'red'}}>{error}</div>}
            {success && <div style={{color: 'green'}}>{success}</div>}
            <form onSubmit={isLogin ? handleLogin : handleSignup}>
                {!isLogin && (
                    <div style={{marginBottom: '15px'}}>
                        <label htmlFor="username">Username:</label>
                        <input 
                            type="text"
                            id="username"
                            value={username}
                            onChange={(e) => {setUsername(e.target.value)
                                console.log(username)
                            }}
                            required
                            style={{width: '100%', padding: '8px'}}
                        />
                    </div>
                )}
                <div style={{marginBottom: '15px'}}>
                    <label htmlFor="email">Email:</label>
                    <input 
                        type="text"
                        id="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                        style={{width: '100%', padding: '8px'}}
                    />
                </div>
                <div style={{marginBottom: '15px'}}>
                    <label htmlFor="password">Password:</label>
                    <input 
                        type="text"
                        id="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                        style={{width: '100%', padding: '8px'}}
                    />
                </div>
                <button type="submit">
                    {isLogin ? 'Login' : 'Sign Up'}
                </button>
            </form>
            <p>
                {isLogin ? 'Don\'t have an account?' : 'Already have an account?'}
                <button onClick={
                    () => setIsLogin(!isLogin)}>
                    {isLogin ? 'Create one' : 'Login'}
                </button>
            </p>
        </div>
    )
}

export default Login