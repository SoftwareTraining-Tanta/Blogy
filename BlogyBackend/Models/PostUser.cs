﻿using System;
using System.Collections.Generic;


namespace BlogyBackend.Models
{
    public partial class PostUser
    {
        public int PostId { get; set; }
        public string Username { get; set; } = null!;
    }
}
