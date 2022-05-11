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
    public static List<UserDto> AsDto(this ICollection<User> users)
    {
        List<UserDto> userDtos = new();
        foreach (User user in users)
        {
            userDtos.Add(user.AsDto());
        }
        return userDtos;
    }
    public static List<User> AsNormal(this ICollection<UserDto> userDtos)
    {
        List<User> users = new();
        foreach (UserDto userDto in userDtos)
        {
            users.Add(userDto.AsNormal());
        }
        return users;
    }
}