using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using BlogyBackend.Interfaces;
using BlogyBackend.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using BlogyBackend.Models;
using BlogyBackend.Dtos;

public partial class Admin

{
    public Admin()
    {
        Comments = new HashSet<Comment>();
        Plans = new HashSet<Plan>();
        Posts = new HashSet<Post>();
    }


    public string Username { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string Password { get; set; } = null!;
    [Column("profilePicture", TypeName = "longblob")]
    public byte[]? ProfilePicture { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<Plan> Plans { get; set; }
    public virtual ICollection<Post> Posts { get; set; }

    private static void Add(Admin admin)
    {
        using (blogyContext db = new())
        {
            db.Admins.Add(admin);
            db.SaveChanges();


        }

    }

    public void UpdatePassword(string username, string newPassword)
    {
        using (blogyContext db = new())
        {
            Admin? admin = db.Admins?.FirstOrDefault(u => u.Username == username);
            admin!.Password = newPassword;

            db.SaveChanges();
        }
    }

    public static AdminDto? Get(string username)
    {
        using (blogyContext db = new())
        {
            return db.Admins?.FirstOrDefault(u => u.Username == username)!.AsDto();
        }
    }
    public static bool Exists(string username)
    {
        using (blogyContext db = new())
        {
            return db.Admins?.Any(u => u.Username == username) ?? false;
        }
    }
    public static bool CheckNumber(string phoneNumber)
    {
        using (blogyContext db = new())
        {
            return db.Admins?.Any(u => u.Phone == phoneNumber) ?? false;
        }
    }
    public static bool CheckEmail(string email)
    {
        using (blogyContext db = new())
        {
            return db.Admins?.Any(u => u.Email == email) ?? false;
        }
    }
    public static async Task<string> UpdateAsync(string username, AdminDto newData)
    {
        if (Admin.Exists(username))
        {
            using (blogyContext db = new())
            {
                Admin admin = await db.Admins.FirstAsync(admin => admin.Username == username);
                admin.Email = newData.Email;
                admin.Phone = newData.Phone;
                admin.Password = newData.Password;
                admin.ProfilePicture = newData.ProfilePicture?.ToBytes();
                await db.SaveChangesAsync();
            }
        }
        else
        {
            throw new Exception("Admin Not Found");

        }


        return "Updated Successfully.";
    }
    public string Register(Admin admin)
    {
        if (Admin.Exists(admin.Username))
            throw new Exception(MyExceptions.UsernameAlreadyExists);

        if (Admin.CheckNumber(admin.Phone!))
            throw new Exception(MyExceptions.PhoneNumberAlreadyUsed);
        if (Admin.CheckEmail(admin.Email!))
            throw new Exception(MyExceptions.EmailAlreadyUsed);
        Admin.Add(admin);
        Smtp.SendMessage(
            toEmail: admin.Email!,
            subject: "Blogy Admin dashboard",
            body: $"Hello {admin.Name}, You've been add as Admin on Blogy with username: {admin.Username}.\n we're So glad to work with you. 🤗❤️"
            );
        return "Added Successfully.";
    }
    public static bool Credentials(string username, string password)
    {
        bool result = false;
        using (blogyContext db = new())
        {
            result = db.Admins.Any(u => u.Password == password && u.Username == username);




        }
        return result;
    }



}
