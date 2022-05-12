using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlogyBackend.Models
{
    [Table("plans")]
    [Index(nameof(Username), Name = "username")]
    public partial class Plan
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("type")]
        [StringLength(7)]
        public string Type { get; set; } = null!;
        [Column("username")]
        [StringLength(30)]
        public string Username { get; set; } = null!;

        [ForeignKey(nameof(Username))]
        [InverseProperty(nameof(User.Plans))]
        public virtual User UsernameNavigation { get; set; } = null!;

        public void Add(Plan plan)
        {
            using (blogyContext db = new())
            {
                db.Plans.Add(plan);
                db.SaveChanges();
            }
        }
    }
}
