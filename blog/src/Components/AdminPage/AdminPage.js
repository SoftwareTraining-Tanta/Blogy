import React, { useEffect } from 'react'
import Dashboard from '../DashBoard/Dashboard'


function AdminPage() {


    return (
        <>
            <div class="container-box">
                <Dashboard />
                <div class="main">
                    <div class="block">
                        <h2 name="statistics">Blog statistics</h2>

                        <div class="card my-card">
                            <div class="card-body">
                                <span class="number"> 123 </span>
                                <span class="title"> Registerd users </span>
                            </div>
                        </div>

                        <div class="card my-card">
                            <div class="card-body">
                                <span class="number"> 512 </span>
                                <span class="title"> Posts </span>
                            </div>
                        </div>

                        <div class="card my-card">
                            <div class="card-title">
                                Top post
                            </div>
                            <div class="card-body">
                                <a href="#"><span class="content"> Top post title is written here </span></a>
                            </div>
                        </div>

                        <div class="card my-card">
                            <div class="card-title">
                                Most active user
                            </div>
                            <div class="card-body">
                                <a href="#"><span class="content"> MoHarby12 </span></a>
                            </div>
                        </div>

                    </div>

                    {/* <div class="block">
                        <h2 name="users">All users</h2>
                        <div class="row" style={{ paddingLeft: '30px' }}>
                            <div class="col-4">
                                <div class="card" style={{ width: '18rem', marginBottom: '30px' }}>
                                    <img class="card-img-top" src="img.png" alt="Card image cap" />
                                    <div class="card-body">
                                        <h5 class="card-title">MoHarby12</h5>
                                        <a href="#" class="btn btn-primary">Send e-mail</a>
                                        <a href="#" class="btn btn-danger">Remove user</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div> */}

                </div>
            </div>
        </>
    )
}

export default AdminPage