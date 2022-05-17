import React, { useState } from 'react'

function Contact() {
    const[userName, setNameUser] = useState()
    const[msg, setMsg] = useState()

    const handleSubmit = (x) => {
        x.preventDefault()
        fetch("https://localhost:5000/api/posts", {
            method: "POST",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ title: titlePost, content: contentPost, dateTime: String(new Date()).split('GMT')[0], username: username, image: base64String, adminUsername: null, isAdmin: false })
        }).
            then(response => response.text()).
            then(json => setMsgResponse(json));
    }

    return (
        <>
            <div className='container w-50'>
                <form onSubmit={handleSubmit}>

                    <div class="mb-3">
                        <label for="username" class="form-label">Username</label>
                        <input type="text" class="form-control" id="username" onChange={(x)=>{setNameUser(x.target.value)}} />
                    </div>

                    <div class="mb-3">
                        <label for="msg" class="form-label">Message</label>
                        <input type="text" class="form-control" id="msg" onChange={(x)=>{setMsg(x.target.value)}} />
                    </div>

                    <input type="submit" value='Submit' class="btn btn-primary" />
                </form>

            </div>
        </>
    )
}

export default Contact