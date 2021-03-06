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

public class AdminController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Add(AdminDto adminDto)
    {

        Admin admin = new();
        await Task.Run(() => (admin.Register(adminDto.AsNormal())));
        return Ok();
    }
    [HttpGet]
    public async Task<AdminDto?> Get([FromQuery] string username)
    {
        AdminDto? admin = new();
        // Console.Write(User.Identity.Name);

        // if (User.Identity.IsAuthenticated)
        // {
        admin = await Task.Run(() => (Admin.Get(username)));
        // }
        return admin;

    }
    [HttpPut]
    public async Task<AdminDto?> Update([FromQuery] string username, [FromBody] AdminDto admin)
    {
        await Admin.UpdateAsync(username, admin);
        return Admin.Get(username);
    }

    [HttpGet("login")]
    public async Task<ActionResult<AdminDto>> Login([FromQuery] string username, [FromQuery] string password)
    {
        try
        {
            AdminDto? admin = Admin.Get(username);


            if (admin == null)
                return NotFound("user not found");
            if (Admin.Credentials(username, password))
            {
                return Ok("admin");
                // var claims = new List<Claim>{
                //         new Claim(ClaimTypes.Name,username),
                //         new Claim(ClaimTypes.Role,Roles.Admin),
                //         new Claim(ClaimTypes.Email,admin.Email!)
                //     };
                // var identity = new ClaimsIdentity(claims, Authentications.AdminAuthentication);
                // ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                // await HttpContext.SignInAsync(principal);
            }
            // if (admin.Password != password)
            await Task.CompletedTask;
            return BadRequest("Username or password is incorrect");
            // return Ok(admin);
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
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("OnlineUsers/")]
    public ActionResult<int> OnlineUsers()
    {

        return Admin.getOnlineUsers();
    }
    [HttpGet("SignedUpUsers/")]
    public ActionResult<int> SignedUpUsers()
    {

        return Admin.SignedUpUsers();
    }
    [HttpGet("MostInteractedPost/")]
    public ActionResult<Post> BestPost()
    {

        return Admin.PostWithMostInteraction();
    }
    [HttpGet("MostReach/")]
    public ActionResult<int> NUmberOfMostReach()
    {

        return Admin.NumberOfMostReach();
    }
    [HttpGet("MostReachPosts/{limit}/")]
    public ActionResult<List<Post>> NumberOfMostReach(int limit)
    {

        return Admin.MostReachPosts(limit);
    }
    [HttpPost("sendtouser")]
    public ActionResult SendToUser(string username, string subject, string message)
    {
        User _user = new();
        User user = _user.Get(username);
        if (user == null) return NotFound("User not found");
        Smtp.SendMessage(user.Email!, subject, message);
        return Ok("Email sent successfully");
    }



}