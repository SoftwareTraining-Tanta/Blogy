namespace BlogyBackend.Dtos;
public class PostDto
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;
    public string DateTime { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string? Image { get; set; }

}