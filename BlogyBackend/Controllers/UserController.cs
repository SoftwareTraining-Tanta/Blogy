using System.Security.Claims;
using BlogyBackend.Dtos;
using BlogyBackend.Models;
using BlogyBackend.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace BlogyBackend.Controllers;
[ApiController]
[Route("api/users")]
[Authorize(Policy = Roles.Premium)]
[Authorize(Policy = Roles.Basic)]
public class UserController : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public ActionResult Add(UserDto userDto)
    {

        User _user = new();
        _user.Add(userDto.AsNormal());
        return Ok();
    }

    [HttpGet("limit/{limit}")]
    public ActionResult Get(int limit)
    {
        User _user = new();
        List<User> users = _user.GetLimit(limit);
        return Ok(users);
    }

    [HttpGet("{username}")]
    public ActionResult Get(string username)
    {
        User _user = new();
        User user = _user.Get(username);
        string planType = Plan.Get(username).Type;
        UserDto userDto = user.AsDto() with
        {
            PlanType = planType
        };
        try
        {
            user.Password = null!;
            return Ok(user.AsDto());
        }
        catch
        {
            return NotFound("user not found");
        }
    }
    [HttpPost("register")]
    [AllowAnonymous]
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
    [HttpPost("putcomment")]
    public ActionResult PutComment(CommentDto commentDto)
    {

        User _user = new();
        _user.PutComment(commentDto);
        return Ok("Done");
    }

    [HttpPost("verify/{username}/{verificationCode}/{planType}")]
    [AllowAnonymous]
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
    [AllowAnonymous]
    public async Task<ActionResult> Login(string username, string password)
    {
        try
        {
            User _user = new();
            User? user = _user.Get(username);
            string planType = Plan.Get(user.Username).Type;
            if (user == null)
                throw new Exception("Username or password is incorrect");
            if (user.Password != password)
                throw new Exception("Username or password is incorrect");
            var claimsUser = new List<Claim>{
                        new Claim(ClaimTypes.Name,username),
                        new Claim(ClaimTypes.Role,planType),
                        new Claim(ClaimTypes.Email,user.Email!)
                        };
            var identity = new ClaimsIdentity(claimsUser, Authentications.user);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
            return Ok("user");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Ok("Logged out successfully");
    }
}