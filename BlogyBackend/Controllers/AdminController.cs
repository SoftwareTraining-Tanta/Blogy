using System.Security.Claims;
using BlogyBackend.Dtos;
using BlogyBackend.Models;
using BlogyBackend.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace BlogyBackend.Controllers;




[ApiController]
[Route("api/admins")]
<<<<<<< HEAD
//[Authorize(Roles.Admin)]
public class AdminController : ControllerBase
{
    [HttpPost]
    //[AllowAnonymous]
=======
// [Authorize(Roles.Admin)]
public class AdminController : ControllerBase
{
    [HttpPost]
    // [AllowAnonymous]
>>>>>>> 97d39c198d9659170dc2200d1bfaf16cbcc0b5b6
    public async Task<ActionResult> Add(AdminDto adminDto)
    {

        Admin admin = new();
        await Task.Run(() => (admin.Register(adminDto.AsNormal())));
        return Ok();
    }
    [HttpGet]
    public async Task<AdminDto?> Get([FromQuery] string username)
    {
        AdminDto? admin=new();
        Console.Write(User.Identity.Name);

        if(User.Identity.IsAuthenticated){
         admin = await Task.Run(() => (Admin.Get(username)));
        }
        return admin;

    }
    [HttpPut]
    public async Task<AdminDto?> Update([FromQuery] string username, [FromBody] AdminDto admin)
    {
        await Admin.UpdateAsync(username, admin);
        return Admin.Get(username);
    }

<<<<<<< HEAD
    //[AllowAnonymous]
=======
    // [AllowAnonymous]
>>>>>>> 97d39c198d9659170dc2200d1bfaf16cbcc0b5b6
    [HttpGet("login")]
    public async Task<ActionResult<AdminDto>> Login([FromQuery] string username, [FromQuery] string password)
    {
        try
        {
            AdminDto? admin = Admin.Get(username);


            if (admin == null)
                throw new Exception("Username or password is incorrect");
            if (Admin.Credentials(username, password))
            {
                var claims = new List<Claim>{
                        new Claim(ClaimTypes.Name,username),
                        new Claim(ClaimTypes.Role,Roles.Admin),
                        new Claim(ClaimTypes.Email,admin.Email!)
                    };
                var identity = new ClaimsIdentity(claims, Authentications.AdminAuthentication);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);
            }
            if (admin.Password != password)
                throw new Exception("Username or password is incorrect");
            return Ok(admin);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("logout")]
    public async Task<ActionResult<string>> Logout()
    {

        await HttpContext.SignOutAsync();
        return "SignedOut";
    }
    [HttpPost("register")]
<<<<<<< HEAD
    //[AllowAnonymous]
=======
    // [AllowAnonymous]
>>>>>>> 97d39c198d9659170dc2200d1bfaf16cbcc0b5b6
    public ActionResult<AdminDto> Register([FromBody] AdminDto adminDto)
    {
        try
        {
            Admin admin = new();
            admin.Register(adminDto.AsNormal());
            return Ok("Done");
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
    [HttpGet("OnlineUsers/{limit}")]
    public ActionResult<List<User>> OnlineUsers(int limit){

        return Admin.getOnlineUsers(limit);
    }
    [HttpGet("SignedUpUsers/{limit}")]
    public ActionResult<List<User>> SignedUpUsers(int limit){

        return Admin.SignedUpUsers(limit);
    }

}