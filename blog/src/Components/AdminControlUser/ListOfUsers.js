import React, { useEffect, useState } from 'react'
import Dashboard from '../DashBoard/Dashboard'
import { NavLink } from 'react-router-dom'
import './admin.css'

function ListOfUsers() {

    const [data, setData] = useState([])
    const [msgResponse, setMsgResponse] = useState()
    const [pending, setPending] = useState(false)
    const isadmin = sessionStorage.getItem('isadmin')

    useEffect(()=>{
        if (isadmin != 'true') {
            window.location.href = '*'
        }
    },[])    

    useEffect(() => {
        fetch("https://localhost:5000/api/users/limit/2000")
            .then(response => response.json())
            .then(json => setData(json))
    }, [])

    const deleteUser = (username) => {
        setPending(true)
        fetch(`https://localhost:5000/api/users/Delete/${username}`, {
            method: "DELETE",
            headers: { 'Content-Type': 'application/json' },
        }).
            then(response => response.text()).
            then(json => setMsgResponse(json));
    }

    useEffect(() => {
        if (msgResponse == 'User deleted successfully') {
            window.location.href = '/adminusers'
        } else {
            setPending(false)
        }
    }, [msgResponse])

    // Loading Animation
    const loadingAnimation =
        <div style={{ position: 'absolute', left: '50%', top: '50%', transform: 'translate(-50%, -50%)' }}>
            <span style={{ fontSize: '25px', marginRight: '10px' }}>Loading</span>
            <div className="spinner-border" role="status"></div>
        </div>

    return (
        <>
            {pending ? loadingAnimation :
                <>
                    <div className='container-box'>
                        <Dashboard />
                        <div className='row'>
                            <h2 className='text-primary m-4'>All Users:</h2>
                        </div>
                        <div class="row mt-5 ">
                            {data.map((i) => {
                                return (
                                    <div class="card m-5" style={{ width: '20rem', height: 'max-content' }}>
                                        <img class="card-img-top" src={'data:image/png;base64,' + i.profilePicture} alt="Card image cap" />
                                        <div class="card-body">
                                            <h5 class="card-title">{i.username}</h5>
                                            <NavLink to="/contact" class="btn btn-primary me-2">Send E-mail</NavLink>
                                            <button onClick={() => deleteUser(i.username)} class="btn btn-danger">Remove user</button>
                                        </div>
                                    </div>
                                )
                            })}
                        </div>
                    </div>
                </>}

        </>
    )
}

export default ListOfUsers