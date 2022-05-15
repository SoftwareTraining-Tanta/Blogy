using System;
using System.Collections.Generic;

namespace testproject.Models
{
    public partial class TempUser
    {
        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Password { get; set; } = null!;
        public byte[]? ProfilePicture { get; set; }
        public string VerificationCode { get; set; } = null!;
    }
}
