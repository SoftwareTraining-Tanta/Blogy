import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import './profilePage.css';

function ProfilePage() {

    // Statues
    const [data, setData] = useState({})

    // Params to catch id of post in url 
    let username = useParams()

    // Fetch Data One Post
    useEffect(() => {
        fetch(`https://localhost:5000/api/users/${username.username}`)
            .then(response => response.json())
            .then(json => setData(json))
    }, [])

    return (
        <>
            <div class="back">
                <div class="card">
                    <div class="left">
                        <img src={'data:image/png;base64,' + data.profilePicture} height="200px" width="200px" alt="Photo" />
                    </div>
                    <div class="right">
                        <div class="card-card">
                            <div>
                                <h3 class="left-text">{data.username}</h3>
                            </div>
                        </div>
                        <div class="card-card">
                            <div class="left-text">
                                <h5>Name</h5>
                                <br />
                                <h5>E-mail</h5>
                                <br />
                                <h5>Phone</h5>
                                <br />
                                <h5>Plan</h5>
                            </div>
                            <div class="left-text">
                                <h5>{data.name}</h5>
                                <br />
                                <h5>{data.email}</h5>
                                <br />
                                <h5>{data.phone}</h5>
                                <br />
                                <h5>{data.planType}</h5>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}

export default ProfilePage