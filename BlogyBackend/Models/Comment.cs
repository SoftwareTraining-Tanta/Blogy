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
        public string Username { get; set; } = null!;
        [Column("postId")]
        public int PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        [InverseProperty("Comments")]
        public virtual Post Post { get; set; } = null!;
        [ForeignKey(nameof(Username))]
        [InverseProperty(nameof(User.Comments))]
        public virtual User UsernameNavigation { get; set; } = null!;
        public int adminId {get;set;}
        public Admin admin {get;set;}
        
    }
}
