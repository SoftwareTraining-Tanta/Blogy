import React, { useEffect, useState } from 'react'
import { AiOutlineDashboard, AiOutlineMessage } from 'react-icons/ai';
import { FcStatistics } from 'react-icons/fc';
import { FiUsers } from 'react-icons/fi';
import { BiLogOut } from 'react-icons/bi';
import { NavLink } from 'react-router-dom'
import './admin.css'

function Dashboard() {
    const [msgResponse, setMsgResponse] = useState()

    const logOut = () =>{
        fetch(`https://localhost:5000/api/admins/logout`, {
            method: "GET",
            headers: { 'Content-Type': 'application/json' }
        }).then(response => response.text())
          .then(json => setMsgResponse(json));
    }

    useEffect(()=>{
        if (msgResponse == 'SignedOut') {
            alert('SignedOut')
            sessionStorage.setItem('admin', '')
            sessionStorage.setItem('isadmin', 'false')
            window.location.href = '/'
        }
    },[msgResponse])

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
                        <button onClick={logOut} className='btn text-dark'>
                            <BiLogOut className='fs-4' />
                            <span class="title ">Log out</span>
                        </button>
                    </li>
                </ul>
            </div>
        </>
    )
}

export default Dashboard