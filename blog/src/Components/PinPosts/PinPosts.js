import React, { useEffect, useState } from 'react'
import { NavLink } from 'react-router-dom'

function PinPosts() {
    // Statues
    const [data, setData] = useState([])
    const [titlePost, setTitlePost] = useState()
    const [contentPost, setContentPost] = useState()
    const [textSearch, setTextSearch] = useState('')
    const [base64String, setBase64String] = useState()
    const username = sessionStorage.getItem('username')
    const admin = sessionStorage.getItem('admin')

    // Fetch Data All Posts
    useEffect(() => {
        fetch(`https://localhost:5000/api/posts/GetPinnedPosts/${username}`)
            .then(response => response.json())
            .then(json => setData(json))
    }, [])

    // Loading Animation
    const noPosts =
        <div style={{ position: 'absolute', left: '50%', top: '50%', transform: 'translate(-50%, -50%)', fontSize:'30px' }}>There Isn't Posts...</div>

    const updateReach = (id) => {
        fetch(`https://localhost:5000/api/posts/updateReachCount/${id}`, {
            method: "PUT",
            headers: { 'Content-Type': 'application/json' }
        }).then(response => response.text())
            .then(json => console.log(json));
    }
    
    return (
        <>
            <div className="container">

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
                                            <div>{i.username}</div>
                                            <div className="small text-muted">{i.dateTime}</div>
                                            <h2 className="card-title">{username || admin ? i.title : i.title.slice(0, 5) + `...`}</h2>
                                            <p className="card-text">{username || admin ? i.content : i.content.slice(0, 10) + `...`}</p>
                                            <NavLink className="btn btn-primary" onClick={() => updateReach(i.id)} to={username ? `/postpage/${i.id}` : `/signin`}>Read more →</NavLink>
                                        </div>
                                    </div>
                                    <div className="col-lg-4"></div>
                                </div>
                            )
                        }) : noPosts}
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
        </>
    )
}

export default PinPosts
