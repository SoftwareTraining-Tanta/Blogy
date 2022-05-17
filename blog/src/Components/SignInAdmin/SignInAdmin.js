import React, { useEffect, useState } from 'react'
import { NavLink } from 'react-router-dom';

function SignInAdmin() {
    // Statues
    const [userName, setUserName] = useState()
    const [password, setPassword] = useState()
    const [msgResponse, setMsgResponse] = useState('')
    const [pending, setPending] = useState(false)


    // Handle Sumbit Button
    const handleSubmit = (x) => {
        x.preventDefault()
        setPending(true)
        fetch(`https://localhost:5000/api/admins/login?username=${userName}&password=${password}`, {
            method: "GET",
            headers: { 'Content-Type': 'application/json' }
        }).then(response => response.text())
            .then(json => setMsgResponse(json));
    }

    // Show Messages Error IF There IS
    useEffect(() => {
        if (msgResponse == 'admin') {
            sessionStorage.setItem('admin', userName)
            sessionStorage.setItem('isadmin', true)
            sessionStorage.setItem('username', null)
            sessionStorage.setItem('isuser', false)
            window.location.href = '/adminhome'
        } else if (msgResponse != '') {
            alert(msgResponse)
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
                    <h2 className='text-center mb-3'>Sign in</h2>
                    <form onSubmit={handleSubmit} className='w-50 mx-auto border border-2 border-primary rounded p-3 mb-5'>
                        <div class="mb-3">
                            <label for="userName" class="form-label">User Name</label>
                            <input type="text" class="form-control" id="userName" onChange={(x) => { setUserName(x.target.value) }} />
                        </div>
                        <div class="mb-3">
                            <label for="password" class="form-label">Password</label>
                            <input type="password" class="form-control" id="password" onChange={(x) => { setPassword(x.target.value) }} />
                        </div>
                        <div className='d-flex justify-content-between fw-bold'>
                            <NavLink className="text-decoration-none" to='/signupadmin'>Create account</NavLink>
                            <input type="submit" value='Submit' class="btn btn-primary" />
                        </div>
                    </form>
                </>}
        </>
    )
}

export default SignInAdmin