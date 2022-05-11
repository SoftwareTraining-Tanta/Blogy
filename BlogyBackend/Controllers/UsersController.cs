using BlogyBackend.Dtos;
using BlogyBackend.Models;
using BlogyBackend.Shared;
using Microsoft.AspNetCore.Mvc;
namespace BlogyBackend.Controllers;
[ApiController]
[Route("api/users")]
class UsersController : ControllerBase
{
    [HttpPost]
    public ActionResult Add(UserDto userDto)
    {

        User _user = new();
        _user.Add(userDto.AsNormal());
        return CreatedAtAction("Done adding user", userDto);
    }

}