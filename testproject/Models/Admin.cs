using System;
using System.Collections.Generic;

namespace testproject.Models
{
    public partial class Admin
    {
        public Admin()
        {
            Comments = new HashSet<Comment>();
            Plans = new HashSet<Plan>();
            Posts = new HashSet<Post>();
        }

        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Password { get; set; } = null!;
        public byte[]? ProfilePicture { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Plan> Plans { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
