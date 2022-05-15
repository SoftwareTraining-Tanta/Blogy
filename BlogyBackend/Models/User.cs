using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using BlogyBackend.Interfaces;
using BlogyBackend.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using BlogyBackend.Dtos;

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
        [Column("postCount")]
        public int PostCount { get; set; }
        [Column("profilePicture", TypeName = "longblob")]
        public byte[]? ProfilePicture { get; set; }
        [Column("isSigned")]
        public bool IsSigned { get; set; }

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
        public User Get(string username)
        {
            using (blogyContext db = new())
            {
                return db.Users?.FirstOrDefault(u => u.Username == username)!;
            }
        }
        public List<User> GetLimit(int limit)
        {
            using (blogyContext db = new())
            {
                List<User> users = db.Users.Take(limit).ToList();
                return users;
            }
        }
        public static bool Exists(string username)
        {
            using (blogyContext db = new())
            {
                return db.Users?.Any(u => u.Username == username) ?? false;
            }
        }
        public static bool CheckNumber(string phoneNumber)
        {
            using (blogyContext db = new())
            {
                return db.Users?.Any(u => u.Phone == phoneNumber) ?? false;
            }
        }
        public static bool CheckEmail(string email)
        {
            using (blogyContext db = new())
            {
                return db.Users?.Any(u => u.Email == email) ?? false;
            }
        }
        public void UpdateIsSigned(string username)
        {
            using (blogyContext db = new())
            {
                User? user = db.Users?.FirstOrDefault(u => u.Username == username);
                user!.IsSigned = !user.IsSigned;
                db.SaveChanges();
            }
        }
        public void IncrementPostCount(string username)
        {
            using (blogyContext db = new())
            {
                User? user = db.Users?.FirstOrDefault(u => u.Username == username);
                user!.PostCount++;
                db.SaveChanges();
            }
        }

        public string Register(User user)
        {
            string verficationCode = new Random().Next(0, 999999).ToString();
            if (User.Exists(user.Username))
                throw new Exception(MyExceptions.UsernameAlreadyExists);
            if (TempUser.Exists(user.Username))
                throw new Exception(MyExceptions.UsernameAlreadyExistsButNotVerified);
            if (User.CheckNumber(user.Phone!))
                throw new Exception(MyExceptions.PhoneNumberAlreadyUsed);
            if (TempUser.CheckNumber(user.Phone!))
                throw new Exception(MyExceptions.PhoneNumberAlreadyUsed);
            if (User.CheckEmail(user.Email!))
                throw new Exception(MyExceptions.EmailAlreadyUsed);
            if (TempUser.CheckEmail(user.Email!))
                throw new Exception(MyExceptions.EmailAlreadyUsed);

            TempUser tempUser = user.AsTempUser(verficationCode);
            Smtp.SendMessage(
                toEmail: tempUser.Email!,
                subject: "Blogy Verification",
                body: $"Your verification code is {verficationCode}"
                );
            tempUser.Add(tempUser);
            return verficationCode;
        }
        public void Verify(string username, string verificationCode, string planType)
        {
            if (planType != "Basic" && planType != "Premium") throw new Exception(MyExceptions.InvalidPlanType);
            TempUser _tempUser = new TempUser();
            TempUser? tempUser = _tempUser.Get(username);
            if (tempUser.VerificationCode != verificationCode)
                throw new Exception("Verification code is not correct");
            try
            {
                using (blogyContext db = new())
                {
                    User user = tempUser.AsNormalUser();
                    db.Users.Add(user);
                    db.TempUsers.Remove(tempUser);
                    db.SaveChanges();
                    Plan plan = new Plan()
                    {
                        Username = user.Username,
                        Type = planType
                    };
                    plan.Add(plan);
                    db.SaveChanges();
                }
            }
            catch
            {
                throw new Exception("Verification failed , Please check your verification code or user may be already verified");
            }
        }


        IPerson IPerson.Get(string username)
        {
            throw new NotImplementedException();
        }
    }
}
