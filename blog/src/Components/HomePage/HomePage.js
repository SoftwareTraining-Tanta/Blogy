import React, { useEffect, useState } from 'react'
import { NavLink } from 'react-router-dom'
import { AiOutlinePushpin } from 'react-icons/ai'

function HomePage() {
    // Statues
    const [data, setData] = useState([])
    const [titlePost, setTitlePost] = useState()
    const [contentPost, setContentPost] = useState()
    const [textSearch, setTextSearch] = useState('')
    const [base64String, setBase64String] = useState()
    const [msgResponse, setMsgResponse] = useState()
    const username = sessionStorage.getItem('username')
    const admin = sessionStorage.getItem('admin')
    const isuser = sessionStorage.getItem('isuser')
    const isadmin = sessionStorage.getItem('isadmin')
    const [pending, setPending] = useState(false)


    // Fetch Data All Posts
    useEffect(() => {
        fetch("https://localhost:5000/api/posts/limit/2000")
            .then(response => response.json())
            .then(json => setData(json))
    }, [])

    // Convert Image to Base64
    function convertImageToBase64(x) {
        var file = x.target.files[0]
        var reader = new FileReader();
        console.log("next");
        reader.onload = function () {
            setBase64String(reader.result.replace("data:", "").replace(/^.+,/, ""));
        }
        reader.readAsDataURL(file);
    }

    // Handle Sumbit Button
    const handleSubmit = (x) => {
        x.preventDefault()
        if (isuser == 'true' || isadmin == 'true') {
            setPending(true)
            fetch("https://localhost:5000/api/posts", {
                method: "POST",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ title: titlePost, content: contentPost, dateTime: String(new Date()).split('GMT')[0], username: isuser == 'true' ? username : null, image: base64String, adminUsername: isadmin == 'true' ? admin : null, isAdmin: isadmin == 'true' ? true : false })
            }).
                then(response => response.text()).
                then(json => setMsgResponse(json));
        } else {
                alert('Sign In Firstly')
        }
    }

    useEffect(() => {
        if (msgResponse == 'Done') {
            window.location.href = '/'
        } else {
            setPending(false)
        }
    }, [msgResponse])

    // Loading Animation
    const loadingAnimation =
        <div style={{ position: 'absolute', left: '50%', top: '50%', transform: 'translate(-50%, -50%)' }}>
            <span style={{ fontSize: '25px', marginRight: '10px' }}>Loading</span>
            <div className="spinner-border" role="status"></div>
        </div>

    // Loading Animation
    const loadingAnimationPosts =
        <div style={{ position: 'absolute', left: '50%', top: '90%', transform: 'translate(-50%, -50%)' }}>
            <span style={{ fontSize: '25px', marginRight: '10px' }}>Loading</span>
            <div className="spinner-border" role="status"></div>
        </div>

    const updateReach = (id) => {
        fetch(`https://localhost:5000/api/posts/updateReachCount/${id}`, {
            method: "PUT",
            headers: { 'Content-Type': 'application/json' }
        }).then(response => response.text())
            .then(json => console.log(json));
    }

    const pinPost = (id) => {
        fetch(`https://localhost:5000/api/posts/pinPost/${username}/${id}`, {
            method: "POST",
            headers: { 'Content-Type': 'application/json' }
        }).then(response => response.text())
            .then(json => console.log(json));
    }

    return (
        <>
            {pending ? loadingAnimation :
                <>
                    <div className="container">

                        <div className="row">
                            {/* Post Form */}
                            <form className="col-lg-8 d-flex flex-column mb-5 mx-auto border border-3 border-primary rounded py-2 h-100" onSubmit={handleSubmit}>
                                <h3 className='mx-auto mb-3'>What's on your mind?</h3>

                                <label className='mb-1'>Title:</label>
                                <input className='mb-3' type='text' onChange={(x) => { setTitlePost(x.target.value) }} />

                                <label className='mb-1'>Content:</label>
                                <textarea className='mb-3' style={{ height: '100px' }} type='text' onChange={(x) => { setContentPost(x.target.value) }} />

                                <label className='mb-1'>Choose a picture:</label>
                                <input className='mb-3 form-control' type='file' onChange={convertImageToBase64} />

                                <input className='w-25 mx-auto p-1 border-0 bg-primary text-light rounded-pill' type='submit' value='Post' />
                            </ form>
                        </div>

                        {data.length ? <hr /> : null}

                        <div className="row d-flex">

                            {/* Posts*/}
                            <div className="col-lg-8 d-flex flex-column-reverse">
                                {data.length ? data.filter((j) => {
                                    if (textSearch == '') {
                                        return j
                                    } else if (j.title.toLowerCase().includes(textSearch.toLowerCase()) || j.content.toLowerCase().includes(textSearch.toLowerCase())) {
                                        return j
                                    }
                                }).map((i) => {
                                    return (
                                        <div className="col-lg-12" key={i.id}>
                                            <div className="card mb-4">
                                                <img style={{ width: 'auto' }} src={'data:image/png;base64,' + i.image} alt="..." />
                                                <div className="card-body">
                                                    <div className='row d-flex justify-content-end'>
                                                        <AiOutlinePushpin className='fs-3' onClick={() => pinPost(i.id)} style={{ cursor: 'pointer', width: 'fit-content' }} />
                                                    </div>
                                                    <div>{i.isAdmin == true ? i.adminUsername : i.username}</div>
                                                    <div className="small text-muted">{i.dateTime}</div>
                                                    <h2 className="card-title">{isadmin == 'true' || isuser == 'true' ? i.title : i.title.slice(0, 5) + `...`}</h2>
                                                    <p className="card-text">{isadmin == 'true' || isuser == 'true' ? i.content : i.content.slice(0, 10) + `...`}</p>
                                                    <NavLink className="btn btn-primary" onClick={() => updateReach(i.id)} to={isadmin == 'true' || isuser == 'true' ? `/postpage/${i.id}` : isuser ? `/signin` : `/signinadmin`}>Read more →</NavLink>
                                                </div>
                                            </div>
                                            <div className="col-lg-4"></div>
                                        </div>
                                    )
                                }) : loadingAnimationPosts}
                            </div>

                            {/* Search*/}
                            {data.length ?
                                <div className="col-lg-4">
                                    <div className="card mb-4">
                                        <div className="card-header">Search</div>
                                        <div className="card-body">
                                            <div className="input-group">
                                                <input className="form-control" type="text" placeholder="Enter search term..." aria-label="Enter search term..." aria-describedby="button-search" onChange={(x) => setTextSearch(x.target.value)} />
                                            </div>
                                        </div>
                                    </div>
                                </div> : null}

                        </div>

                    </div>
                </>}
        </>
    )
}

export default HomePage