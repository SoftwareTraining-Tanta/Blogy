using BlogyBackend.Models;
using BlogyBackend.Dtos;
namespace BlogyBackend.Shared;

public static class ExtensionMethods
{
    public static UserDto AsDto(this User user)
    {
        return new UserDto
        {
            Username = user.Username,
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone,
            Password = user.Password,
            ProfilePicture = user.ProfilePicture?.ToBase64()
        };
    }
    public static User AsNormal(this UserDto userDto)
    {

        return new User
        {
            Username = userDto.Username,
            Name = userDto.Name,
            Email = userDto.Email,
            Phone = userDto.Phone,
            Password = userDto.Password,
            ProfilePicture = userDto.ProfilePicture?.ToBytes()
        };
    }
}