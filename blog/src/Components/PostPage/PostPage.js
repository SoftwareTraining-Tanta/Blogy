import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import NavBar from '../NavBar/NavBar'

function PostPage() {

    // Statues
    const [data, setData] = useState({})
    const [commentPost, setCommentPost] = useState()

    // Params to catch id of post in url 
    let post = useParams()

    // Fetch Data One Post
    useEffect(() => {
        fetch(`https://localhost:5000/api/posts/${post.id}`)
            .then(response => response.json())
            .then(json => setData(json))
    }, [])

    // Handle Sumbit Button
    const handleSubmit = (x) => {
        x.preventDefault()
        // fetch("https://localhost:5000/api/comments/putcomment", {
        //     method: "POST",
        //     headers: { 'Content-Type': 'application/json' },
        //     body: JSON.stringify({ title: titlePost, content: contentPost, dateTime: String(new Date()).split('GMT')[0], username: 'admin', image: base64String }),
        // }).
        //     then(response => response.json()).
        //     then(json => console.log(json));
        window.location.href = `/postpage/${post.id}`
    }

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

                                    {/* Comments */}
                                    <div class="d-flex mb-4">
                                        <div class="flex-shrink-0"><img class="rounded-circle" src="https://dummyimage.com/50x50/ced4da/6c757d.jpg" alt="..." /></div>
                                        <div class="ms-3">
                                            <div class="fw-bold">Commenter Name</div>
                                            If you're going to lead a space frontier, it has to be government; it'll never be private enterprise. Because the space frontier is dangerous, and it's expensive, and it has unquantified risks.
                                        </div>
                                    </div>
                                    <div class="d-flex">
                                        <div class="flex-shrink-0"><img class="rounded-circle" src="https://dummyimage.com/50x50/ced4da/6c757d.jpg" alt="..." /></div>
                                        <div class="ms-3">
                                            <div class="fw-bold">Commenter Name</div>
                                            When I look at the universe and all the ways the universe wants to kill us, I find it hard to reconcile that with statements of beneficence.
                                        </div>
                                    </div>

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