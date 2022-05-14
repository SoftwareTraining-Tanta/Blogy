namespace BlogyBackend.Dtos;
public class CommentDto
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public string Username { get; set; } = null!;
    public int PostId { get; set; }
    public string? AdminUsername { get; set; }
    public bool IsAdmin { get; set; }
}