import { Routes, Route, Navigate } from "react-router-dom";
import Home from "../pages/Home";
import Cart from "../pages/Cart"
import Movies from "../pages/Movies";
import Login from "../pages/Login";
import Settings from "../pages/Settings";
import Showtimes from "../pages/Showtimes";
import Tickets from "../pages/Tickets";
import Reviews from "../pages/Reviews";
import Management from "../pages/Management";import MyReviews from "../pages/MyReviews";

const UseRoutes = () => {
    return(
        <Routes>
            <Route path="/" element={<Home />}></Route>
            <Route path="/Cart" element={<Cart />}></Route>
            <Route path="/Movies" element={<Movies />}></Route>
            <Route path="/Login" element={<Login />}></Route>
            <Route path="/Settings" element={<Settings />}></Route>
            <Route path='/Movies/:movieId' element={<Showtimes />}></Route>
            <Route path='/Reviews/:movieId' element={<Reviews />}></Route>
            <Route path='/Movies/:movieId/Tickets/:showtimeID' element={<Tickets />}></Route>
            <Route path="/MyReviews" element={<MyReviews />}></Route>
            <Route path="/Management" element={<Management />}></Route>
      
        </Routes>
    )
}

export default UseRoutes