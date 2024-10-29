import { Routes, Route } from "react-router-dom";
import Home from "../pages/Home";
import Cart from "../pages/Cart"
import Movies from "../pages/Movies";
import Checkout from "../pages/Checkout";
import Login from "../pages/Login";
import Settings from "../pages/Settings";
import Movie from "./Movie";

const UseRoutes = () => {
    return(
        <Routes>
            <Route path="/" element={<Home />}></Route>
            <Route path="/Cart" element={<Cart />}></Route>
            <Route path="/Movies" element={<Movies />}></Route>
            <Route path="/Checkout" element={<Checkout />}></Route>
            <Route path="/Login" element={<Login />}></Route>
            <Route path="/Settings" element={<Settings />}></Route>
            <Route path="/Movie/:id" element={<Movie />}></Route>
        </Routes>
    )
}

export default UseRoutes