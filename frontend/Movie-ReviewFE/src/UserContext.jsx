import { createContext, useState, useContext, useEffect } from 'react';

const UserContext = createContext(null);

export const UserProvider = ({children}) => {
    const [user, setUser] = useState(null);

    // loading the user from session storage
    const userData = JSON.parse(localStorage.getItem('user'))

    const fetchUserData = async () => {
        try {
            const res = await fetch(`http://localhost:5190/api/User/GetUserByEmail?email=${userData.email}`)
            if(res.ok) {
                const data = await res.json()
                console.log(data)
                setUser(data)
            }
        } catch (err) {
            console.log(err.message)
        }
    }

    useEffect(() => {
        login()
    }, [])

    const login = () => {
        fetchUserData()
    };

    const logout = () => {
        setUser(null);
    };

    return (
        <UserContext.Provider value={{ user, login, logout }}>
            {children}
        </UserContext.Provider>
    );
};

export const useUser = () => {
    return useContext(UserContext);
};
