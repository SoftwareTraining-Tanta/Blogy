﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlogyBackend.Interfaces;
using BlogyBackend.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogyBackend.Models
{
    [Table("users")]
    [Index(nameof(Email), Name = "email", IsUnique = true)]
    [Index(nameof(Phone), Name = "phone", IsUnique = true)]
    public partial class User : IPerson
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Plans = new HashSet<Plan>();
            Posts = new HashSet<Post>();
        }

        [Key]
        [Column("username")]
        [StringLength(30)]
        public string Username { get; set; } = null!;
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [Column("email")]
        [StringLength(70)]
        public string? Email { get; set; }
        [Column("phone")]
        [StringLength(13)]
        public string? Phone { get; set; }
        [Column("password")]
        [StringLength(256)]
        public string Password { get; set; } = null!;
        [Column("profilePicture")]
        public byte[]? ProfilePicture { get; set; }

        [InverseProperty(nameof(Comment.UsernameNavigation))]
        public virtual ICollection<Comment> Comments { get; set; }
        [InverseProperty(nameof(Plan.UsernameNavigation))]
        public virtual ICollection<Plan> Plans { get; set; }

        [ForeignKey("Username")]
        [InverseProperty(nameof(Post.Usernames))]
        public virtual ICollection<Post> Posts { get; set; }
        public void Add(User user)
        {
            using (blogyContext db = new())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
        public void UpdatePassword(string username, string newPassword)
        {
            using (blogyContext db = new())
            {
                User? user = db.Users?.FirstOrDefault(u => u.Username == username);
                user!.Password = newPassword;
                db.SaveChanges();
            }
        }
        public IPerson Get(string username)
        {
            using (blogyContext db = new())
            {
                return db.Users?.FirstOrDefault(u => u.Username == username)!;
            }
        }
        public bool Exists(string username)
        {
            using (blogyContext db = new())
            {
                return db.Users?.Any(u => u.Username == username) ?? false;
            }
        }
        public string Register(User user)
        {
            string verficationCode = new Random().Next(0, 99999).ToString();

            TempUser tempUser = user.AsTempUser(verficationCode);
            Smtp.SendMessage(
                toEmail: tempUser.Email!,
                subject: "Blogy Verification",
                body: $"Your verification code is {verficationCode}"
                );
            tempUser.Add(tempUser);
            return verficationCode;
        }
        public void Verify(string username, string verificationCode)
        {
            TempUser _tempUser = new TempUser();
            try
            {
                using (blogyContext db = new())
                {
                    TempUser? tempUser = _tempUser.Get(username);
                    if (tempUser.VerificationCode == verificationCode)
                    {
                        User user = tempUser.AsNormalUser();
                        db.Users.Add(user);
                        db.TempUsers.Remove(tempUser);
                        db.SaveChanges();
                    }
                    else
                        throw new Exception("Verification code is not correct");

                }
            }
            catch
            {
                throw new Exception("Verification failed , Please check your verification code or user may be already verified");
            }
        }
    }
}
