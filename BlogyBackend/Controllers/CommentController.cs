using System.Security.Claims;
using BlogyBackend.Dtos;
using BlogyBackend.Models;
using BlogyBackend.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Route("api/comments")]
// [Authorize]
public class CommentController : ControllerBase
{
    [HttpPost("putcomment")]
    public ActionResult PutComment([FromBody] CommentDto commentDto)
    {
        User _user = new();
        Post _post = new();
        Post commentPost = _post.Get(commentDto.PostId);
        var roles = ((ClaimsIdentity)User.Identity!).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);
        // foreach (var role in roles)
        // {
        //     if (role == Roles.Admin || role == Roles.Premium || _post.IsAdmin)
        //     {
        //         Comment.Add(commentDto.AsNormal());
        //         return Ok("Done");

        //     }
        //     if (!_post.IsAdmin)
        //         continue;
        // }
        Comment.Add(commentDto.AsNormal());
        return Ok("Done");
        // return BadRequest("Error adding comment , User not authorized");
    }
    [HttpGet("limit/{limit}")]
    public ActionResult GetLimit(int limit)
    {
        return Ok(Comment.GetLimit(limit));
    }

}