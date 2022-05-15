import React from 'react'
import { AiOutlineDashboard, AiOutlineMessage } from 'react-icons/ai';
import { FcStatistics } from 'react-icons/fc';
import { FiUsers } from 'react-icons/fi';
import { BiLogOut } from 'react-icons/bi';
import { NavLink } from 'react-router-dom'
import './admin.css'

function Dashboard() {
    return (
        <>
            <div class="navigation">
                <div className='text-center d-flex justify-content-center align-items-center'>
                    <AiOutlineDashboard className='text-light fs-5 fw-bold me-2' />
                    <span className="title text-light fs-5 fw-bold">Dashboard</span>
                </div>
                <hr />
                <ul>
                    <li>
                        <NavLink to="/adminhome">
                            <FcStatistics className='fs-4' />
                            <span class="title">Blog Statistics</span>
                        </NavLink>
                    </li>

                    <li>
                        <NavLink to="/adminusers">
                            <FiUsers className='fs-4' />
                            <span class="title">All users</span>
                        </NavLink>

                    </li>

                    <li>
                        <NavLink to="/">
                            <AiOutlineMessage className='fs-4' />
                            <span class="title">Messages</span>
                        </NavLink>
                    </li>

                    <li>
                        <NavLink to="/">
                            <BiLogOut className='fs-4' />
                            <span class="title">Log out</span>
                        </NavLink>
                    </li>
                </ul>
            </div>
        </>
    )
}

export default Dashboard