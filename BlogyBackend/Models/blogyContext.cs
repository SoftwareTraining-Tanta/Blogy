using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace BlogyBackend.Models
{
    public partial class blogyContext : DbContext
    {
        public blogyContext() { }

        public blogyContext(DbContextOptions<blogyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Plan> Plans { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(configuration.GetConnectionString("myDb1"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comments_ibfk_2");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comments_ibfk_1");
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Plans)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("plans_ibfk_1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PRIMARY");

                entity.HasMany(d => d.Posts)
                    .WithMany(p => p.Usernames)
                    .UsingEntity<Dictionary<string, object>>(
                        "PinPost",
                        l => l.HasOne<Post>().WithMany().HasForeignKey("PostId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("pinPosts_ibfk_2"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("Username").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("pinPosts_ibfk_1"),
                        j =>
                        {
                            j.HasKey("Username", "PostId").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("pinPosts");

                            j.HasIndex(new[] { "PostId" }, "postId");

                            j.IndexerProperty<string>("Username").HasMaxLength(30).HasColumnName("username");

                            j.IndexerProperty<int>("PostId").HasColumnName("postId");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
