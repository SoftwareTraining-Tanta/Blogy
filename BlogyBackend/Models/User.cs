using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlogyBackend.Models
{
    [Table("users")]
    [Index(nameof(Email), Name = "email", IsUnique = true)]
    [Index(nameof(Phone), Name = "phone", IsUnique = true)]
    public partial class User
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
                user!.Password=newPassword;
                db.SaveChanges();
            }
        }
    }
}
