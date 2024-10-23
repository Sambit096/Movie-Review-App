import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import PageButton from "./PageButton";
import Home from "../pages/Home";
import Cart from "../pages/Cart"
import Movies from "../pages/Movies";
import Checkout from "../pages/Checkout";

const Navigation = () => {
    return (
        <Router>
            <PageButton to=""/>
            <PageButton to="Movies" />
            <PageButton to="Cart" />
            <PageButton to="Checkout" />
            <Routes>
                <Route path="/" element={<Home />}></Route>
                <Route path="/Cart" element={<Cart />}></Route>
                <Route path="/Movies" element={<Movies />}></Route>
                <Route path="/Checkout" element={<Checkout />}></Route>
            </Routes>
        </Router>
    )
}

export default Navigation