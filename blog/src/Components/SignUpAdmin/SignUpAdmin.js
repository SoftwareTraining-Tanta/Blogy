import React, { useEffect, useState } from 'react'
import { NavLink } from 'react-router-dom'

function SignUpAdmin() {

    // Statues 
    const [name, setName] = useState()
    const [userName, setUserName] = useState()
    const [email, setEmail] = useState()
    const [phone, setPhone] = useState()
    const [password, setPassword] = useState()
    const [base64String, setBase64String] = useState()
    const plan = sessionStorage.getItem('Plan')
    const [msgError, setMsgError] = useState('')
    const [msgResponse, setMsgResponse] = useState('')
    const [pending, setPending] = useState(false)


    // Convert Image to Base64
    function convertImageToBase64(x) {
        var file = x.target.files[0]
        var reader = new FileReader();
        console.log("next");
        reader.onload = function () {
            setBase64String(reader.result.replace("data:", "").replace(/^.+,/, ""));
        }
        reader.readAsDataURL(file);
    }

    // Handle Sumbit Button
    const handleSubmit = (x) => {
        x.preventDefault()
        setPending(true)
        fetch("https://localhost:5000/api/admins/register", {
            method: "POST",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ username: userName, name: name, email: email, phone: phone, password: password, profilePicture: base64String })
        })
            .then(response => response.text())
            .then(msg => setMsgResponse(msg))
    }

    // Show Messages Error IF There IS
    useEffect(() => {
        if (msgResponse == 'Done') {
            sessionStorage.setItem('admin', userName)
            sessionStorage.setItem('isadmin', true)
            sessionStorage.setItem('username', null)
            sessionStorage.setItem('isuser', false)
            window.location.href = '/adminhome'
        } else if (msgResponse != '') {
            setMsgError(msgResponse)
            setPending(false)
            setMsgResponse('')
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
                    <h2 className='text-center mb-3'>Sign up</h2>
                    <h2 className='text-center mb-3'>{msgError}</h2>

                    <form onSubmit={handleSubmit} className='w-50 mx-auto border border-2 border-primary rounded p-3 mb-5'>

                        <div class="mb-3">
                            <label for="fullName" class="form-label">Full name</label>
                            <input type="text" class="form-control" id="fullName" required onChange={(x) => { setName(x.target.value) }} />
                        </div>

                        <div class="mb-3">
                            <label for="userName" class="form-label">User name</label>
                            <input type="text" class="form-control" id="userName" required onChange={(x) => { setUserName(x.target.value) }} />
                        </div>

                        <div class="mb-3">
                            <label for="email" class="form-label">Email</label>
                            <input type="email" class="form-control" id="email" required aria-describedby="emailHelp" onChange={(x) => { setEmail(x.target.value) }} />
                        </div>

                        <div class="mb-3">
                            <label for="phone" class="form-label">Phone</label>
                            <input type="number" class="form-control" id="phone" required onChange={(x) => { setPhone(x.target.value) }} />
                        </div>

                        <div class="mb-3">
                            <label for="password" class="form-label">Password</label>
                            <input type="password" class="form-control" id="password" required onChange={(x) => { setPassword(x.target.value) }} />
                        </div>

                        <div class="mb-3">
                            <label for="picture" class="form-label">Choose a picture:</label>
                            <input type="file" class="form-control" id="picture" required onChange={convertImageToBase64} />
                        </div>

                        <div className='d-flex justify-content-between'>
                            <NavLink className="text-decoration-none fw-bold" to='/signinadmin'>Sign in</NavLink>
                            <input type="submit" value='Submit' class="btn btn-primary" />
                        </div>

                    </form>
                </>}

        </>
    )
}

export default SignUpAdmin