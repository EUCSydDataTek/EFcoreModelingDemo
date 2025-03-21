﻿using Microsoft.EntityFrameworkCore;

namespace OneToManyFluentAPI;

class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
        .UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = BloggingDb; Trusted_Connection = True; ")
        .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
}

public class Blog
{
    public int BlogId { get; set; }
    public string? Url { get; set; }
    public ICollection<Post>? Posts { get; set; }
}

public class Post
{
    public int PostId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }

    public int FKBlogId { get; set; }     // FK
    public Blog? Blog { get; set; }
}
