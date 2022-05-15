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
        [Column("dateTime")]
        [StringLength(26)]
        public string DateTime { get; set; } = null!;
        [Column("username")]
        [StringLength(30)]
        public string? Username { get; set; }
        [Column("reachCount")]
        public int ReachCount { get; set; }
        [Column("isAdmin")]
        public bool IsAdmin { get; set; }
        [Column("image", TypeName = "longblob")]
        public byte[]? Image { get; set; }

        [InverseProperty(nameof(Comment.Post))]
        public virtual ICollection<Comment> Comments { get; set; }

        [ForeignKey("PostId")]
        [InverseProperty(nameof(User.Posts))]
        public virtual ICollection<User> Usernames { get; set; }
        public string? AdminUsername { get; set; }
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
        public void UpdateReachCount(int id)
        {
            using (blogyContext db = new())
            {
                Post? post = db.Posts.FirstOrDefault(p => p.Id == id);
                post!.ReachCount++;
                db.SaveChanges();
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
        public void PinPost(string username, int id)
        {
            using (blogyContext db = new())
            {
                User? user = db.Users.FirstOrDefault(u => u.Username == username);
                Post? post = db.Posts.FirstOrDefault(p => p.Id == id);
                user!.Posts.Add(post!);
                post!.Usernames.Add(user!);
                db.SaveChanges();
            }
        }
        public List<Post> GetPinnedPosts(string username)
        {
            using (blogyContext db = new())
            {
                List<Post> pinnedPosts = db.Users.Include(u => u.Posts).FirstOrDefault(u => u.Username == username)?.Posts.ToList()!;

                return pinnedPosts;
            }
        }
    }
}
