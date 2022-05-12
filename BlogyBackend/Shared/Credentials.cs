using System.ComponentModel.DataAnnotations;

namespace BlogyBackend.Shared;

public class CredentialsHelper
{
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}