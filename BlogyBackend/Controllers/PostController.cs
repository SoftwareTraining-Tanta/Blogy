using System.Security.Claims;
using BlogyBackend.Dtos;
using BlogyBackend.Models;
using BlogyBackend.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace BlogyBackend.Controllers;
[ApiController]
[Route("api/posts")]
// [Authorize(Roles = $"{Roles.Premium},{Roles.Admin}")]
public class PostController : ControllerBase
{
    [HttpPost]
    public ActionResult Add(PostDto postDto)
    {
        //     var roles = ((ClaimsIdentity)User.Identity!).Claims
        //             .Where(c => c.Type == ClaimTypes.Role)
        //             .Select(c => c.Value);
        User _user = new();
        User user = _user.Get(postDto.Username!);
        Post _post = new();
        // foreach (string role in roles)
        // {
        //     if (role == Roles.Premium && user.PostCount >= 2)
        //         throw new Exception("User has reached the limit of posts");
        //     if (role == Roles.Premium && user.PostCount < 2)
        //     {
        //         _user.IncrementPostCount(postDto.Username!);
        //         return Ok("Done");
        //     }
        //     if (role == Roles.Admin)
        //     {
        //         _post.Add(postDto.AsNormal());
        //     }
        // }
        _post.Add(postDto.AsNormal());
        return Ok("Done");
        // return NotFound("User not authorized");
    }
    [HttpGet("limit/{limit}")]
    // [AllowAnonymous]
    public ActionResult Get(int limit)
    {
        using (blogyContext db = new())
        {
            var posts = db.Posts.Take(limit).ToList();
            return Ok(posts.AsDto());
        }
    }
    [HttpGet("{id}")]
    public ActionResult GetById(int id)
    {
        Post _post = new();
        return Ok(_post.Get(id));
    }
    [HttpPut("updateContent/{id}")]
    public ActionResult UpdateContent(int id, string content)
    {
        Post _post = new();
        _post.UpdateContent(id, content);
        return Ok();
    }
    [HttpDelete("{id}")]
    // [Authorize(Roles = Roles.Admin)]
    public ActionResult Delete(int id)
    {
        Post _post = new();
        _post.Delete(id);
        return Ok();
    }
    [HttpPost("pinPost/{username}/{postId}")]
    public ActionResult PinPost(string username, int postId)
    {
        Post _post = new();
        _post.PinPost(username, postId);
        return Ok("Done");
    }
    [HttpPut("updateReachCount/{id}")]
    public ActionResult UpdateReachCount(int id)
    {
        Post _post = new();
        _post.UpdateReachCount(id);
        return Ok();
    }
    // [HttpGet("GetPinnedPosts/{username}")]
    // public ActionResult GetPinnedPosts(string username)
    // {
    //     Post _post = new();
    //     return Ok(_post.GetPinnedPosts(username));
    // }
}
