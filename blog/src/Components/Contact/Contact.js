import React, { useEffect, useState } from 'react'

function Contact() {
    const [userName, setNameUser] = useState()
    const [subject, setSubject] = useState()
    const [msg, setMsg] = useState()
    const [msgResponse, setMsgResponse] = useState()
    const isadmin = sessionStorage.getItem('isadmin')

    const handleSubmit = (x) => {
        x.preventDefault()
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
    }

    useEffect(() => {
        if (msgResponse == 'Email sent successfully') {
            window.location.href = '/contact'
        }
    }, [msgResponse])

    return (
        <>
            <div className='container w-50'>
                <h2>Contact</h2>
                <hr />
                <form onSubmit={handleSubmit}>

                    <div class="mb-3">
                        <label for="username" class="form-label">Username</label>
                        <input type="text" class="form-control" id="username" onChange={(x) => { setNameUser(x.target.value) }} />
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

            </div>
        </>
    )
}

export default Contact