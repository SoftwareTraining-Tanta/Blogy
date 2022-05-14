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
[Authorize("adminstrator")]
public class AdminController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Add(AdminDto adminDto)
    {

        Admin admin = new();
        await Task.Run(() => (admin.Register(adminDto.AsNormal())));
        return Ok();
        // return CreatedAtAction("Done adding user", userDto);
    }
    [HttpGet]
    public async Task<AdminDto?> Get([FromQuery] string username)
    {
        AdminDto? admin = await Task.Run(() => (Admin.Get(username)));
        return admin;
    }
    [HttpPut]
    public async Task<AdminDto?> Update([FromQuery] string username, [FromBody] AdminDto admin)
    {
        await Admin.UpdateAsync(username, admin);
        return Admin.Get(username);
    }

    [AllowAnonymous]
    [HttpPost("login")]
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
                        new Claim(ClaimTypes.Role,"adminstrator"),
                        new Claim(ClaimTypes.Email,admin.Email!)
                    };
                var identity = new ClaimsIdentity(claims, "AdminAuthentication");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);
                return Ok(admin);
            }
            if (admin.Password != password)
                throw new Exception("Username or password is incorrect");
            return Ok("admin");
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
}