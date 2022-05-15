import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import NavBar from '../NavBar/NavBar'

function PostPage() {
    
    // Statues
    const [data, setData] = useState({})
    const [dataComments, setDataComments] = useState([])
    const [commentPost, setCommentPost] = useState()
    const [msgResponse, setMsgResponse] = useState()

    const username = sessionStorage.getItem('username')

    // Params to catch id of post in url 
    let post = useParams()

    // Fetch Data One Post
    useEffect(() => {
        fetch(`https://localhost:5000/api/posts/${post.id}`)
            .then(response => response.json())
            .then(json => setData(json))
    }, [])

    // Fetch Data Of Comments
    useEffect(() => {
        fetch(`https://localhost:5000/api/comments/limit/1000/${post.id}`)
            .then(response => response.json())
            .then(json => setDataComments(json))
    }, [])

    // Handle Sumbit Button
    const handleSubmit = (x) => {
        x.preventDefault()
        fetch("https://localhost:5000/api/comments/putcomment", {
            method: "POST",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ content: commentPost, username: username, postId: post.id, adminUsername: null, isAdmin: false })
        }).
            then(response => response.text()).
            then(json => setMsgResponse(json));
    }

    useEffect(()=>{
        if (msgResponse == 'Done') {
            window.location.href = `/postpage/${post.id}`
        }
    },[msgResponse])

    return (
        <>
            <div class="container mt-5">
                <div class="row">
                    <div class="col-lg-8">

                        {/* Post */}
                        <article>
                            <header class="mb-4">
                                <h1 class="fw-bolder mb-1">{data.title}</h1>
                                <div class="text-muted fst-italic mb-2">{data.dateTime}</div>
                                <div class="text-muted fst-bold mb-2">{data.reachCount}</div>
                            </header>
                            <figure class="mb-4"><img class="img-fluid rounded" src={'data:image/png;base64,' + data.image} alt="..." /></figure>
                            <section class="mb-5">
                                <p class="fs-5 mb-4">{data.content}</p>
                            </section>
                        </article>

                        {/* Comments */}
                        <section class="mb-5">
                            <div class="card bg-light">
                                <div class="card-body">
                                    {/* Form Comment */}
                                    <form class="mb-4" onSubmit={handleSubmit}>
                                        <textarea class="form-control mb-3" rows="3" placeholder="Join the discussion and leave a comment!" onChange={(x) => { setCommentPost(x.target.value) }}></textarea>
                                        <input type="submit" class="btn btn-primary w-25" value='Submit' />
                                    </form>

                                    <hr />

                                    {/* Comments */}
                                    {dataComments.map((i) => {
                                        return (
                                            <div class="d-flex mb-4">
                                                {/* <div class="flex-shrink-0"><img class="rounded-circle" src="https://dummyimage.com/50x50/ced4da/6c757d.jpg" alt="..." /></div> */}
                                                <div class="ms-3">
                                                    <div class="fw-bold">{i.username}</div>
                                                    {i.content}
                                                </div>
                                            </div>
                                        )
                                    })}

                                </div>
                            </div>
                        </section>
                    </div>
                </div>
            </div>
        </>
    )
}

export default PostPage