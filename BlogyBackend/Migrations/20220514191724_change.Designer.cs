// <auto-generated />
using System;
using BlogyBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlogyBackend.Migrations
{
    [DbContext(typeof(blogyContext))]
    [Migration("20220514191724_change")]
    partial class change
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");

            modelBuilder.Entity("Admin", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.Property<byte[]>("ProfilePicture")
                        .HasColumnType("longblob");

                    b.HasKey("Username")
                        .HasName("PRIMARY");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("BlogyBackend.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("AdminUsername")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("content");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("isAdmin");

                    b.Property<int>("PostId")
                        .HasColumnType("int")
                        .HasColumnName("postId");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex("AdminUsername");

                    b.HasIndex(new[] { "PostId" }, "postId");

                    b.HasIndex(new[] { "Username" }, "username");

                    b.ToTable("comments");
                });

            modelBuilder.Entity("BlogyBackend.Models.Plan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("AdminUsername")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("varchar(7)")
                        .HasColumnName("type");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex("AdminUsername");

                    b.HasIndex(new[] { "Username" }, "username")
                        .HasDatabaseName("username1");

                    b.ToTable("plans");
                });

            modelBuilder.Entity("BlogyBackend.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("AdminUsername")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("varchar(3000)")
                        .HasColumnName("content");

                    b.Property<string>("DateTime")
                        .IsRequired()
                        .HasMaxLength(26)
                        .HasColumnType("varchar(26)")
                        .HasColumnName("dateTime");

                    b.Property<byte[]>("Image")
                        .HasColumnType("blob")
                        .HasColumnName("image");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("isAdmin");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("title");

                    b.Property<string>("Username")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex("AdminUsername");

                    b.ToTable("posts");
                });

            modelBuilder.Entity("BlogyBackend.Models.TempUser", b =>
                {
                    b.Property<string>("Username")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("username");

                    b.Property<string>("Email")
                        .HasMaxLength(70)
                        .HasColumnType("varchar(70)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .HasMaxLength(13)
                        .HasColumnType("varchar(13)")
                        .HasColumnName("phone");

                    b.Property<byte[]>("ProfilePicture")
                        .HasColumnType("longblob")
                        .HasColumnName("profilePicture");

                    b.Property<string>("VerificationCode")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)")
                        .HasColumnName("verificationCode");

                    b.HasKey("Username")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Email" }, "email")
                        .IsUnique();

                    b.HasIndex(new[] { "Phone" }, "phone")
                        .IsUnique();

                    b.ToTable("tempUsers");
                });

            modelBuilder.Entity("BlogyBackend.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("username");

                    b.Property<string>("Email")
                        .HasMaxLength(70)
                        .HasColumnType("varchar(70)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .HasMaxLength(13)
                        .HasColumnType("varchar(13)")
                        .HasColumnName("phone");

                    b.Property<byte[]>("ProfilePicture")
                        .HasColumnType("longblob")
                        .HasColumnName("profilePicture");

                    b.HasKey("Username")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Email" }, "email")
                        .IsUnique()
                        .HasDatabaseName("email1");

                    b.HasIndex(new[] { "Phone" }, "phone")
                        .IsUnique()
                        .HasDatabaseName("phone1");

                    b.ToTable("users");
                });

            modelBuilder.Entity("PinPost", b =>
                {
                    b.Property<string>("Username")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("username");

                    b.Property<int>("PostId")
                        .HasColumnType("int")
                        .HasColumnName("postId");

                    b.HasKey("Username", "PostId")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.HasIndex(new[] { "PostId" }, "postId")
                        .HasDatabaseName("postId1");

                    b.ToTable("pinPosts", (string)null);
                });

            modelBuilder.Entity("PostUser", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("varchar(255)");

                    b.HasKey("PostId", "Username");

                    b.ToTable("PostUser");
                });

            modelBuilder.Entity("BlogyBackend.Models.Comment", b =>
                {
                    b.HasOne("Admin", "admin")
                        .WithMany("Comments")
                        .HasForeignKey("AdminUsername");

                    b.HasOne("BlogyBackend.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .IsRequired()
                        .HasConstraintName("comments_ibfk_2");

                    b.HasOne("BlogyBackend.Models.User", "UsernameNavigation")
                        .WithMany("Comments")
                        .HasForeignKey("Username")
                        .IsRequired()
                        .HasConstraintName("comments_ibfk_1");

                    b.Navigation("Post");

                    b.Navigation("UsernameNavigation");

                    b.Navigation("admin");
                });

            modelBuilder.Entity("BlogyBackend.Models.Plan", b =>
                {
                    b.HasOne("Admin", "admin")
                        .WithMany("Plans")
                        .HasForeignKey("AdminUsername");

                    b.HasOne("BlogyBackend.Models.User", "UsernameNavigation")
                        .WithMany("Plans")
                        .HasForeignKey("Username")
                        .IsRequired()
                        .HasConstraintName("plans_ibfk_1");

                    b.Navigation("UsernameNavigation");

                    b.Navigation("admin");
                });

            modelBuilder.Entity("BlogyBackend.Models.Post", b =>
                {
                    b.HasOne("Admin", "admin")
                        .WithMany("Posts")
                        .HasForeignKey("AdminUsername");

                    b.Navigation("admin");
                });

            modelBuilder.Entity("PinPost", b =>
                {
                    b.HasOne("BlogyBackend.Models.Post", null)
                        .WithMany()
                        .HasForeignKey("PostId")
                        .IsRequired()
                        .HasConstraintName("pinPosts_ibfk_2");

                    b.HasOne("BlogyBackend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("Username")
                        .IsRequired()
                        .HasConstraintName("pinPosts_ibfk_1");
                });

            modelBuilder.Entity("Admin", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Plans");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("BlogyBackend.Models.Post", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("BlogyBackend.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Plans");
                });
#pragma warning restore 612, 618
        }
    }
}
