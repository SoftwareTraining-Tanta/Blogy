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
    [HttpPost("register")]
    public ActionResult Register(UserDto userDto)
    {
        try
        {

            User _user = new();
            string verficationCode = _user.Register(userDto.AsNormal());
            return Ok("valid");
        }
        catch (Exception ex)
        {
            return BadRequest("invalid");
        }

    }
    [HttpPost("verify/{username}/{verificationCode}")]
    public ActionResult Verify(string username, string verificationCode)
    {
        try
        {
            User _user = new();
            _user.Verify(username, verificationCode);
            return Ok("User verified successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}