using BlogyBackend.Dtos;
using BlogyBackend.Models;
using BlogyBackend.Shared;
using Microsoft.AspNetCore.Mvc;
namespace BlogyBackend.Controllers;
[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    [HttpPost]
    public ActionResult Add(UserDto userDto)
    {

        User _user = new();
        _user.Add(userDto.AsNormal());
        return Ok();
        // return CreatedAtAction("Done adding user", userDto);
    }
    [HttpGet("{limit}")]
    public ActionResult Get(int limit)
    {
        using (blogyContext db = new())
        {
            var users = db.Users.Take(limit).ToList();
            return Ok(users.AsDto());
        }
    }
}