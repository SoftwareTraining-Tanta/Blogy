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
    public static PostDto AsDto(this Post post)
    {
        return new PostDto
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            DateTime = post.DateTime,
            Username = post.Username!,
            Image = post.Image?.ToBase64()
        };
    }
    public static Post AsNormal(this PostDto postDto)
    {
        return new Post
        {
            Id = postDto.Id,
            Title = postDto.Title!,
            Content = postDto.Content!,
            DateTime = postDto.DateTime,
            Username = postDto.Username!,
            Image = postDto.Image?.ToBytes()
        };
    }
    public static List<PostDto> AsDto(this ICollection<Post> posts)
    {
        List<PostDto> postDtos = new();
        foreach (Post post in posts)
        {
            postDtos.Add(post.AsDto());
        }
        return postDtos;
    }
    public static List<Post> AsNormal(this ICollection<PostDto> postDtos)
    {
        List<Post> posts = new();
        foreach (PostDto postDto in postDtos)
        {
            posts.Add(postDto.AsNormal());
        }
        return posts;
    }
    public static TempUser AsTempUser(this User user, string verificationCode)
    {
        return new TempUser
        {
            Username = user.Username,
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone,
            Password = user.Password,
            ProfilePicture = user.ProfilePicture,
            VerificationCode = verificationCode
        };
    }

    public static User AsNormalUser(this TempUser tempUser)
    {
        return new User
        {
            Username = tempUser.Username,
            Name = tempUser.Name,
            Email = tempUser.Email,
            Phone = tempUser.Phone,
            Password = tempUser.Password,
            ProfilePicture = tempUser.ProfilePicture
        };
    }
    //Admin methods
    public static AdminDto AsDto(this Admin admin)
    {

        return new AdminDto
        {
            Username = admin.Username,
            Name = admin.Name,
            Email = admin.Email,
            Phone = admin.Phone,
            Password = admin.Password,
            ProfilePicture = admin.ProfilePicture?.ToBase64()
        };
    }

    public static Admin AsNormal(this AdminDto userDto)
    {

        return new Admin
        {
            Username = userDto.Username,
            Name = userDto.Name,
            Email = userDto.Email,
            Phone = userDto.Phone,
            Password = userDto.Password,
            ProfilePicture = userDto.ProfilePicture?.ToBytes()
        };
    }
    public static CommentDto AsDto(this Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Content = comment.Content,
            Username = comment.Username!,
            PostId = comment.PostId,
        };
    }
    public static Comment AsNormal(this CommentDto commentDto)
    {
        return new Comment
        {
            Id = commentDto.Id,
            Content = commentDto.Content!,
            Username = commentDto.Username!,
            PostId = commentDto.PostId
        };
    }
}