import React from 'react'
import { NavLink } from 'react-router-dom';
import {AiOutlineHome, AiFillPushpin, AiOutlineContacts} from 'react-icons/ai'
import {CgProfile} from 'react-icons/cg'

function NavBar() {
    const username = sessionStorage.getItem('username')
    const admin = sessionStorage.getItem('admin')
    const isadmin = sessionStorage.getItem('isadmin')
    return (
        <>
            <nav className="navbar navbar-expand-lg navbar-light bg-primary bg-gradient mb-5">
                <div className="container">
                    {/* Logo */}
                    <NavLink className="navbar-brand text-light fs-5" to="/">Blogy</NavLink>

                    {/* Button For Responsive */}
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>

                    {/* List of Links */}
                    <div className="collapse navbar-collapse" id="navbarSupportedContent">

                        <ul className="navbar-nav ms-auto mb-2 mb-lg-0">

                            <li className="nav-item d-flex align-items-center me-2">
                                <AiOutlineHome className='text-light fs-5'/>
                                <NavLink className="nav-link active text-light fs-5" aria-current="page" to="/">Home</NavLink>
                            </li>

                            <li className="nav-item d-flex align-items-center me-2">
                                <CgProfile className='text-light fs-5'/>
                                <NavLink className="nav-link text-light fs-5" to={`/profilepage/${ isadmin == 'true' ? admin : username}`}>Profile</NavLink>
                            </li>

                            <li className="nav-item d-flex align-items-center me-2">
                                <AiFillPushpin className='text-light fs-5'/>
                                <NavLink className="nav-link text-light fs-5" to='/pinposts'>Pin Posts</NavLink>
                            </li>

                            <li className="nav-item d-flex align-items-center">
                                <AiOutlineContacts className='text-light fs-5'/>
                                <NavLink className="nav-link text-light fs-5" to='/contact'>Contact</NavLink>
                            </li>

                            <li class="nav-item dropdown d-flex align-self-center">
                                <a class="nav-link dropdown-toggle text-light" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false"></a>
                                <ul class="dropdown-menu" style={{textAlign: 'center', top:'51px', left: '-110px'}} aria-labelledby="navbarDropdown">
                                    <li><NavLink className="dropdown-item text-decoration-none" to={isadmin == 'true' ? `/adminhome` : `/signinadmin`}>Admin</NavLink></li>
                                    <hr />
                                    <li><NavLink className="dropdown-item text-decoration-none" to="/signin">Login</NavLink></li>
                                    <hr />
                                    <li><NavLink className="dropdown-item text-decoration-none" to="/signup">Register</NavLink></li>
                                </ul>
                            </li>
                        </ul>

                    </div>

                </div>
            </nav>
        </>
    )
}

export default NavBar