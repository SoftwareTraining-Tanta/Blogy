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
    const admin = sessionStorage.getItem('admin')
    const isuser = sessionStorage.getItem('isuser')
    const isadmin = sessionStorage.getItem('isadmin')
    const [pending, setPending] = useState(false)


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
        setPending(true)
        fetch("https://localhost:5000/api/comments/putcomment", {
            method: "POST",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ content: commentPost, username: isuser == 'true' ? username : null, postId: post.id, adminUsername: isadmin == 'true' ? admin : null, isAdmin: isadmin =='true' ? true : false })
        }).
            then(response => response.text()).
            then(json => setMsgResponse(json));
    }

    // Loading Animation
    const loadingAnimation =
        <div style={{ position: 'absolute', left: '50%', top: '50%', transform: 'translate(-50%, -50%)' }}>
            <span style={{ fontSize: '25px', marginRight: '10px' }}>Loading</span>
            <div className="spinner-border" role="status"></div>
        </div>

    useEffect(() => {
        if (msgResponse == 'Done') {
            window.location.href = `/postpage/${post.id}`
        } else {
            setPending(false)
        }
    }, [msgResponse])

    return (
        <>
            {pending ? loadingAnimation :
                <>
                    <div class="container mt-5">
                        <div class="row">
                            <div class="col-lg-8">

                                {/* Post */}
                                <article>
                                    <header class="mb-4">
                                        <h1 class="fw-bolder mb-1">{data.title}</h1>
                                        <hr />
                                        <div class="text-muted fst-italic mb-2">{data.dateTime}</div>
                                        <div class="text-muted fst-bold mb-2">Reach: {data.reachCount} Users</div>
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
                                                        <div class="ms-3">
                                                            <div class="fw-bold">{i.isAdmin ? i.adminUsername : i.username}</div>
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
                </>}
        </>
    )
}

export default PostPage