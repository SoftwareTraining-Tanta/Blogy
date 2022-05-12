fetch("https://localhost:5000/api/users/10").
then(response => response.json()).
then(json=>{console.log(json.name)});