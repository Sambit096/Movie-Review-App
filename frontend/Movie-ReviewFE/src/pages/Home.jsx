import { useState, useEffect } from 'react';
import LoginButton from '../components/LoginButton';

const Home = () => {
    const [userData, setUserData] = useState(null);

    useEffect(() => {
        const storedValue = localStorage.getItem("user");
    if (storedValue) {
      const parse = JSON.parse(storedValue);
      setUserData(parse);
    }
    }, []);
    
    function logOut() {
        localStorage.removeItem("user");
        setUserData(null);
    }

    return (
        <section className="page" id="home--page">
            <h1 className="page--header">Welcome to our Movie review site!</h1>
            {!userData && (<LoginButton to="Login"/>)}
            {userData && (<button onClick={logOut}>Log Out</button>)}
        </section>
    )
}

export default Home