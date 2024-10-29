import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import PageButton from "./PageButton";
import Home from "../pages/Home";
import Cart from "../pages/Cart"
import Movies from "../pages/Movies";
import Checkout from "../pages/Checkout";

const Navigation = () => {
    return (
        <Router>
            <nav className="navbar">
                <div id="nav--left">
                    <h3>App Title</h3>
                </div>
                <div id="nav--right">
                    <PageButton to=""/>
                    <PageButton to="Movies" />
                    <PageButton to="Cart" />
                    <PageButton to="Checkout" />
                </div>
            </nav>
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