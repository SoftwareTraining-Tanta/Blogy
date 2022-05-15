using System;
using System.Collections.Generic;

namespace testproject.Models
{
    public partial class Plan
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string? AdminUsername { get; set; }

        public virtual Admin? AdminUsernameNavigation { get; set; }
        public virtual User UsernameNavigation { get; set; } = null!;
    }
}
