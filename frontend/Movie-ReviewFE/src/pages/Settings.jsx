import { useState, useEffect } from "react"
import { useUser } from "../UserContext"

const Settings = () => {

    const { user, updateUser } = useUser();
    const [formData, setFormData] = useState({});

    useEffect(() => {
        if(user) {
            setFormData({
                username: user.username,
                fisrtname: user.firstname,
                lastname: user.lastname
            });
        }
    }, [user]);

    const handleChange = async () => {
        setFormData({...formData, [e.target.name]: e.target.value});
    };

    const handleSubmit = async (e) => {
        e.preventDefault()
        
        // API Call to update user info
        const res = await fetch('http://localhost:5190/api/User/UpdateUser', {
            method: 'PUT',
            headers: {'Content-Type': 'application/json',},
            body: JSON.stringify(formData)
        });
        if(res.ok) {
            const updatedUser = await res.json()
            updateUser(updatedUser) // update user context
        }
    }

    return (
        <div>
            <form onSubmit={handleSubmit}>
                <input
                    name="username"
                    value={formData.username || ''}
                    onChange={handleChange}
                    placeholder="Username"
                />
                <input
                    name="firstname"
                    value={formData.firstname || ''}
                    onChange={handleChange}
                    placeholder="First Name"
                />
                <input
                    name="lastname"
                    value={formData.lastname || ''}
                    onChange={handleChange}
                    placeholder="Last Name"
                />
            <button type="submit">Update</button>
            </form>
        </div>
    );
}

export default Settings