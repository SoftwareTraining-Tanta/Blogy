using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlogyBackend.Models
{
    [Table("posts")]
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Usernames = new HashSet<User>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("title")]
        [StringLength(50)]
        public string Title { get; set; } = null!;
        [Column("content")]
        [StringLength(3000)]
        public string Content { get; set; } = null!;
        [Column("dateTime", TypeName = "datetime")]
        public DateTime DateTime { get; set; }
        [Column("username")]
        [StringLength(30)]
        public string Username { get; set; } = null!;
        [Column("image", TypeName = "blob")]
        public byte[]? Image { get; set; }

        [InverseProperty(nameof(Comment.Post))]
        public virtual ICollection<Comment> Comments { get; set; }

        [ForeignKey("PostId")]
        [InverseProperty(nameof(User.Posts))]
        public virtual ICollection<User> Usernames { get; set; }
    }
}
