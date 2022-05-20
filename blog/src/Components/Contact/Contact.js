import React, { useEffect, useState } from 'react'

function Contact() {
    const [userName, setNameUser] = useState()
    const [subject, setSubject] = useState()
    const [msg, setMsg] = useState()
    const [msgResponse, setMsgResponse] = useState()
    const [pending, setPending] = useState(false)
    const isadmin = sessionStorage.getItem('isadmin')
    const isuser = sessionStorage.getItem('isuser')
    const username = sessionStorage.getItem('username')

    const handleSubmit = (x) => {
        x.preventDefault()
        if (isadmin == 'true' || isuser == 'true') {
            setPending(true)
            if (isadmin == 'true') {
                fetch(`https://localhost:5000/api/admins/sendtouser?username=${userName}&subject=${subject}&message=${msgResponse}`, {
                    method: "POST",
                    headers: { 'Content-Type': 'application/json' },
                }).then(response => response.text())
                    .then(json => setMsgResponse(json))
            } else {
                fetch(`https://localhost:5000/api/users/sendemailtoadmin/${userName}/${msg}`, {
                    method: "POST",
                    headers: { 'Content-Type': 'application/json' },
                }).then(response => response.text())
                    .then(json => setMsgResponse(json))
            }
        } else {
            alert('Sign in firstly')
        }

    }

    useEffect(() => {
        if (msgResponse == 'Email sent successfully') {
            window.location.href = '/contact'
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
                <div className='container w-50'>
                    <h2>Contact</h2>
                    <hr />
                    <form onSubmit={handleSubmit}>

                        <div class="mb-3">
                            <label for="username" class="form-label">Username</label>
                            <input type="text" value={isuser == 'true' ? username : null} class="form-control" id="username" onChange={(x) => { setNameUser(x.target.value) }} />
                        </div>

                        {isadmin == 'true' ?
                            <div class="mb-3">
                                <label for="subject" class="form-label">Subject</label>
                                <input type="text" class="form-control" id="subject" onChange={(x) => { setSubject(x.target.value) }} />
                            </div> : null}

                        <div class="mb-3">
                            <label for="msg" class="form-label">Message</label>
                            <input type="text" class="form-control" id="msg" onChange={(x) => { setMsg(x.target.value) }} />
                        </div>

                        <input type="submit" value='Submit' class="btn btn-primary" />
                    </form>
                </div>}
        </>
    )
}

export default Contact