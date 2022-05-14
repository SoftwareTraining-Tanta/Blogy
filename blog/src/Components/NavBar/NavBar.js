import React from 'react'
import { NavLink } from 'react-router-dom';

function NavBar() {
    return (
        <>
            <nav className="navbar navbar-expand-lg navbar-light bg-primary bg-gradient mb-5">
                <div className="container">
                    {/* Logo */}
                    <NavLink className="navbar-brand text-light fs-4" to="/">Blogy</NavLink>
                    
                    {/* Button For Responsive */}
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    
                    {/* List of Links */}
                    <div className="collapse navbar-collapse" id="navbarSupportedContent">

                        <ul className="navbar-nav ms-auto mb-2 mb-lg-0">

                            <li className="nav-item">
                                <NavLink className="nav-link active text-light fs-4" aria-current="page" to="/">Home</NavLink>
                            </li>

                            <li className="nav-item">
                                <NavLink className="nav-link text-light fs-4" to="/signin">Login</NavLink>
                            </li>

                            <li className="nav-item">
                                <NavLink className="nav-link text-light fs-4" to="/signup">Register</NavLink>
                            </li>
                        </ul>

                    </div>

                </div>
            </nav>
        </>
    )
}

export default NavBar