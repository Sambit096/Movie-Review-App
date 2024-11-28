import { useState, useEffect } from "react"

const Settings = () => {

    const [user, setUser] = useState({
        email: "user@example.com",
        username: "username",
        firstName: "First",
        lastName: "Last",
        gender: "None",
        ageGroup: "YoungAdult",
        password: "",
        notiPreference: "None",
        userType: "User",
      });
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    const genderOptions = ["None", "F", "M", "Other"];
    const ageGroupOptions = ["Teen", "YoungAdult", "Adult", "Retired"];
    const notiPreferenceOptions = ["SMS", "Email", "Both", "None"];
    const userTypeOptions = ["User", "Admin"];  

    useEffect(() => {
        const fetchUserData = async () => {
          try {
            const user = localStorage.getItem("user");
            if (!user) {
              throw new Error("User not found in local storage.");
            }
            const parseUser = JSON.parse(user);
            const userId = parseUser?.userId;
            const response = await fetch(`http://localhost:5190/api/User/GetUserById?id=${userId}`);
            if (!response.ok) {
              throw new Error("Failed to fetch user data.");
            }
    
            const userData = await response.json();
            setUser(userData);
          } catch (err) {
            setError(err.message);
          } finally {
            setLoading(false);
          }
        };
    
        fetchUserData();
      }, []);
      

      if (loading) {
        return <div>Loading user data...</div>;
      }
    
      if (error) {
        return <div>Error: {error}</div>;
      }

    // const handleChange = async () => {
    //     setFormData({...formData, [e.target.name]: e.target.value});
    // };

    // const handleSubmit = async (e) => {
    //     e.preventDefault()
        
    //     // API Call to update user info
    //     const res = await fetch('http://localhost:5190/api/User/UpdateUser', {
    //         method: 'PUT',
    //         headers: {'Content-Type': 'application/json',},
    //         body: JSON.stringify(formData)
    //     });
    //     if(res.ok) {
    //         const updatedUser = await res.json()
    //         updateUser(updatedUser) // update user context
    //     }
    // }
    const handleChange = (e) => {
        const { name, value } = e.target;
        setUser({ ...user, [name]: value });
      };
    
      // Handle form submission
      const handleSubmit = async (e) => {
        e.preventDefault();
        console.log("Updated User Info:", user);
        const res = await fetch('http://localhost:5190/api/User/UpdateUser', {
            method: 'PUT',
            headers: {'Content-Type': 'application/json',},
            body: JSON.stringify(user)
        });
        if(res.ok) {
            localStorage.removeItem('user');
            localStorage.setItem('user', JSON.stringify({email: user.email, username: user.username, userId: user.userId, firstName: user.firstName, lastName: user.lastName, userType: user.userType}))
        }
      };
    
      return (
        <div>
          <h2>User Settings</h2>
          <form onSubmit={handleSubmit}>
            <div>
              <label>Email:</label>
              <input
                type="email"
                name="email"
                value={user.email}
                onChange={handleChange}
                required
              />
            </div>
            <div>
              <label>Username:</label>
              <input
                type="text"
                name="username"
                value={user.username}
                onChange={handleChange}
                required
              />
            </div>
            <div>
              <label>First Name:</label>
              <input
                type="text"
                name="firstName"
                value={user.firstName}
                onChange={handleChange}
                required
              />
            </div>
            <div>
              <label>Last Name:</label>
              <input
                type="text"
                name="lastName"
                value={user.lastName}
                onChange={handleChange}
                required
              />
            </div>
            <div>
              <label>Gender:</label>
              <select
                name="gender"
                value={user.gender}
                onChange={handleChange}
              >
                {genderOptions.map((option) => (
                  <option key={option} value={option}>
                    {option}
                  </option>
                ))}
              </select>
            </div>
            <div>
              <label>Age Group:</label>
              <select
                name="ageGroup"
                value={user.ageGroup}
                onChange={handleChange}
              >
                {ageGroupOptions.map((option) => (
                  <option key={option} value={option}>
                    {option}
                  </option>
                ))}
              </select>
            </div>
            <div>
              <label>Password:</label>
              <input
                type="password"
                name="password"
                value={user.password}
                onChange={handleChange}
                required
              />
            </div>
            <div>
              <label>Notification Preference:</label>
              <select
                name="notiPreference"
                value={user.notiPreference}
                onChange={handleChange}
              >
                {notiPreferenceOptions.map((option) => (
                  <option key={option} value={option}>
                    {option}
                  </option>
                ))}
              </select>
            </div>
            <div>
              <label>User Type:</label>
              <select
                name="userType"
                value={user.userType}
                onChange={handleChange}
                disabled
              >
                {userTypeOptions.map((option) => (
                  <option key={option} value={option}>
                    {option}
                  </option>
                ))}
              </select>
            </div>
            <button type="submit">Save Changes</button>
          </form>
        </div>
      );
    };
    
    export default Settings;