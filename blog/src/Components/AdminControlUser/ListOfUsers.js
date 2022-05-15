import React, { useEffect, useState } from 'react'
import Dashboard from '../DashBoard/Dashboard'

function ListOfUsers() {

    const [data, setData] = useState([])

    useEffect(() => {
        fetch("https://localhost:5000/api/users/limit/2000")
            .then(response => response.json())
            .then(json => setData(json))
    }, [])

    return (
        <>

            <div className='container-box'>
                <Dashboard />
                <div className='ms-3 mt-3'>
                    <h2 className='text-primary mb-4'>All users</h2>
                    <div class="row" style={{ paddingLeft: '30px' }}>
                        {data.map((i) => {
                            return (
                                <div class="col-4">
                                    <div class="card" style={{ width: '18rem', marginBottom: '30px' }}>
                                        <img class="card-img-top" src={'data:image/png;base64,' + i.profilePicture} alt="Card image cap" />
                                        <div class="card-body">
                                            <h5 class="card-title">{i.username}</h5>
                                            <a href="#" class="btn btn-primary">Send e-mail</a>
                                            <a href="#" class="btn btn-danger">Remove user</a>
                                        </div>
                                    </div>
                                </div>
                            )
                        })}
                    </div>
                </div>
            </div>

        </>
    )
}

export default ListOfUsers