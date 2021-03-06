using System.Security.Claims;
using BlogyBackend.Dtos;
using BlogyBackend.Models;
using BlogyBackend.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
namespace BlogyBackend.Controllers;
[ApiController]
[Route("api/users")]
// [Authorize(Roles = $"{Roles.Admin},{Roles.Basic},{Roles.Premium}")]
public class UserController : ControllerBase
{
    [HttpPost]
    // [AllowAnonymous]
    public ActionResult Add(UserDto userDto)
    {

        User _user = new();
        Plan _plan = new();
        _user.Add(userDto.AsNormal());
        PlanDto planDto = new()
        {
            Type = userDto.PlanType,
            Username = userDto.Username
        };
        _plan.Add(planDto.AsNormal());
        return Ok();
    }

    [HttpGet("limit/{limit}")]
    // [Authorize(Roles = $"{Roles.Admin},{Roles.Premium},{Roles.Basic}")]

    public ActionResult<List<User>> Get(int limit)
    {
        User _user = new();
        List<UserDto> users = _user.GetLimit(limit).AsDto();
        return Ok(users);
    }

    [HttpGet("{username}")]
    // [Authorize(Roles = $"{Roles.Admin},{Roles.Premium},{Roles.Basic}")]
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
            return Ok(userDto);
        }
        catch
        {
            return NotFound("user not found");
        }
    }
    [HttpPost("register")]
    // [AllowAnonymous]
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
    // [AllowAnonymous]
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
    // [AllowAnonymous]
    public async Task<ActionResult> Login(string username, string password)
    {
        try
        {
            User _user = new();
            User? user = _user.Get(username);
            if (user == null)
                return NotFound("User not found");
            if (user.Password != password)
                return BadRequest("Username or password is incorrect");
            // string planType = Plan.Get(user.Username).Type;
            // var claimsUser = new List<Claim>{
            //             new Claim(ClaimTypes.Name,username),
            //             new Claim(ClaimTypes.Role,planType),
            //             new Claim(ClaimTypes.Email,user.Email!)
            //             };
            // var identity = new ClaimsIdentity(claimsUser, Authentications.user);
            // ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            // _user.UpdateIsSigned(user.Username);
            // await HttpContext.SignInAsync(principal);
            // var roles = ((ClaimsIdentity)User.Identity!).Claims
            //     .Where(c => c.Type == ClaimTypes.Role)
            //     .Select(c => c.Value);
            // foreach (var role in roles)
            // {
            //     Console.WriteLine(role.ToString());
            // }
            await Task.CompletedTask;
            return Ok("user");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("Delete/{username}")]
    // [Authorize(Roles = Roles.Admin)]
    public ActionResult Delete(string username)
    {
        // try
        // {
        User _user = new();
        _user.Delete(username);
        return Ok("User deleted successfully");
        // }
        // catch (Exception ex)
        // {
        //return BadRequest(ex.Message);
        // }
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        // User _user = new();
        // _user.UpdateIsSigned(User.Identity!.Name!);
        await HttpContext.SignOutAsync();
        return Ok("Logged out successfully");
    }
    [HttpPost("sendemailtoadmin/{username}/{message}")]
    public ActionResult SendEmailToAdmin(string username, string message)
    {
        try
        {
            User _user = new();
            User user = _user.GetWithPlan(username);
            if (user == null) return NotFound("User not found");
            if (user.Plans.First().Type == "Basic")
                return BadRequest("You can't send email to admin , User not authorized");
            Smtp.SendMessage(Smtp.From, user.Username, message);
            return Ok("Email sent successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}