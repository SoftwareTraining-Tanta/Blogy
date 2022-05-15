using System;
using System.Collections.Generic;

namespace testproject.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Usernames = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string DateTime { get; set; } = null!;
        public string? Username { get; set; }
        public bool IsAdmin { get; set; }
        public byte[]? Image { get; set; }
        public string? AdminUsername { get; set; }
        public int ReachCount { get; set; }

        public virtual Admin? AdminUsernameNavigation { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<User> Usernames { get; set; }
        public void PinPost(string username, int postId)
        {
            using (blogyContext db = new())
            {
                var PinnedPosts = new Class { Username = username, PostId = postId };
                PinnedPosts.Posts.Add(new Student { Name = "Alice" });
                mathClass.Students.Add(new Student { Name = "Bob" });

                context.AddToClasses(mathClass);
                context.SaveChanges();
            }
        }
    }

}
