using BlogyBackend.Dtos;
using BlogyBackend.Models;
using BlogyBackend.Shared;
using Microsoft.AspNetCore.Mvc;
namespace BlogyBackend.Controllers;
[ApiController]
[Route("api/users")]
public class PostController : ControllerBase
{
    [HttpPost]
    public ActionResult Add(PostDto postDto)
    {
        Post _post = new();
        _post.Add(postDto.AsNormal());
        return Ok();
    }
    [HttpGet("limit/{limit}")]
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
    public ActionResult Delete(int id)
    {
        Post _post = new();
        _post.Delete(id);
        return Ok();
    }
}