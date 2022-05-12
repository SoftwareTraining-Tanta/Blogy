using System.Security.Claims;
using BlogyBackend.Dtos;
using BlogyBackend.Models;
using BlogyBackend.Shared;
using Microsoft.AspNetCore.Authentication;
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
            return Ok(verficationCode);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("verify/{username}/{verificationCode}/{planType}")]
    public ActionResult Verify(string username, string verificationCode, string planType)
    {
        try
        {
            User _user = new();
            _user.Verify(username, verificationCode, planType);
            return Ok("User verified successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("login/{username}/{password}")]
    public async Task<ActionResult> Login(string username, string password)
    {
        try
        {
            User _user = new();
            User? user = _user.Get(username);
            if (user == null)
                throw new Exception("Username or password is incorrect");
            if (username == "admin" && password == "2510203121")
            {
                var claims = new List<Claim>{
                        new Claim(ClaimTypes.Name,username),
                        new Claim(ClaimTypes.Role,username),
                        new Claim(ClaimTypes.Email,user.Email!)
                    };
                var identity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("login", principal);
                return Ok("admin");
            }
            if (user.Password != password)
                throw new Exception("Username or password is incorrect");
            return Ok("user");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}