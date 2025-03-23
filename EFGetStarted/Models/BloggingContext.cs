using EFGetStarted.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;

// https://learn.microsoft.com/da-dk/ef/core/logging-events-diagnostics/simple-logging

namespace EFGetStarted;

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = BloggingDb; Trusted_Connection = True; ")
            .EnableSensitiveDataLogging(true)
            //.LogTo(Console.WriteLine);
            .LogTo(Console.WriteLine, LogLevel.Information);
            //.LogTo(message => Debug.WriteLine(message), LogLevel.Information);

        optionsBuilder.UseSeeding((context, _) =>
         {
             Blog testBlog = context.Set<Blog>().FirstOrDefault(b => b.Url == "http://test1.com");
             if (testBlog == null)
             {
                 Blog newBlog = new Blog { Url = "http://test1.com" };
                 newBlog.Posts.Add(new Post { Title = "Post 1", Content = "Content 1" });
                 newBlog.Posts.Add(new Post { Title = "Post 2", Content = "Content 2" });
                 context.Set<Blog>().Add(newBlog);
             }

             testBlog = context.Set<Blog>().FirstOrDefault(b => b.Url == "http://test2.com");
             if (testBlog == null)
             {
                 Blog newBlog = new Blog { Url = "http://test2.com" };
                 newBlog.Posts.Add(new Post { Title = "Post 3", Content = "Content 3" });
                 newBlog.Posts.Add(new Post { Title = "Post 4", Content = "Content 4" });
                 context.Set<Blog>().Add(newBlog);
             }
             context.SaveChanges();
         });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<BlogMetadata>();

        modelBuilder.Entity<BlogMetadata>()
            .ToTable("blog-metadata");

        //modelBuilder.HasDefaultSchema("blogging");


        //modelBuilder.Entity<BlogMetadata>()
        //    .Ignore(b => b.LoadedFromDatabase);


        //modelBuilder.Entity<BlogMetadata>()
        //    .HasKey(b => b.Id)
        //    .HasName("PrimaryKey_BlogMetadataId");

        modelBuilder.Entity<BlogMetadata>()
            .Property(p => p.Created)
            .HasComputedColumnSql("getdate()");

        // Old way of seeding
        //    modelBuilder.Entity<Blog>().HasData(
        //        new Blog { BlogId = 1, Url = "http://sample1.com" },
        //        new Blog { BlogId = 2, Url = "http://sample2.com" });

        //    modelBuilder.Entity<Post>().HasData(
        //        new Post { PostId = 1, Title = "Post 1", Content = "Content 1",  BlogId = 1   },
        //        new Post { PostId = 2, BlogId = 2 });
    }
}