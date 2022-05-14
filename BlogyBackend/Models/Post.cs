using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Column("dateTime")]
        [StringLength(26)]
        public string DateTime { get; set; } = null!;
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
        public int adminId { get; set; }
        public Admin? admin { get; set; }
        public void Add(Post post)
        {
            using (blogyContext db = new())
            {
                db.Posts.Add(post);
                db.SaveChanges();
            }
        }
        public void UpdateContent(int id, string newContent)
        {
            using (blogyContext db = new())
            {
                Post? post = db.Posts.FirstOrDefault(p => p.Id == id);
                post!.Content = newContent;
                db.SaveChanges();
            }
        }
        public Post Get(int id)
        {
            using (blogyContext db = new())
            {
                return db.Posts?.FirstOrDefault(p => p.Id == id)!;
            }
        }
        public List<Post> GetLimit(int limit)
        {
            using (blogyContext db = new())
            {
                return db.Posts?.Take(limit).ToList()!;
            }
        }
        public void Delete(int id)
        {
            using (blogyContext db = new())
            {
                db.Posts.Remove(db.Posts?.Where(p => p.Id == id).FirstOrDefault()!);
                db.SaveChanges();
            }
        }
    }
}
