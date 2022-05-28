import React, { useEffect, useState } from 'react'

function AdminControlPosts() {

    const [data, setData] = useState([])

    const deletePosts = (id) => {
        fetch(`https://localhost:5000/api/posts/${id}`, {
            method: "DELETE",
            headers: { 'Content-Type': 'application/json' },
        }).
            then(response => response.text()).
            then(json => console.log(json));
        window.location.href = `/adminposts`    
    }

    // Fetch Data All Posts
    useEffect(() => {
        fetch("https://localhost:5000/api/posts/limit/2000")
            .then(response => response.json())
            .then(json => setData(json))
    }, [])

    return (
        <>
            <div className='container'>
                {data.map((i) => {
                    return (
                        <div class="card mb-2">
                            <div class="card-body">
                                {i.content}
                            </div>
                            <div className='d-flex justify-content-end'>
                                <button className='p-1 me-2 mb-2 bg-primary text-light' onClick={() => deletePosts(i.id)}>Delete</button>
                            </div>
                        </div>
                    )
                })}
            </div>
        </>
    )
}

export default AdminControlPosts