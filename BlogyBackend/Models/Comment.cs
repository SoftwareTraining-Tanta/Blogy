using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlogyBackend.Models
{
    [Table("comments")]
    [Index(nameof(PostId), Name = "postId")]
    [Index(nameof(Username), Name = "username")]
    public partial class Comment
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("content")]
        [StringLength(1000)]
        public string Content { get; set; } = null!;
        [Column("username")]
        [StringLength(30)]
        public string? Username { get; set; }
        [Column("postId")]
        public int PostId { get; set; }
        [Column("isAdmin")]
        public bool IsAdmin { get; set; }

        [ForeignKey(nameof(PostId))]
        [InverseProperty("Comments")]
        public virtual Post Post { get; set; } = null!;
        [ForeignKey(nameof(Username))]
        [InverseProperty(nameof(User.Comments))]
        public virtual User? UsernameNavigation { get; set; } 
        public string? AdminUsername { get; set; }
        public Admin? admin { get; set; }

        public void Add(Comment comment)
        {
            using (blogyContext db = new())
            {
                db.Comments.Add(comment);
                db.SaveChanges();
            }
        }
    }
}
