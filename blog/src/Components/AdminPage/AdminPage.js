import React, { useEffect, useState } from 'react'
import Dashboard from '../DashBoard/Dashboard'


function AdminPage() {

    const [numSignedUpUsers, setNumSignedUpUsers] = useState()
    const [numPosts, setNumPosts] = useState([])
    const [mostInteractedPost, setMostInteractedPost] = useState({})
    const isadmin = sessionStorage.getItem('isadmin')


    useEffect(()=>{
        if (isadmin != 'true') {
            window.location.href = '*'
        }
    },[])

    useEffect(() => {
        fetch("https://localhost:5000/api/admins/SignedUpUsers")
            .then(response => response.text())
            .then(json => setNumSignedUpUsers(json))
    }, [])

    useEffect(() => {
        fetch("https://localhost:5000/api/posts/limit/2000")
            .then(response => response.json())
            .then(json => setNumPosts(json))
    }, [])

    useEffect(() => {
        fetch("https://localhost:5000/api/admins/MostInteractedPost")
            .then(response => response.json())
            .then(json => setMostInteractedPost(json))
    }, [])


    return (
        <>
            <div class="container-box">
                <Dashboard />
                <div class="main">
                    <div class="block">
                        <h2 name="statistics">Blog statistics</h2>

                        <div class="card my-card">
                            <div class="card-body">
                                <span class="number"> {numSignedUpUsers} </span>
                                <span class="title"> Registerd users </span>
                            </div>
                        </div>

                        <div class="card my-card">
                            <div class="card-body">
                                <span class="number"> {numPosts.length} </span>
                                <span class="title"> Posts </span>
                            </div>
                        </div>

                        <div class="card my-card">
                            <div class="card-title">
                                Top post
                            </div>
                            <div class="card-body">
                                <a href="#"><span class="content"> {mostInteractedPost.title} </span></a>
                            </div>
                        </div>
                        
                    </div>

                </div>
            </div>
        </>
    )
}

export default AdminPage