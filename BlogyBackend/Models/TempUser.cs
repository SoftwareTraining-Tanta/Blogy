
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlogyBackend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogyBackend.Models
{
    [Table("tempUsers")]
    [Index(nameof(Email), Name = "email", IsUnique = true)]
    [Index(nameof(Phone), Name = "phone", IsUnique = true)]
    public partial class TempUser : IPerson
    {
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
        [Column("verificationCode")]
        [StringLength(6)]
        public string VerificationCode { get; set; } = null!;

        public void Add(TempUser tempUser)
        {
            using (blogyContext db = new())
            {
                db.TempUsers.Add(tempUser);
                db.SaveChanges();
            }
        }
        public TempUser Get(string username)
        {
            using (blogyContext db = new())
            {
                TempUser? tempUser = db.TempUsers?.FirstOrDefault(x => x.Username == username);
                return tempUser!;
            }
        }

        public void Add(User user)
        {
            throw new NotImplementedException();
        }

        IPerson IPerson.Get(string username)
        {
            throw new NotImplementedException();
        }
    }
}
