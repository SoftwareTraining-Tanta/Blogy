import React, { useEffect, useState } from 'react'
import { NavLink } from 'react-router-dom';

function SignIn() {
    // Statues
    const [userName, setUserName] = useState()
    const [password, setPassword] = useState()
    const [msgResponse, setMsgResponse] = useState('')

    // Handle Sumbit Button
    const handleSubmit = (x) => {
        x.preventDefault()
        fetch(`https://localhost:5000/api/users/login/${userName}/${password}`, {
            method: "POST",
            headers: { 'Content-Type': 'application/json' }
        }).then(response => response.text())
          .then(json => setMsgResponse(json));
    }

    // Show Messages Error IF There IS
    useEffect(()=>{
        if (msgResponse == 'user') {
            sessionStorage.setItem('username',userName)
            sessionStorage.setItem('isuser',true)
            sessionStorage.setItem('admin',null)
            sessionStorage.setItem('isadmin',null)
            window.location.href = '/'
        } else if(msgResponse != '') {
            alert(msgResponse)
        }
    },[msgResponse])

    return (
        <>
            <h2 className='text-center mb-3'>Sign in</h2>
            <form onSubmit={handleSubmit} className='w-50 mx-auto border border-2 border-primary rounded p-3 mb-5'>
                <div class="mb-3">
                    <label for="userName" class="form-label">User name</label>
                    <input type="text" class="form-control" id="userName"  onChange={(x)=>{setUserName(x.target.value)}}/>
                </div>
                <div class="mb-3">
                    <label for="password" class="form-label">Password</label>
                    <input type="password" class="form-control" id="password" onChange={(x)=>{setPassword(x.target.value)}}/>
                </div>
                <div className='d-flex justify-content-between fw-bold'>
                    <NavLink className="text-decoration-none" to='/signup'>Create account</NavLink>
                    <input type="submit" value='Submit' class="btn btn-primary" />
                </div>
            </form>
        </>
    )
}

export default SignIn