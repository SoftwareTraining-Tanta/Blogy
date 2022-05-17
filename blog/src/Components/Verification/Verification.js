import React, { useEffect, useState } from 'react'

function Verification() {
    // Statues
    const username = sessionStorage.getItem('username')
    const [verifyCode, setVerifyCode] = useState()
    const plan = sessionStorage.getItem('plan')
    const [msgResponse, setMsgResponse] = useState()
    const [pending, setPending] = useState(false)


    // Handle Sumbit Button
    const handleSubmit = (x) => {
        x.preventDefault()
        setPending(true)
        fetch(`https://localhost:5000/api/users/verify/${username}/${verifyCode}/${plan}`, {
            method: "POST",
            headers: { 'Content-Type': 'application/json' }
        }).then(response => response.text())
            .then(json => setMsgResponse(json));
    }
    console.log(msgResponse)

    // Show Messages Error IF There IS
    useEffect(() => {
        if (msgResponse == 'User verified successfully') {
            window.location.href = '/'
        } else if (msgResponse == 'Verification code is not correct') {
            alert("Verification Code isn't correct")
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
                    <h2 className='text-center mb-3'>Verification Page</h2>
                    <form onSubmit={handleSubmit} className='w-50 mx-auto border border-2 border-primary rounded p-3 mb-5'>
                        <div class="mb-3">
                            <label for="verifyCode" class="form-label">Verification Code</label>
                            <input type="text" id="verifyCode" class="form-control" onChange={(x) => { setVerifyCode(x.target.value) }} />
                        </div>
                        <input type="submit" value='Submit' class="btn btn-primary" />
                    </form>
                </>}
        </>
    )
}

export default Verification