import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'

function ProfilePage() {
    // Statues
    const [data, setData] = useState({})
    const [statusCode , setStatusCode] = useState()
    const isAdmin = sessionStorage.getItem('isadmin')
    console.log(isAdmin)
    // Params to catch id of post in url 
    let user = useParams()

    // Fetch Data for user
    useEffect(() => {
        if (isAdmin == 'true') {
            fetch(`https://localhost:5000/api/admins?username=${user.username}`)
            .then(response => response.json())
            .then(json => setData(json))
        }else{
            fetch(`https://localhost:5000/api/users/${user.username}`)
            .then(response => response.json())
            .then(json => setData(json))
        }
    }, [])

    return (
        <>
            <div className="d-flex" style={{height: "90%", display: "flex", alignItems: "center", justifyContent: "center"}}>
                <div className="card " style={{width: "90%", height: "90%", borderRadius: "25px", boxShadow: "0 4px 8px 0 #1b1b1b4d"}}>
                    <div className="row">
                        <div className="col-sm-4">
                            <div className="card-body text-center">
                                <img src={'data:image/png;base64,' + data.profilePicture} height="200px" width="200px" alt="Your Photo" />
                            </div>
                        </div>

                        <div className="col-sm-8">
                            <div className="card-body">
                                <div className="mt-5 row">
                                    <div className="col-sm-5">
                                        <h3>{data.username}</h3>
                                        <h5>{isAdmin ? 'Admin' : 'User'}</h5>
                                    </div>
                                </div>

                                <div className="mt-5 row justify-content-center">
                                    <div className="col-4">
                                        <h5>Name</h5>
                                        <br />
                                        <h5>E-mail</h5>
                                        <br />
                                        <h5>Phone</h5>
                                        <br />
                                        <h5>{isAdmin ? null : 'Plan'}</h5>
                                    </div>
                                    <div className="col-8">
                                        <h5>{data.name}</h5>
                                        <br />
                                        <h5>{data.email}</h5>
                                        <br />
                                        <h5>{data.phone}</h5>
                                        <br />
                                        <h5>{isAdmin ? null : data.planType}</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}

export default ProfilePage