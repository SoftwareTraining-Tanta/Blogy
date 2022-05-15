import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'

function ProfilePage() {
    // Statues
    const [data, setData] = useState({})

    // Params to catch id of post in url 
    let user = useParams()


    // Fetch Data for user
    useEffect(() => {
        fetch(`https://localhost:5000/api/users/${user.username}`)
            .then(response => response.json())
            .then(json => console.log(json))
    }, [])

    return (
        <>
            <div class="container mt-5">
                <div class="row">
                    <div class="col-lg-8">

                        {/* Post */}
                        <article>
                            <header class="mb-4">
                                <h1 class="fw-bolder mb-1">{data.username}</h1>
                                <div class="text-muted fst-italic mb-2">{data.name}</div>
                            </header>
                            <figure class="mb-4"><img class="img-fluid rounded" src={'data:image/png;base64,' + data.profilePicture} alt="..." /></figure>
                            <section class="mb-5">
                                <p class="fs-5 mb-4">{data.email}</p>
                            </section>
                        </article>
                    </div>
                </div>
            </div>
        </>
    )
}

export default ProfilePage