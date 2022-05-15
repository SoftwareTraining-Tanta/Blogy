using System;
using System.Collections.Generic;

namespace testproject.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public string? Username { get; set; }
        public int PostId { get; set; }
        public bool IsAdmin { get; set; }
        public string? AdminUsername { get; set; }

        public virtual Admin? AdminUsernameNavigation { get; set; }
        public virtual Post Post { get; set; } = null!;
        public virtual User? UsernameNavigation { get; set; }
    }
}
