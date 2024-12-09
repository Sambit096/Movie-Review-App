import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import NavbarButton from "./NavbarButton";

const Navigation = () => {
    return (
        <div>
            <nav className="navbar">
                <div id="nav--left">
                    <h3>Movie Review App</h3>
                </div>
                <div id="nav--right">
                    <NavbarButton to="" label=""/>
                    <NavbarButton to="Movies" label="Movies" />
                    <NavbarButton to="Cart" label="Cart" />
                    <NavbarButton to="Settings" label="Account" />
                    <NavbarButton to="Management" label="Management"/>
                </div>
            </nav>
        </div>
    )
}

export default Navigation