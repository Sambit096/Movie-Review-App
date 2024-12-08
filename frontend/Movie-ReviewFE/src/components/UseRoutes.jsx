import { Routes, Route, Navigate } from "react-router-dom";
import { useState, useEffect } from "react";
import Home from "../pages/Home";
import Cart from "../pages/Cart"
import Movies from "../pages/Movies";
import Checkout from "../pages/Checkout";
import Login from "../pages/Login";
import Settings from "../pages/Settings";
import Showtimes from "../pages/Showtimes";
import Tickets from "../pages/Tickets";
import Reviews from "../pages/Reviews";
import Management from "../pages/Management";import MyReviews from "../pages/MyReviews";

const ProtectedRoute = ({ isAllowed, redirectPath = "/", children }) => {
    if (!isAllowed) {
      return <Navigate to={redirectPath} />;
    }
    return children;
  };

  // const user = JSON.parse(localStorage.getItem('user')) || {};
  // const isAdmin = user.userType === 'Admin';

const UseRoutes = () => {
    const [user, setUser] = useState(() => JSON.parse(localStorage.getItem("user")) || {});

    const refreshUser = () => {
      const updatedUser = JSON.parse(localStorage.getItem("user")) || {};
      setUser(updatedUser);
  };

  const isAdmin = user.userType === "Admin";
    return(
        <Routes>
            <Route path="/" element={<Home />}></Route>
            <Route path="/Cart" element={<Cart />}></Route>
            <Route path="/Movies" element={<Movies />}></Route>
            <Route path="/Checkout" element={<Checkout />}></Route>
            <Route path="/Login" element={<Login />}></Route>
            <Route path="/Settings" element={<Settings />}></Route>
            <Route path='/Movies/:movieId' element={<Showtimes />}></Route>
            <Route path='/Reviews/:movieId' element={<Reviews />}></Route>
            <Route path='/Movies/:movieId/Tickets/:showtimeID' element={<Tickets />}></Route>
            <Route path="/MyReviews" element={<MyReviews />}></Route>
            <Route path="/Management" element={<ProtectedRoute isAllowed={isAdmin} refreshUser={refreshUser}><Management /></ProtectedRoute>}
      />
        </Routes>
    )
}

export default UseRoutes