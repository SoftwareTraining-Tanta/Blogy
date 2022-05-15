 fetch("https://localhost:5002/api/users/login/bbb/string",{method:"POST"})
 .then(Response=>{fetch("https://localhost:5002/api/users/bbb",{method:"GET"})})
